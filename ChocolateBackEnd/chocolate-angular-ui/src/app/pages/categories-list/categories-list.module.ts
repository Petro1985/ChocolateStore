import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {CategoriesListComponent} from "./categories-list/categories-list.component";
import {CategoryCardComponent} from "./category-card/category-card.component";
import {SlickCarouselModule} from "ngx-slick-carousel";
import {ProductsCarouselComponent} from "./products-carousel/products-carousel.component";
import {ProductCardComponent} from "./product-card/product-card.component";

@NgModule({
  declarations: [
    CategoriesListComponent,
    CategoryCardComponent,
    ProductsCarouselComponent,
    ProductCardComponent,
  ],
    imports: [
        CommonModule,
        NgOptimizedImage,
        SlickCarouselModule
    ]
})
export class CategoriesListModule { }
