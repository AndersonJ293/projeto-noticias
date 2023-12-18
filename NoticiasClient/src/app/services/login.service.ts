import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable, NgModule } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private readonly baseURL = environment.endPoint;

  constructor(private httpClient: HttpClient) {}

  loginUsuario(dadosLogin: any) {
    return this.httpClient.post<any>(
      `${this.baseURL}/CriarTokenIdentity`,
      dadosLogin
    );
  }
}
