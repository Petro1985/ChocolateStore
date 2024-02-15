import {ChangeDetectorRef, Component, ElementRef, HostListener, ViewChild} from '@angular/core';
import {BehaviorSubject, Observable, switchMap, tap} from "rxjs";
import {ICategory} from "../../../services/contracts/category";
import {FetchService} from "../../../services/fetch-service";
import {IProduct} from "../../../services/contracts/products";

@Component({
  selector: 'app-main-part',
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {
  public categories$: BehaviorSubject<ICategory[]> = new BehaviorSubject<ICategory[]>([]);
  public products$: Observable<IProduct[]> = new BehaviorSubject<IProduct[]>([]);
  public currentCategory!: ICategory;

  constructor(private fetchService: FetchService) {
    this.products$ = this.fetchService.GetCategories()
      .pipe(switchMap(x => {
        this.categories$.next(x);
        this.currentCategory = x[0];
        return this.fetchService.GetProducts(x[0].id);
      }));

  }
}
