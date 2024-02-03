import {AfterContentChecked, AfterContentInit, AfterViewInit, Component, Input, OnInit} from '@angular/core';
import {ICategory} from "../../services/contracts/category";
import {ImageService} from "../../services/imageService";

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {
  @Input() category!: ICategory;
  imageUrl!: string;

  constructor(public imageService: ImageService) {
  }

  ngOnInit(): void {
    console.log('ngOnInit -> ', this.category);
    this.imageUrl = this.imageService.getImageUrl(this.category.mainPhotoId);
  }
}
