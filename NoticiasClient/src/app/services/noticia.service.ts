import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class NoticiaService {
  private readonly baseURL = environment.endPoint;

  constructor(private httpClient: HttpClient) {}

  public ListarNoticias() {
    return this.httpClient.post<any>(`${this.baseURL}/ListarNoticias`, null);
  }
}
