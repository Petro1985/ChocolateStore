import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ICategory } from './contracts/category';
import { StorageService } from './storage-service/storage-service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private currentCategory: BehaviorSubject<ICategory | null> = new BehaviorSubject<ICategory | null>(null);

  constructor(private storageService: StorageService) {
    // Initialize from storage if available
    this.storageService.GetCurrentCategory().subscribe(category => {
      if (category) {
        this.currentCategory.next(category);
      }
    });
  }

  public setCurrentCategory(categoryId: string): void {
    this.storageService.SetCurrentCategory(categoryId);
    
    const category: ICategory = {
      id: categoryId,
      name: '',
      mainPhotoId: ''
    };
    this.currentCategory.next(category);
  }

  public getCurrentCategory(): Observable<ICategory | null> {
    return this.currentCategory.asObservable();
  }
}
