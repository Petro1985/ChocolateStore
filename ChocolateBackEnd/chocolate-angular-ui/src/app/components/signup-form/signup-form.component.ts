import {Component, Input} from '@angular/core';
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrl: './signup-form.component.scss'
})
export class SignupFormComponent {

  public phone!: string;

  phoneNumberChange() {
    this.phone.replace(/^\+7\((\d{3})\)(\d{3})-(\d{2})-(\d{2})$/, "+7($1)$2-$3-$4");
  }

  onSubmit(signupForm: NgForm) {

  }
}
