import {FetchService} from "./fetch-service";
import {BehaviorSubject, Observable, take} from "rxjs";
import {IProduct} from "./contracts/products";
import {Injectable} from "@angular/core";
import { CategoryService } from "./category-service";
import { EmptyProduct } from "../constants/product-constants";

@Injectable({
  providedIn: 'root'
})
export class ProductService
{
  private _products$!: Observable<IProduct[]>;
  private _currentProduct$: BehaviorSubject<IProduct> = new BehaviorSubject<IProduct>(EmptyProduct);

  constructor(private fetchService: FetchService, categoryService: CategoryService) {
    categoryService.getCurrentCategory().subscribe(category => {
      if (category)
      {
        this._products$ = fetchService.GetProducts(category.id);
      }
    });
  }

  public GetProducts() : Observable<IProduct[]>
  {
    return this._products$;
  }

  public setCurrentProduct(productId: string)
  {
    this._products$.pipe(take(1)).subscribe(products => {
      const product = products.find(p => p.id === productId);
      if (product)
      {
        this._currentProduct$.next(product);
      }
    });
  }

  public getCurrentProduct() : Observable<IProduct>
  {
    return this._currentProduct$;
  }
}
