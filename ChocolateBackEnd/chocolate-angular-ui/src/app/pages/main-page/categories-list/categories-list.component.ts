import { Component, Input, OnInit } from '@angular/core';
import {ICategory} from "../../../services/contracts/category";
import {Observable, take} from "rxjs";
import { CategoryService } from '../../../services/category-service';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent implements OnInit {
  public currentCategory$!: Observable<ICategory| null>;
  public categories$!: Observable<ICategory[]>;

  constructor(private categoryService: CategoryService) {
    this.categories$ = this.categoryService.getAllCategories();
    this.currentCategory$ = this.categoryService.getCurrentCategory();
    this.categories$.pipe(take(1)).subscribe(categories => {
      this.categoryService.setCurrentCategory(categories[0].id);
    });
  }

  ngOnInit(): void {
  }

  OnCategoryClick(category: ICategory) {
    this.categoryService.setCurrentCategory(category.id);
    console.log('Child -> category changed to', category);
  }
}
