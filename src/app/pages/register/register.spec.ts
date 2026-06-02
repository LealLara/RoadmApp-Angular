import { TestBed, ComponentFixture } from '@angular/core/testing';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { describe, it, expect, beforeEach, afterEach, vi } from 'vitest';
import { Register } from './register';
import Swal from 'sweetalert2';

describe('Register Component', () => {
  let component: Register;
  let fixture: ComponentFixture<Register>;
  let router: Router;

  beforeEach(async () => {
    const routerMock = { navigate: vi.fn() };
    
    await TestBed.configureTestingModule({
      imports: [Register, FormsModule],
      providers: [
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Register);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
    vi.spyOn(Swal, 'fire').mockResolvedValue({ isConfirmed: true } as any);
  });

  it('should create register component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with empty fields', () => {
    expect(component.nome).toBe('');
    expect(component.email).toBe('');
    expect(component.senha).toBe('');
  });

  it('should show warning when any field is empty', async () => {
    component.nome = 'John';
    component.email = '';
    component.senha = 'pass123';
    await component.cadastrar();

    expect(Swal.fire).toHaveBeenCalled();
  });

  it('should show error when email already exists', async () => {
    localStorage.setItem('roadmapp-users', JSON.stringify([
      { nome: 'John', email: 'john@test.com', password: 'pass123' }
    ]));

    component.nome = 'Jane';
    component.email = 'john@test.com';
    component.senha = 'newpass123';
    await component.cadastrar();

    expect(Swal.fire).toHaveBeenCalled();
  });

  it('should register successfully with new user', async () => {
    localStorage.setItem('roadmapp-users', JSON.stringify([]));

    component.nome = 'Jane';
    component.email = 'jane@test.com';
    component.senha = 'pass123';
    await component.cadastrar();

    const users = JSON.parse(localStorage.getItem('roadmapp-users') || '[]');
    expect(users.length).toBe(1);
    expect(users[0].email).toBe('jane@test.com');
  });

  it('should navigate to login on entrar', () => {
    component.entrar();
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
  });

  afterEach(() => {
    vi.clearAllMocks();
  });
});
