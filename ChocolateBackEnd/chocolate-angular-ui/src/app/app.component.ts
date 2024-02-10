import {Component, OnInit} from '@angular/core';
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {CommonModule} from "@angular/common";
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
