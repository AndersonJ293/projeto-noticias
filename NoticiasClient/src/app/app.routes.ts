import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { NoticiasComponent } from './pages/noticias/noticias.component';
import { AddNoticiaComponent } from './pages/add-noticia/add-noticia.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'noticias', component: NoticiasComponent },
  { path: 'addNoticia', component: AddNoticiaComponent },
];
