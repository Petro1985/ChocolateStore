import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ImageService} from "../../../services/image-service";
import {IProduct} from "../../../services/contracts/products";
import {CategoryConstants} from "../../../constants/categoryConstants";
import { ModalService } from '../../../_modal';
import { ProductService } from '../../../services/product-service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
  @Input() product!: IProduct;
  @Output() pickedProduct: EventEmitter<IProduct> = new EventEmitter<IProduct>();

  public imageUrl!: string;

  constructor(private imageService: ImageService, private modalService: ModalService, private productService: ProductService) {
  }

  ngOnInit(): void {
    this.imageUrl = this.imageService.getImageUrl(this.product.mainPhotoId);
  }

  protected readonly CategoryConstants = CategoryConstants;

  pickProduct() {
    this.productService.setCurrentProduct(this.product.id);
    this.modalService.open('product-modal');
  }
}
