import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AutenticaService } from '../../services/autentica.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  constructor(
    private router: Router,
    private autenticaService: AutenticaService
  ) {}

  sair() {
    this.autenticaService.LimpaToken();
    this.router.navigate(['/login']);
  }
}
