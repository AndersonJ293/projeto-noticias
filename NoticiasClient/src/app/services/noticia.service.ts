import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { NoticiaViewModel } from '../models/noticiaViewModel';

@Injectable({
  providedIn: 'root',
})
export class NoticiaService {
  private readonly baseURL = environment.endPoint;

  constructor(private httpClient: HttpClient) {}

  public ListarNoticias() {
    return this.httpClient.post<NoticiaViewModel[]>(
      `${this.baseURL}/ListarNoticiasCustomizada`,
      null
    );
  }

  public AdicionaNoticia(noticia: NoticiaViewModel) {
    return this.httpClient.post<any>(
      `${this.baseURL}/AdicionaNoticia`,
      noticia
    );
  }
}
