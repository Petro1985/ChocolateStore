import {
  AfterViewInit, ChangeDetectorRef,
  Component,
  ElementRef,
  HostListener,
  ViewChild
} from '@angular/core';
import {FetchService} from "../../../services/fetch-service";
import {ICategory} from "../../../services/contracts/category";
import {Observable} from "rxjs";
import {CategoryConstants} from "../../../constants/categoryConstants";

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent implements AfterViewInit {
  @ViewChild('categoriesContainer') categoriesContainer!: ElementRef;


  public categories$: Observable<ICategory[]>;
  slideConfig: any;

  constructor(private fetchService: FetchService, private cd:ChangeDetectorRef) {
    this.categories$ = this.fetchService.GetCategories();
  }

  ngAfterViewInit(): void {
    this.UpdateConfig(null);
    this.cd.detectChanges();
  }

  @HostListener('window:resize', ['$event'])
  UpdateConfig(_: any) {
    console.log(this.categoriesContainer.nativeElement.offsetWidth);
    const cardsOnPage = Math.floor(this.categoriesContainer.nativeElement.offsetWidth / CategoryConstants.CardWidth);

    this.slideConfig = {
      dots: true,
      infinite: true,
      speed: 500,
      slidesToShow: cardsOnPage,
      slidesToScroll: cardsOnPage,
    };
  }

  // {
  //   console.log(categoryId);
  // };

  public ScrollTo: (categoryId: string) => void = categoryId =>
  {
    const el = this.categoriesContainer.nativeElement.querySelector(`[id="${categoryId}"]`);
    el.scrollIntoView();
  }

  slickInit($event: { event: any; slick: any }) {

  }

  breakpoint($event: { event: any; slick: any; breakpoint: any }) {

  }

  afterChange($event: { event: any; slick: any; currentSlide: number; first: boolean; last: boolean }) {

  }

  beforeChange($event: { event: any; slick: any; currentSlide: number; nextSlide: number }) {

  }
}
