import {Component, Input, OnInit} from '@angular/core';
import {ICategory} from "../../../services/contracts/category";
import {ImageService} from "../../../services/imageService";
import {FetchService} from "../../../services/fetch-service";

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {
  @Input() category!: ICategory;
  public imageUrl!: string;
  categoryLink!: string;

  constructor(private imageService: ImageService, private fetchService: FetchService) {
  }

  ngOnInit(): void {
    this.imageUrl = this.imageService.getImageUrl(this.category.mainPhotoId);
    this.categoryLink = this.fetchService.GetCategoryLink(this.category.id);
  }
}
