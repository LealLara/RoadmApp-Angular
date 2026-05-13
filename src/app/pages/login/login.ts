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
    alert('Login realizado');
  }

  criarConta(){
    this.router.navigate(['/register']);
  }

}