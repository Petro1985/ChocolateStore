import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {ProductsListComponent} from "./products-list/products-list.component";
import {ProductCardComponent} from "./product-card/product-card.component";

@NgModule({
  declarations: [
    ProductsListComponent,
    ProductCardComponent
  ],
  imports: [
    CommonModule,
    NgOptimizedImage
  ]
})
export class ProductsListModule { }
