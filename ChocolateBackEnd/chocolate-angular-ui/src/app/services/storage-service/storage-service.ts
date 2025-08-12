import {Injectable} from "@angular/core";
import {localstorageConstants} from "../../constants/localstorage-constants";
import {BehaviorSubject, Observable} from "rxjs";
import {ICategory} from "../contracts/category";
import {FetchService} from "../fetch-service";

const emptyCategory: ICategory = {id:'', name: '', mainPhotoId: ''}

@Injectable()
export class StorageService
{
  constructor(private fetchService: FetchService) {
  }

}
