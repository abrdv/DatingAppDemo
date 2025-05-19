import { Component, output, inject, OnInit } from '@angular/core';
import {  AbstractControl, Form, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf } from '@angular/common';
import { TextInputComponent } from '../_forms/text-input/text-input.component';
import { DatePickerComponent } from '../_forms/date-picker/date-picker.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, TextInputComponent, DatePickerComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  cancelRegister = output<boolean>();
  registerForm: FormGroup = new FormGroup({});
  maxDate=new Date();
  validationErrors: string[] = [];

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }
  initializeForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    });
    this.registerForm.controls['password'].valueChanges.subscribe(() => {
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity();
    });
  }
matchValues(matchTo: string) : ValidatorFn{
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { isMatching: true };
    }
  }
  register() {
    const dob = this.getDateOnly(this.registerForm.get('dateOfBirth')?.value);
    this.registerForm.patchValue({dateOfBirth: dob});
    // Call the register method from the account service and subscribe to the response
    this.accountService.register(this.registerForm.value).subscribe(
      {
      // On success, log the response and call the cancel method
      next: _ => this.router.navigateByUrl('/members'),
      // On error, display an error message using the toastr service
      error: error => this.validationErrors = error,
      }
    );
    // Log the form value to the console
  console.log(this.registerForm.value);
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
  private getDateOnly(date: string | undefined) {
    if (!date)return;
    return new Date(date).toISOString().slice(0, 10);
  }
}
