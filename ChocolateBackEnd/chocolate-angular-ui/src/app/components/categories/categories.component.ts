import { Component } from '@angular/core';
import {FetchService} from "../../services/fetch-service";
import {ICategory} from "../../services/contracts/category";
import {Observable} from "rxjs";

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent {

  public categories: Observable<ICategory[]>;

  constructor(private fetchService: FetchService) {
    this.categories = this.fetchService.GetCategories();
  }
}
