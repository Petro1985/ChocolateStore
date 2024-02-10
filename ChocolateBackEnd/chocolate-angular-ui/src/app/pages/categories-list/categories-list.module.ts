import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {CategoriesListComponent} from "./categories-list/categories-list.component";
import {CategoryCardComponent} from "./category-card/category-card.component";

@NgModule({
  declarations: [
    CategoriesListComponent,
    CategoryCardComponent,
  ],
  imports: [
    CommonModule,
    NgOptimizedImage
  ]
})
export class CategoriesListModule { }
