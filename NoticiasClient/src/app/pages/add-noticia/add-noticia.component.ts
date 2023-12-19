import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NoticiaService } from '../../services/noticia.service';
import { NoticiaViewModel } from '../../models/noticiaViewModel';

@Component({
  selector: 'app-add-noticia',
  standalone: true,
  imports: [CommonModule, NavbarComponent, ReactiveFormsModule],
  templateUrl: './add-noticia.component.html',
  styleUrl: './add-noticia.component.scss',
})
export class AddNoticiaComponent implements OnInit {
  addNoticiaForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private noticiaService: NoticiaService
  ) {}

  ngOnInit(): void {
    this.addNoticiaForm = this.formBuilder.group({
      titulo: ['', [Validators.required]],
      informacao: ['', [Validators.required]],
    });
  }

  submitAddNoticia(): void {
    this.addNoticiaForm.markAllAsTouched();

    if (this.addNoticiaForm.invalid) return;

    const dadosNovaNoticia =
      this.addNoticiaForm.getRawValue() as NoticiaViewModel;

    let noticia = new NoticiaViewModel();
    noticia.titulo = dadosNovaNoticia.titulo;
    noticia.informacao = dadosNovaNoticia.informacao;

    this.noticiaService.AdicionaNoticia(noticia).subscribe(
      (resposta) => {
        this.router.navigate(['/noticias']);
      },
      (error) => {
        this.router.navigate(['/login']);
      }
    );
  }
}
