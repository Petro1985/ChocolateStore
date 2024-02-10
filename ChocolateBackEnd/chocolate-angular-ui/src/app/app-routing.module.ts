import { NgModule } from '@angular/core';
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet, Routes} from '@angular/router';
import {CategoriesListComponent} from "./pages/categories-list/categories-list/categories-list.component";
import {ProductsListComponent} from "./pages/products-list/products-list/products-list.component";
import {CommonModule} from "@angular/common";

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
  imports: [
    RouterModule.forRoot(routes),
    CommonModule, RouterOutlet, RouterLink, RouterLinkActive,
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
