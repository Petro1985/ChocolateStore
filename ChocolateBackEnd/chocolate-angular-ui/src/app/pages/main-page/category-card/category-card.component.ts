import { Component, Input, OnInit} from '@angular/core';
import { ICategory } from "../../../services/contracts/category";
import { ImageService } from "../../../services/image-service";
import { FetchService } from "../../../services/fetch-service";
import { CategoryConstants } from "../../../constants/categoryConstants";

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {
  @Input() category!: ICategory;
  @Input() isSelected: boolean = false;
  public imageUrl!: string;
  categoryLink!: string;

  constructor(private imageService: ImageService, private fetchService: FetchService) {
  }

  ngOnInit(): void {
    console.log()
    this.imageUrl = this.imageService.getImageUrl(this.category.mainPhotoId);
    this.categoryLink = this.fetchService.GetCategoryLink(this.category.id);
  }
}
