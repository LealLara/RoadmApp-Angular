import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

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
      alert('Por favor, informe e-mail e senha.');
      return;
    }

    const stored = localStorage.getItem('roadmapp-users');
    const users = stored ? JSON.parse(stored) : [];
    const user = users.find((item: any) => item.email === email);

    if (!user) {
      alert('E-mail não cadastrado.');
      return;
    }

    if (user.password !== senha) {
      alert('Senha inválida.');
      return;
    }

    localStorage.setItem('roadmapp-current-user', JSON.stringify(user));
    this.router.navigate(['/dashboard']);
  }

  criarConta(){
    this.router.navigate(['/register']);
  }

}