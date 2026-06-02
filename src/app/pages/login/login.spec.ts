import { TestBed, ComponentFixture } from '@angular/core/testing';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { describe, it, expect, beforeEach, afterEach, vi } from 'vitest';
import { Login } from './login';
import Swal from 'sweetalert2';

describe('Login Component', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;
  let router: Router;

  beforeEach(async () => {
    const routerMock = { navigate: vi.fn() };
    
    await TestBed.configureTestingModule({
      imports: [Login, FormsModule],
      providers: [
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
    vi.spyOn(Swal, 'fire').mockResolvedValue({ isConfirmed: true } as any);
  });

  it('should create login component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with empty email and senha', () => {
    expect(component.email).toBe('');
    expect(component.senha).toBe('');
  });

  it('should show warning when email is empty', async () => {
    component.email = '';
    component.senha = 'senha123';
    await component.logar();

    expect(Swal.fire).toHaveBeenCalled();
  });

  it('should show warning when senha is empty', async () => {
    component.email = 'test@email.com';
    component.senha = '';
    await component.logar();

    expect(Swal.fire).toHaveBeenCalled();
  });

  it('should show error when email does not exist', async () => {
    localStorage.setItem('roadmapp-users', JSON.stringify([
      { nome: 'John', email: 'john@test.com', password: 'pass123' }
    ]));

    component.email = 'nonexistent@test.com';
    component.senha = 'pass123';
    await component.logar();

    expect(Swal.fire).toHaveBeenCalled();
  });

  it('should show error when password is incorrect', async () => {
    localStorage.setItem('roadmapp-users', JSON.stringify([
      { nome: 'John', email: 'john@test.com', password: 'correctpass' }
    ]));

    component.email = 'john@test.com';
    component.senha = 'wrongpass';
    await component.logar();

    expect(Swal.fire).toHaveBeenCalled();
  });

  it('should navigate to register on criarConta', () => {
    component.criarConta();
    expect(router.navigate).toHaveBeenCalledWith(['/register']);
  });

  afterEach(() => {
    vi.clearAllMocks();
  });
});

