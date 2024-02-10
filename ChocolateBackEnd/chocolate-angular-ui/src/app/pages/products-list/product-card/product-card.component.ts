import {Component, Input, OnInit} from '@angular/core';
import {ImageService} from "../../../services/imageService";
import {FetchService} from "../../../services/fetch-service";
import {IProduct} from "../../../services/contracts/products";

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
  @Input() product!: IProduct;
  public imageUrl!: string;

  constructor(private imageService: ImageService, private fetchService: FetchService) {
  }

  ngOnInit(): void {
    this.imageUrl = this.imageService.getImageUrl(this.product.mainPhotoId);
    console.log('image -> ', this.imageUrl);
  }
}
