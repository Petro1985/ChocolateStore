import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from '../../../services/contracts/products';
import { ProductService } from '../../../services/product-service';
import { ICategory } from '../../../services/contracts/category';
import { CategoryService } from '../../../services/category-service';
import { ModalService } from '../../../_modal';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  public products$: Observable<IProduct[] | null>;
  public currentCategory$: Observable<ICategory | null>;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private modalService: ModalService
  ) {
    this.products$ = this.productService.GetProducts();
    this.currentCategory$ = this.categoryService.getCurrentCategory();
  }

  ngOnInit(): void {
    this.currentCategory$.subscribe(category => {
      this.products$ = this.productService.GetProducts();
    });
  }

  onProductClick(product: IProduct): void {
    this.modalService.open('product-modal');
  }
}

