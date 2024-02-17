import {Component, Input} from '@angular/core';
import {IProduct} from "../../../services/contracts/products";

@Component({
  selector: 'app-product-modal',
  templateUrl: './product-modal.component.html',
  styleUrl: './product-modal.component.scss'
})
export class ProductModalComponent {

  @Input() product!: IProduct;
  @Input() closeFunction!: Function;

  OnClose() {
    this.closeFunction();
  }
}
