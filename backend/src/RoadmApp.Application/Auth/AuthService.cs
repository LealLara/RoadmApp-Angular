using RoadmApp.Application.Abstractions;
using RoadmApp.Application.Users;
using RoadmApp.Domain.Users;

namespace RoadmApp.Application.Auth;

public sealed class AuthService(
    IUserRepository users,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
{
    public async Task<AuthResponse> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
        {
            throw new InvalidOperationException("Password must contain at least 6 characters.");
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        if (await users.ExistsByEmailAsync(normalizedEmail, cancellationToken))
        {
            throw new InvalidOperationException("Email already registered.");
        }

        var user = new User(request.Name, normalizedEmail, passwordHasher.Hash(request.Password));
        users.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(new UserDto(user.Id, user.Name, user.Email));
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await users.GetByEmailAsync(normalizedEmail, cancellationToken);

        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return new AuthResponse(new UserDto(user.Id, user.Name, user.Email));
    }
}
