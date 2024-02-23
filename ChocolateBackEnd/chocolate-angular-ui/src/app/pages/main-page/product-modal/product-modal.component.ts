import {
  Component,
  ElementRef,
  Input,
  OnInit, Renderer2,
  SimpleChanges,
  ViewChild
} from '@angular/core';
import {IProduct} from "../../../services/contracts/products";
import {ImageService} from "../../../services/imageService";
import {SwiperContainer} from "swiper/swiper-element";
import {SwiperOptions} from "swiper/types";
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

  @ViewChild('mainSwiper', {static: true}) mainSwiper!: ElementRef<SwiperContainer>
  @ViewChild('thumbnailSwiper', {static: true}) thumbnailSwiper!: ElementRef<SwiperContainer>

  currentPhoto: number = 0;

  constructor(private imageService: ImageService, private renderer: Renderer2) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['product']) {
      if (this.mainSwiper) {
        // Заполняем основной Swiper
        const newSlides = this.product.photos.map(x => {
          const slideHtmlElement: HTMLDivElement = this.renderer.createElement('div');
          const imageUrl = this.imageService.getImageUrl(x);
          slideHtmlElement.innerHTML = `<div class="swiper-zoom-container"><img src="${imageUrl}" alt="product photo" width="452" height="452"/></div>`;
          slideHtmlElement.classList.add('swiper-slide');
          slideHtmlElement.classList.add('swiper-slide-image');
          return slideHtmlElement;
        })

        this.mainSwiper.nativeElement.swiper.removeAllSlides();
        this.mainSwiper.nativeElement.swiper.addSlide(1, newSlides);
        this.mainSwiper.nativeElement.swiper.activeIndex = 0;
        this.mainSwiper.nativeElement.loop = newSlides.length > 2;

        // // Заполняем Swiper для маленьких фотографий
        const thumbnailSlides = this.product.photos.map((x, i) => {
          const slideHtmlElement: HTMLDivElement = this.renderer.createElement('div');
          const imageUrl = this.imageService.getImageUrl(x);
          slideHtmlElement.innerHTML = `<img src="${imageUrl}" alt="product photo" width="85" height="85"/>`;
          slideHtmlElement.classList.add('swiper-slide');
          slideHtmlElement.classList.add('swiper-thumbnail-slide-image');
          slideHtmlElement.addEventListener('click', () => this.onThumbnailClick(i))
          return slideHtmlElement;
        })

        this.thumbnailSwiper.nativeElement.swiper.removeAllSlides();
        this.thumbnailSwiper.nativeElement.swiper.addSlide(1, thumbnailSlides);
        this.thumbnailSwiper.nativeElement.swiper.activeIndex = 0;
        this.thumbnailSwiper.nativeElement.loop = newSlides.length > 2;
      }
    }
  }

  onThumbnailClick(i: number): void {
    this.mainSwiper.nativeElement.swiper.slideTo(i);
    // this.thumbnailSwiper.nativeElement.swiper.slideTo(i);
    // this.thumbnailSwiper.nativeElement.swiper.slides[this.currentPhoto].classList.remove('active');
    this.currentPhoto = i;
    // this.thumbnailSwiper.nativeElement.swiper.slides[i].classList.add('active');
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

    this.mainSwiper.nativeElement.slidesPerView = 1;
    this.mainSwiper.nativeElement.speed= 500;
    this.mainSwiper.nativeElement.loop = false;
    // this.mainSwiper.nativeElement.spaceBetween = 20;
    // @ts-ignore
    this.mainSwiper.nativeElement.initialize();

    // this.thumbnailSwiper.nativeElement.spaceBetween = 0;
    this.thumbnailSwiper.nativeElement.freeMode = true;
    this.thumbnailSwiper.nativeElement.watchSlidesProgress = true;
    this.thumbnailSwiper.nativeElement.slideActiveClass = 'thumb-slide-active';
    this.thumbnailSwiper.nativeElement.slidesPerView = 4;

    this.thumbnailSwiper.nativeElement.initialize();

  }
}
