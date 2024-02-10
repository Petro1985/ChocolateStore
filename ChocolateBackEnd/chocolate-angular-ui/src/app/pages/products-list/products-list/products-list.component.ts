import {Component, Input} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {IProduct} from "../../../services/contracts/products";
import {Observable} from "rxjs";
import {FetchService} from "../../../services/fetch-service";
import {ICategory} from "../../../services/contracts/category";
import {localstorageConstants} from "../../../constants/localstorage-constants";
import {StorageService} from "../../../services/storage-service/storage-service";

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent {



  public products!: Observable<IProduct[]>;
  public categories: Observable<ICategory[]>;

  private readonly categoryId!: string;

  constructor(private route:ActivatedRoute, private fetchService:FetchService, private storageService: StorageService){
    this.categoryId = this.route.snapshot.params["id"];
    storageService.SetCurrentCategory(this.categoryId)

    this.products = this.fetchService.GetProducts(this.categoryId);
    this.categories = this.fetchService.GetCategories();
  }
}
