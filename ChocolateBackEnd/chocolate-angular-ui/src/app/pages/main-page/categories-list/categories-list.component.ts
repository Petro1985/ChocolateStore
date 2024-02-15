import {
  Component,
  Input, OnInit
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

  constructor() {
  }

  ngOnInit(): void {
    console.log('CategoriesListComponent init!!')
  }
}
