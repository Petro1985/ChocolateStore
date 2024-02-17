import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {CategoriesListComponent} from "./categories-list/categories-list.component";
import {CategoryCardComponent} from "./category-card/category-card.component";
import {SlickCarouselModule} from "ngx-slick-carousel";
import {ProductsCarouselComponent} from "./products-carousel/products-carousel.component";
import {ProductCardComponent} from "./product-card/product-card.component";
import { MainPageComponent } from './main-page/main-page.component';
import { ProductModalComponent } from './product-modal/product-modal.component';
import {ModalModule} from "../../_modal";

@NgModule({
  declarations: [
    CategoriesListComponent,
    CategoryCardComponent,
    ProductsCarouselComponent,
    ProductCardComponent,
    MainPageComponent,
    ProductModalComponent,
  ],
  imports: [
    CommonModule,
    NgOptimizedImage,
    SlickCarouselModule,
    ModalModule
  ]
})
export class MainPageModule { }
