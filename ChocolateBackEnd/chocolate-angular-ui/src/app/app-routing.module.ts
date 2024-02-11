import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CategoriesListComponent} from "./pages/categories-list/categories-list/categories-list.component";

const routes: Routes = [
  {
    path: 'Categories',
    component: CategoriesListComponent
  },
  {
    path: '**',
    redirectTo: '/Categories',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
