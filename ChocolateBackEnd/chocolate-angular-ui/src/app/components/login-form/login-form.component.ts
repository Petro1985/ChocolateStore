import {Component} from '@angular/core';
import {ModalService} from "../../_modal";
import {NgForm} from "@angular/forms";
import {UserService} from "../../services/user-service";
import {IUserLogin} from "../../services/contracts/user-login";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss'
})
export class LoginFormComponent {
  constructor(private modalService: ModalService, private userService: UserService) {
  }

  onSignupClick() {
    this.modalService.open('signupModal')
  }

  onSubmit(loginForm: NgForm) {
    // const user: IUserLogin = {
    //   userName: loginForm.value.email,
    //   password: loginForm.value.password,
    //   remember: loginForm.value.remember
    // }
    this.userService.Login(loginForm.value.email, loginForm.value.password, !!loginForm.value.remember)
      .subscribe({
        next: _ =>
        {
          this.modalService.close('loginModal');
        },
        error: (err: string) =>
        {
          window.alert(`Ошибка: ${err}`);
        }
      });
  }
}
