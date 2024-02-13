import {
  ChangeDetectorRef,
  Component,
  HostListener,
  Input,
  OnInit
} from '@angular/core';
import {ICategory} from "../../../services/contracts/category";
import {CategoryConstants} from "../../../constants/categoryConstants";
import {IProduct} from "../../../services/contracts/products";
import {Observable} from "rxjs";
import {FetchService} from "../../../services/fetch-service";

@Component({
  selector: 'app-products-carousel',
  templateUrl: './products-carousel.component.html',
  styleUrl: './products-carousel.component.scss'
})
export class ProductsCarouselComponent implements OnInit {
  @Input() category!: ICategory;
  @Input() containerWidth!: number;

  slideConfig: any;
  products: Observable<IProduct[]> = new Observable<IProduct[]>();

  constructor(private fetchService: FetchService, private cd: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.products = this.fetchService.GetProducts(this.category.id)
    // this.UpdateConfig(null);
    this.slideConfig = {
      dots: true,
      arrows: true,
      infinite: true,
      speed: 500,
      slidesToShow: 4,
      slidesToScroll: 4,
      draggable:false,
      responsive: [
        {
          breakpoint: 1400,
          settings: {
            slidesToShow: 3,
            slidesToScroll: 3,
            infinite: true,
            dots: true
          }
        },
        {
          breakpoint: 1000,
          settings: {
            slidesToShow: 2,
            slidesToScroll: 2
          }
        },
        {
          breakpoint: 750,
          settings: {
            slidesToShow: 1,
            slidesToScroll: 1
          }
        }
      ]
    };
  }

  // @HostListener('window:resize', ['$event'])
  // UpdateConfig(_: any) {
  //   const cardsOnPage = Math.floor(this.containerWidth / CategoryConstants.CardWidth);
  //   this.slideConfig = {
  //     dots: true,
  //     arrows: true,
  //     infinite: true,
  //     speed: 500,
  //     slidesToShow: cardsOnPage,
  //     slidesToScroll: cardsOnPage,
  //   };
  // }
}
