import { Routes } from '@angular/router';
import { LibraryComponent } from './components/library/library.component';

export const routes: Routes = [
  // Root path
  { path: '', component: LibraryComponent },

  // /library
  { path: 'library', component: LibraryComponent },

  // wildcard (tu mohol byť PageNotFoundComponent)
  { path: '**', component: LibraryComponent },
];
