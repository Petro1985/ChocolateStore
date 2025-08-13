import {Component, ElementRef, Input, OnInit, Renderer2, SimpleChanges, ViewChild} from '@angular/core';
import {IProduct} from "../../../services/contracts/products";
import {ImageService} from "../../../services/image-service";
import {SwiperContainer} from "swiper/swiper-element";
import {Swiper} from "swiper";
import { ProductService } from '../../../services/product-service';
import { ModalService } from '../../../_modal';
import { EmptyProduct } from '../../../constants/product-constants';

@Component({
  selector: 'app-product-modal',
  templateUrl: './product-modal.component.html',
  styleUrl: './product-modal.component.scss',
})
export class ProductModalComponent implements OnInit {
  public product!: IProduct;
  @Input() closeFunction!: Function;

  @ViewChild('mainSwiper', {static: true}) mainSwiper!: ElementRef<SwiperContainer>
  @ViewChild('thumbnailSwiper', {static: true}) thumbnailSwiper!: ElementRef<SwiperContainer>
  @ViewChild('testSwiper', {static: true}) testSwiper!: ElementRef<SwiperContainer>

  constructor(public imageService: ImageService, private productService: ProductService, private modalService: ModalService) {
    productService.getCurrentProduct().subscribe(p => {
      this.product = p;
    });
  }

  onThumbnailClick(i:number, e: any): void {
    this.mainSwiper.nativeElement.swiper.slideTo(i, 500);
  }

  private setActiveThumbnail(newActiveNumber: number)
  {
    this.thumbnailSwiper.nativeElement.swiper.slides.forEach(x => {
      const divContainer = x.querySelector('div.swiper-thumbnail-slide-image');
      if (divContainer){
        divContainer.classList.remove('thumb-slide-active');
      }
    })

    this.thumbnailSwiper.nativeElement.swiper.slides[newActiveNumber]
      .querySelector('div.swiper-thumbnail-slide-image')?.classList.add('thumb-slide-active');
  }

  Close() {
    this.productService.setCurrentProduct(null);
    this.mainSwiper.nativeElement.loop = false;
    this.modalService.close('product-modal');
  }

  ngOnInit() {
    this.mainSwiper.nativeElement.swiper.on('slideChange' ,swiper => this.MainSwiperOnSlideChange(swiper));
    this.mainSwiper.nativeElement.swiper.on('activeIndexChange' ,swiper => this.setActiveThumbnail(swiper.realIndex));
    this.mainSwiper.nativeElement.swiper.on('slidesUpdated' ,swiper => this.onSwiperInit());

  }

  MainSwiperOnSlideChange(swiper: Swiper) {
    if (this.product?.photos)
    {
      this.thumbnailSwiper.nativeElement.swiper.slideTo(swiper.realIndex, 500);
    }
  }

  private onSwiperInit() {
    this.thumbnailSwiper.nativeElement.swiper.slideTo(0, 0);
    this.mainSwiper.nativeElement.swiper.slideTo(0, 0);
  }
}
