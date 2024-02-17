import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ImageService} from "../../../services/imageService";
import {IProduct} from "../../../services/contracts/products";
import {CategoryConstants} from "../../../constants/categoryConstants";
import {log} from "@angular-devkit/build-angular/src/builders/ssr-dev-server";

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
  @Input() product!: IProduct;
  @Output() pickedProduct: EventEmitter<IProduct> = new EventEmitter<IProduct>();

  public imageUrl!: string;

  constructor(private imageService: ImageService) {
  }

  ngOnInit(): void {
    this.imageUrl = this.imageService.getImageUrl(this.product.mainPhotoId);
  }

  protected readonly CategoryConstants = CategoryConstants;

  pickProduct() {
    this.pickedProduct.emit(this.product);
  }
}
