import {
  AfterViewChecked,
  Component,
  Input, OnChanges,
  SimpleChanges,
  ViewChild
} from '@angular/core';
import {BehaviorSubject, Observable, switchMap, finalize} from "rxjs";
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
export class MainPageComponent implements OnChanges {
  @ViewChild(CategoriesListComponent) categoriesList!: CategoriesListComponent;

  public categories$: BehaviorSubject<ICategory[]> = new BehaviorSubject<ICategory[]>([]);
  public products$: Observable<IProduct[]> = new BehaviorSubject<IProduct[]>([]);
  @Input() currentCategory!: ICategory;
  @Input() pickedProduct: IProduct = EmptyProduct;
  public isLoadingProducts: boolean = false;
  public isLoadingCategories: boolean = false;
  // public currentCategory!: ICategory;

  constructor(private fetchService: FetchService, public modalService: ModalService) {
    this.isLoadingCategories = true;
    this.isLoadingProducts = true;
    
    // Load categories first
    const categoriesObservable = this.fetchService.GetCategories();
    categoriesObservable.subscribe({
      next: (categories) => {
        this.categories$.next(categories);
        this.currentCategory = categories[0];
        this.isLoadingCategories = false;
        
        // Then load products for the first category
        const productsObservable = this.fetchService.GetProducts(categories[0].id);
        this.products$ = productsObservable;
        
        productsObservable.subscribe({
          next: () => {
            this.isLoadingProducts = false;
          },
          error: () => {
            this.isLoadingProducts = false;
          }
        });
      },
      error: () => {
        this.isLoadingCategories = false;
        this.isLoadingProducts = false;
      }
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    const q = changes["currentCategory"]

  }

  currentCategoryChanged($event: ICategory) {
    this.currentCategory = $event;
    this.isLoadingProducts = true;

    const productsObservable = this.fetchService.GetProducts($event.id);
    this.products$ = productsObservable;

    // Subscribe to handle loading state
    productsObservable.subscribe({
      next: () => {
        this.isLoadingProducts = false;
      },
      error: () => {
        this.isLoadingProducts = false;
      }
    });

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
