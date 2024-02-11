import {Component, Input, input} from '@angular/core';
import {ICategory} from "../../../services/contracts/category";
import {CategoryConstants} from "../../../constants/categoryConstants";
import {IProduct} from "../../../services/contracts/products";
import {Observable} from "rxjs";
import NewCommandModule from "@angular/cli/src/commands/new/cli";
import {FetchService} from "../../../services/fetch-service";

@Component({
  selector: 'app-products-carousel',
  templateUrl: './products-carousel.component.html',
  styleUrl: './products-carousel.component.scss'
})
export class ProductsCarouselComponent {
  @Input() category!: ICategory;
  @Input() containerWidth!: number;

  slideConfig: any;
  products: Observable<IProduct[]> = new Observable<IProduct[]>();

  constructor(fetchService: FetchService) {
    this.products = fetchService.GetProducts(this.category.id);

    const cardsOnPage = Math.floor(this.containerWidth / CategoryConstants.CardWidth);

    this.slideConfig = {
      dots: true,
      infinite: true,
      speed: 500,
      slidesToShow: cardsOnPage,
      slidesToScroll: cardsOnPage,
    };
  }
}
