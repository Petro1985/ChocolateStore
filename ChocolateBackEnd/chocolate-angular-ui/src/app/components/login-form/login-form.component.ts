import {Component, ElementRef, Input} from '@angular/core';
import {ModalService} from "../../_modal";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss'
})
export class LoginFormComponent {
  constructor(private modalService: ModalService) {
  }

  onSignupClick() {
    this.modalService.open('signupModal')
  }
}
