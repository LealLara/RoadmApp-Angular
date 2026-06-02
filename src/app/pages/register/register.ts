import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

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
      alert('Preencha nome, e-mail e senha para continuar.');
      return;
    }

    const stored = localStorage.getItem('roadmapp-users');
    const users = stored ? JSON.parse(stored) : [];

    if (users.some((item: any) => item.email === email)) {
      alert('Este e-mail já está registrado.');
      return;
    }

    const user = { nome, email, password: senha };
    users.push(user);
    localStorage.setItem('roadmapp-users', JSON.stringify(users));
    localStorage.setItem('roadmapp-current-user', JSON.stringify(user));
    this.router.navigate(['/dashboard']);
  }

  entrar(){
    this.router.navigate(['/login']);
  }

}
