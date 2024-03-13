import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { createMask, InputmaskOptions } from "@ngneat/input-mask";

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrl: './signup-form.component.scss',
})
export class SignupFormComponent {
  public phone!: string;
  phoneInputMask: InputmaskOptions<any> | null | undefined;

  constructor() {
    this.phoneInputMask = createMask({
      mask: '+7 (999) 999-99-99',
      keepStatic: true,
    });
  }

  onSubmit(signupForm: NgForm) {

  }
}
