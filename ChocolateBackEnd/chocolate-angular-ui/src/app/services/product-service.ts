import {FetchService} from "./fetch-service";
import {BehaviorSubject} from "rxjs";
import {IProduct} from "./contracts/products";
import {ActivatedRoute} from "@angular/router";

export class ProductService
{
  private _products: BehaviorSubject<IProduct[] | null> = new BehaviorSubject<IProduct[] | null>(null);
  private _categoryId: string = '';

  constructor(private fetchService: FetchService, private route:ActivatedRoute) {

    // this.fetchService.GetProducts()
  }

  public GetProducts()
  {
    if (this._products.value === null)
    {
      const categoryId = this.route.snapshot.params["id"];
      this.fetchService.GetProducts(categoryId)
        .subscribe(
          {
          next: value =>
          {
            this._products.next(value);
          }
        })
    }
    return this._products;
  }


}
