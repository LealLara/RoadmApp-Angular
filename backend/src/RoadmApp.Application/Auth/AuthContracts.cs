using RoadmApp.Application.Users;

namespace RoadmApp.Application.Auth;

public sealed record RegisterUserRequest(string Name, string Email, string Password);

public sealed record LoginRequest(string Email, string Password);

public sealed record AuthResponse(UserDto User);
