import {Component, ElementRef, Input, OnInit, Renderer2, SimpleChanges, ViewChild} from '@angular/core';
import {IProduct} from "../../../services/contracts/products";
import {ImageService} from "../../../services/imageService";
import {SwiperContainer} from "swiper/swiper-element";
// import {SwiperOptions} from "swiper/types";
// import { A11y, Mousewheel, Navigation, Pagination, Thumbs} from "swiper/types/modules";
// import { Thumbs } from "swiper/types/modules";

@Component({
  selector: 'app-product-modal',
  templateUrl: './product-modal.component.html',
  styleUrl: './product-modal.component.scss',
})
export class ProductModalComponent implements OnInit {
  @Input() product!: IProduct;
  @Input() closeFunction!: Function;

  // @ViewChild('mainSwiper', {static: true}) mainSwiper!: ElementRef<SwiperContainer>
  // @ViewChild('thumbnailSwiper', {static: true}) thumbnailSwiper!: ElementRef<SwiperContainer>
  // @ViewChild('testSwiper', {static: true}) testSwiper!: ElementRef<SwiperContainer>
  @ViewChild('mainSwiper', {static: false}) mainSwiper!: ElementRef<SwiperContainer>
  @ViewChild('thumbnailSwiper', {static: false}) thumbnailSwiper!: ElementRef<SwiperContainer>
  @ViewChild('testSwiper', {static: false}) testSwiper!: ElementRef<SwiperContainer>

  currentPhoto: number = 0;

  constructor(public imageService: ImageService, private renderer: Renderer2) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    // if (changes['product']) {
    //   if (this.mainSwiper) {
    //     // Заполняем основной Swiper
    //     this.mainSwiper.nativeElement.swiper.virtual.removeAllSlides();
    //     this.mainSwiper.nativeElement.swiper.virtual.appendSlide(this.getMainSlides());
    //     this.mainSwiper.nativeElement.swiper.virtual.update(true);
    //     // this.mainSwiper.nativeElement.swiper.removeAllSlides();
    //     // this.mainSwiper.nativeElement.swiper.appendSlide(this.getMainSlides());
    //     // this.mainSwiper.nativeElement.swiper.appendSlide(this.getMainSlides());
    //     // this.mainSwiper.nativeElement.slidesPerView = 1;
    //     // this.mainSwiper.nativeElement.swiper.update();
    //     this.mainSwiper.nativeElement.swiper.activeIndex = 0;
    //
    //     // this.mainSwiper.nativeElement.loop = this.mainSwiper.nativeElement.swiper.virtual.slides.length > 2;
    //
    //     // Заполняем Swiper для маленьких фотографий
    //     this.thumbnailSwiper.nativeElement.swiper.virtual.removeAllSlides();
    //     this.thumbnailSwiper.nativeElement.swiper.virtual.appendSlide(this.getThumbnailSlides());
    //     this.thumbnailSwiper.nativeElement.swiper.virtual.update(false);
    //     this.thumbnailSwiper.nativeElement.swiper.activeIndex = 0;
    //     this.thumbnailSwiper.nativeElement.loop = this.thumbnailSwiper.nativeElement.swiper.virtual.slides.length > 2;
    //   }
    // }
  }

  onThumbnailClick(i:number, e: any): void {
    // this.mainSwiper.nativeElement.swiper.slideNext();
    this.mainSwiper.nativeElement.swiper.slideTo(i, 500);
    this.thumbnailSwiper.nativeElement.swiper.slides.forEach(x => {
      const divContainer = x.querySelector('div.swiper-thumbnail-slide-image');
      if (divContainer){
        divContainer.classList.remove('thumb-slide-active');
      }
    })
    e.target.parentElement.classList.add('thumb-slide-active');
  }

  Close() {
    this.mainSwiper.nativeElement.loop = false;
    this.closeFunction();
  }

  ngOnInit() {
    // const config: SwiperOptions = {
    //   // modules: [ Navigation, Pagination, A11y, Mousewheel],
    //   autoHeight: true,
    //   spaceBetween: 20,
    //   navigation: false,
    //   pagination: {clickable: true, dynamicBullets: true},
    //   slidesPerView: 1,
    //   centeredSlides: true,
    //   breakpoints: {
    //     400: {
    //       slidesPerView: "auto",
    //       centeredSlides: false
    //     },
    //   }
    // }

    // this.mainSwiper.nativeElement.slidesPerView = 1;
    // this.mainSwiper.nativeElement.speed= 500;
    // this.mainSwiper.nativeElement.loop = false;
    //
    // this.thumbnailSwiper.nativeElement.spaceBetween = 2;
    // this.thumbnailSwiper.nativeElement.slidesPerView = 4;
    // this.thumbnailSwiper.nativeElement.swiper.params.freeMode = true;
  }

  // private getThumbnailSlides() {
  //   return this.product.photos.map((x, i) => {
  //     const imageContainerHtmlElement: HTMLDivElement = this.renderer.createElement('div');
  //     const imageHtmlElement: HTMLImageElement = this.renderer.createElement('img');
  //
  //     imageHtmlElement.src = this.imageService.getThumbnailImageUrl(x);
  //     imageHtmlElement.style.width = '100%';
  //     imageHtmlElement.style.height = '100%';
  //
  //     imageContainerHtmlElement.classList.add('swiper-thumbnail-slide-image');
  //
  //     imageHtmlElement.addEventListener('click', (e) => this.onThumbnailClick(i, e))
  //
  //     // if (i === 0)
  //     // {
  //     //   slideHtmlElement.classList.add('swiper-thumbnail-slide-image');
  //     // }
  //
  //     imageContainerHtmlElement.appendChild(imageHtmlElement);
  //     return imageContainerHtmlElement.outerHTML;
  //   });
  // }
  //
  // private getMainSlides() {
  //   return this.product.photos?.map((x, i) => {
  //     const imageContainerHtmlElement: HTMLDivElement = this.renderer.createElement('div');
  //     const imageHtmlElement: HTMLImageElement = this.renderer.createElement('img');
  //
  //     imageHtmlElement.src = this.imageService.getImageUrl(x);
  //     imageHtmlElement.style.width = '100%';
  //     imageHtmlElement.style.height = '100%';
  //
  //     // imageContainerHtmlElement.classList.add('swiper-slide-image');
  //
  //     // if (i === 0)
  //     // {
  //     //   slideHtmlElement.classList.add('swiper-thumbnail-slide-image');
  //     // }
  //
  //     imageContainerHtmlElement.appendChild(imageHtmlElement);
  //     return imageContainerHtmlElement.outerHTML;
  //   }) ?? [];
  // }
  OnMainSwiperUpdate() {
    if (this.product.photos)
    {
      this.mainSwiper.nativeElement.loop = this.product.photos.length > 2;
      this.mainSwiper.nativeElement.swiper.slideTo(0, 0);
      console.log('updated!!!')
    }
  }
}
