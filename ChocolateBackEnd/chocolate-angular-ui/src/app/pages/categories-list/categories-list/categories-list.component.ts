import { Component } from '@angular/core';
import {FetchService} from "../../../services/fetch-service";
import {ICategory} from "../../../services/contracts/category";
import {Observable} from "rxjs";
@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent {

  public categories: Observable<ICategory[]>;
  constructor(private fetchService: FetchService) {
    this.categories = this.fetchService.GetCategories();
  }
}
