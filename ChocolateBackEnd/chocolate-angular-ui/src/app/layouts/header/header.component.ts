import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {TranslateService} from "@ngx-translate/core";
import {localstorageConstants} from "../../constants/localstorage-constants";
import {StorageService} from "../../services/storage-service/storage-service";
import {Observable} from "rxjs";
import {ICategory} from "../../services/contracts/category";
import {ModalComponent} from "../../_modal/modal.component";
import {ModalService} from "../../_modal";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements AfterViewInit {
  public category$: Observable<ICategory>;
  @ViewChild('loginModal') loginModal?: ModalComponent;
  @ViewChild('signupModal') signupModal?: ModalComponent;

  constructor(private translateService: TranslateService,
              private storageService: StorageService,
              private modalService: ModalService,
              private authService: AuthService) {
    this.category$ = this.storageService.GetCurrentCategory();
  }

  ngAfterViewInit(): void {
    this.modalService.add(this.loginModal);
    this.modalService.add(this.signupModal);
  }

  onLanguageChange(event: any) {
    localStorage.setItem(localstorageConstants.language, event.target.value);
    this.translateService.use(event.target.value)
  }

  async onLoginClick() {

    const user = (await this.authService.logIn()).data;

    // this.modalService.open('loginModal');
  }
}
