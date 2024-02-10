import {Injectable} from "@angular/core";
import {localstorageConstants} from "../../constants/localstorage-constants";
import {BehaviorSubject, Observable} from "rxjs";
import {ICategory} from "../contracts/category";
import {FetchService} from "../fetch-service";

const emptyCategory: ICategory = {id:'', name: '', mainPhotoId: ''}

@Injectable()
export class StorageService
{
  private currentCategory: BehaviorSubject<ICategory> = new BehaviorSubject<ICategory>(emptyCategory);

  constructor(private fetchService: FetchService) {
  }

  SetCurrentCategory(categoryId: string)
  {
    this.fetchService.GetCategory(categoryId)
      .subscribe(
        {
          next: value =>
          {
            this.currentCategory.next(value);
            localStorage.setItem(localstorageConstants.currentCategoryId, JSON.stringify(value));
          }
        }
      );
  }

  GetCurrentCategory(): Observable<ICategory>
  {
    return this.currentCategory;
  }
}
