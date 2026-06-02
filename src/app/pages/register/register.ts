import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  nome = '';
  email = '';
  senha = '';

  constructor(private router: Router){}

  cadastrar(){
    const nome = this.nome.trim();
    const email = this.email.trim().toLowerCase();
    const senha = this.senha.trim();

    if (!nome || !email || !senha) {
      Swal.fire({
        icon: 'warning',
        title: 'Campos incompletos',
        text: 'Preencha nome, e-mail e senha para continuar.',
        confirmButtonColor: '#0f766e'
      });
      return;
    }

    const stored = localStorage.getItem('roadmapp-users');
    const users = stored ? JSON.parse(stored) : [];

    if (users.some((item: any) => item.email === email)) {
      Swal.fire({
        icon: 'error',
        title: 'E-mail já registrado',
        text: 'Este e-mail já está registrado.',
        confirmButtonColor: '#0f766e'
      });
      return;
    }

    const user = { nome, email, password: senha };
    users.push(user);
    localStorage.setItem('roadmapp-users', JSON.stringify(users));
    localStorage.setItem('roadmapp-current-user', JSON.stringify(user));
    
    Swal.fire({
      icon: 'success',
      title: 'Conta criada!',
      text: `Bem-vindo, ${nome}!`,
      confirmButtonColor: '#0f766e',
      timer: 1500,
      showConfirmButton: false
    });
    setTimeout(() => {
      this.router.navigate(['/dashboard']);
    }, 1500);
  }

  entrar(){
    this.router.navigate(['/login']);
  }

}
