import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AutenticaService {
  private usuarioAutenticado: boolean = false;

  public DefineToken(token: string): void {
    sessionStorage.setItem('token', token);
  }

  public ObtemToken(): string {
    const token = sessionStorage.getItem('token');
    return token ?? '';
  }

  public LimpaToken(): void {
    sessionStorage.removeItem('token');
  }

  constructor() {}
}
