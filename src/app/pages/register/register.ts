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
    alert('Conta criada');
  }

  entrar(){
    this.router.navigate(['/login']);
  }

}
