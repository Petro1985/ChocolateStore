import { Component } from '@angular/core';
import {TranslateService} from "@ngx-translate/core";
import {localstorageConstants} from "./constants/localstorage-constants";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private translationService: TranslateService) {
    this.translationService.setDefaultLang('ru');
    const currentLang = localStorage.getItem(localstorageConstants.language) || 'ru';
    this.translationService.use(currentLang);
  }
}
