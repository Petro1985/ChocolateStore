import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ProductsListComponent} from "./pages/products-list/products-list/products-list.component";
import {CategoriesListComponent} from "./pages/categories-list/categories-list/categories-list.component";

const routes: Routes = [
  {
    path: 'Categories',
    component: CategoriesListComponent
  },
  {
    path: 'Category/:id',
    component: ProductsListComponent
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
