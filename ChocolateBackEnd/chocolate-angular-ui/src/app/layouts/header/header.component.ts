import { Component } from '@angular/core';
import {FetchService} from "../../services/fetch-service";
import {TranslateService} from "@ngx-translate/core";
import {localstorageConstants} from "../../constants/localstorage-constants";
import {Router} from "@angular/router";
import {StorageService} from "../../services/storage-service/storage-service";
import {Observable} from "rxjs";
import {ICategory} from "../../services/contracts/category";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  public category$: Observable<ICategory>;


  constructor(private fetchService: FetchService, private translateService: TranslateService, private router: Router, private storageService: StorageService) {
    this.category$ = this.storageService.GetCurrentCategory();
  }

  onLanguageChange(event: any) {
    localStorage.setItem(localstorageConstants.language, event.target.value);
    this.translateService.use(event.target.value)
  }
}
