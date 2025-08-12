import {
  Component, EventEmitter,
  Input, OnInit, Output
} from '@angular/core';
import {ICategory} from "../../../services/contracts/category";
import {Observable} from "rxjs";

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent implements OnInit {
  @Input() categories$!: Observable<ICategory[]>;
  @Input() currentCategory!: ICategory;
  @Output() chosenCategory: EventEmitter<ICategory> = new EventEmitter<ICategory>();

  constructor() {
  }

  ngOnInit(): void {
    console.log('CategoriesListComponent init!!')
    this.categories$.subscribe(
      {
        // next: value => this.chosenCategory = value[0]
      }
    );
  }

  OnCategoryClick(category: ICategory) {
    this.chosenCategory.emit(category);
    console.log('Child -> category changed to', category);
  }
}
