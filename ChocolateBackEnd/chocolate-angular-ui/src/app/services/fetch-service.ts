import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable, shareReplay} from "rxjs";
import {ICategory} from "./contracts/category";
import {Injectable} from "@angular/core";
import {IProduct} from "./contracts/products";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class FetchService {
  private _serverUrl: string = environment.serverApiUrl;

  private _getCategoriesRequest: Observable<ICategory[]> | null = null;
  // TODO: Вынести кэширование в отдельный сервис.
  private readonly _categoriesMap: Map<string, Observable<IProduct[]>> = new Map<string, Observable<IProduct[]>>();
  private readonly _categoryMap: Map<string, Observable<ICategory>> = new Map<string, Observable<ICategory>>();

  constructor(private client: HttpClient) {
  }

  public GetCategoryLink(categoryId: string) {
    return `${environment.serverApiUrl}Categories/${categoryId}`;
  }

  public GetCategories(): Observable<ICategory[]> {
    if (!this._getCategoriesRequest)
    {
      const path = this._serverUrl + 'categories';
      this._getCategoriesRequest = this.client.get<ICategory[]>(path)
        .pipe(shareReplay());
    }
    return this._getCategoriesRequest;
  }

  GetProducts(categoryId: string): Observable<IProduct[]> {
    const existObservable = this._categoriesMap.get(categoryId);

    if (!existObservable) {
      const path = `${this._serverUrl}Products?categoryId=${categoryId}`;
      const newObservable = this.client.get<IProduct[]>(path)
        .pipe(shareReplay());

      this._categoriesMap.set(categoryId, newObservable);
      return newObservable;
    }

    return existObservable;
  }

  GetCategory(categoryId: string): Observable<ICategory> {
    const existObservable = this._categoryMap.get(categoryId);

    if (!existObservable) {
      const path = `${this._serverUrl}Categories/${categoryId}`;
      const newObservable = this.client.get<ICategory>(path)
        .pipe(shareReplay());

      this._categoryMap.set(categoryId, newObservable);
      return newObservable;
    }

    return existObservable;
  }
}
