import {
  AfterViewChecked,
  Component,
  Input, OnChanges,
  SimpleChanges,
  ViewChild
} from '@angular/core';
import {BehaviorSubject, Observable, switchMap} from "rxjs";
import {ICategory} from "../../../services/contracts/category";
import {FetchService} from "../../../services/fetch-service";
import {IProduct} from "../../../services/contracts/products";
import {CategoriesListComponent} from "../categories-list/categories-list.component";
import {ModalService} from "../../../_modal";
import {EmptyProduct} from "../../../constants/product-constants";

@Component({
  selector: 'app-main-part',
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent implements AfterViewChecked, OnChanges {
  @ViewChild(CategoriesListComponent) categoriesList!: CategoriesListComponent;

  public categories$: BehaviorSubject<ICategory[]> = new BehaviorSubject<ICategory[]>([]);
  public products$: Observable<IProduct[]> = new BehaviorSubject<IProduct[]>([]);
  @Input() currentCategory!: ICategory;
  @Input() pickedProduct: IProduct = EmptyProduct;
  // public currentCategory!: ICategory;

  constructor(private fetchService: FetchService, public modalService: ModalService) {
    this.products$ = this.fetchService.GetCategories()
      .pipe(switchMap(x => {
        this.categories$.next(x);
        this.currentCategory = x[0];
        return this.fetchService.GetProducts(x[0].id);
      }));

  }

  ngOnChanges(changes: SimpleChanges): void {
    const q = changes["currentCategory"]

  }

  ngAfterViewChecked() {
    // viewChild is updated after the view has been checked
    // if (this.currentCategory === this.categoriesList.chosenCategory) {
    //   console.log('AfterViewChecked (no change)');
    // } else {
    //   this.currentCategory = this.categoriesList.chosenCategory;
    //   console.log('AfterViewChecked');
    // }
  }

  currentCategoryChanged($event: ICategory) {
    this.currentCategory = $event;
    this.products$ = this.fetchService.GetProducts($event.id);
    console.log('Parent -> Current category set to ', $event);
  }

  pickedProductChanged($event: IProduct) {
    this.pickedProduct = $event;
    this.modalService.open('product-modal')
  }

  closeProductModal = () =>
  {
    this.modalService.close('product-modal');
  }
}
