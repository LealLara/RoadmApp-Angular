import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  email = '';
  senha = '';

  constructor(private router: Router){}

  logar(){
    const email = this.email.trim().toLowerCase();
    const senha = this.senha.trim();

    if (!email || !senha) {
      Swal.fire({
        icon: 'warning',
        title: 'Campos incompletos',
        text: 'Por favor, informe e-mail e senha.',
        confirmButtonColor: '#0f766e'
      });
      return;
    }

    const stored = localStorage.getItem('roadmapp-users');
    const users = stored ? JSON.parse(stored) : [];
    const user = users.find((item: any) => item.email === email);

    if (!user) {
      Swal.fire({
        icon: 'error',
        title: 'Usuário não encontrado',
        text: 'E-mail não cadastrado.',
        confirmButtonColor: '#0f766e'
      });
      return;
    }

    if (user.password !== senha) {
      Swal.fire({
        icon: 'error',
        title: 'Acesso negado',
        text: 'Senha inválida.',
        confirmButtonColor: '#0f766e'
      });
      return;
    }

    localStorage.setItem('roadmapp-current-user', JSON.stringify(user));
    Swal.fire({
      icon: 'success',
      title: 'Bem-vindo!',
      text: `Olá, ${user.nome}`,
      confirmButtonColor: '#0f766e',
      timer: 1500,
      showConfirmButton: false
    });
    setTimeout(() => {
      this.router.navigate(['/dashboard']);
    }, 1500);
  }

  criarConta(){
    this.router.navigate(['/register']);
  }

}