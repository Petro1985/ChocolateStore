import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, take } from 'rxjs';
import { ICategory } from './contracts/category';
import { StorageService } from './storage-service/storage-service';
import { FetchService } from './fetch-service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private allCategories$!: Observable<ICategory[]>;
  private currentCategory: BehaviorSubject<ICategory | null> = new BehaviorSubject<ICategory | null>(null);

  constructor(private storageService: StorageService, private fetchService: FetchService) {
    this.allCategories$ = this.fetchService.GetCategories();
}

  public getAllCategories(): Observable<ICategory[]> {
    return this.allCategories$;
  }

  public setCurrentCategory(categoryId: string): void {
    this.allCategories$.pipe(take(1)).subscribe(categories => {
      const found = categories.find(c => c.id === categoryId);
      if (found) {
        this.currentCategory.next(found);
      } else {
        // Fallback to a minimal category shape if not found
        this.currentCategory.next({ id: categoryId, name: '', mainPhotoId: '' });
      }
    });
  }

  public getCurrentCategory(): Observable<ICategory | null> {
    return this.currentCategory;
  }
}
