import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { NoticiaService } from '../../services/noticia.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NoticiaViewModel } from '../../models/noticiaViewModel';

@Component({
  selector: 'app-noticias',
  standalone: true,
  imports: [NavbarComponent, CommonModule, HttpClientModule],
  providers: [NoticiaService],
  templateUrl: './noticias.component.html',
  styleUrl: './noticias.component.scss',
})
export class NoticiasComponent implements OnInit {
  listaNoticias!: NoticiaViewModel[];

  constructor(private noticiaService: NoticiaService, private router: Router) {}

  ngOnInit(): void {
    this.ListarNoticias();
  }

  ListarNoticias() {
    this.noticiaService.ListarNoticias().subscribe(
      (noticias) => {
        this.listaNoticias = noticias;
      },
      (error) => {
        this.router.navigate(['/login']);
      }
    );
  }
}
