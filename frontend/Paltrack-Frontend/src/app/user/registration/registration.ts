import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    ReactiveFormsModule,
    Validators,
    AbstractControl,
    ValidatorFn,
} from '@angular/forms';
import { AuthService } from '../../shared/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

type RegistrationFormControls = {
    fullName: FormControl<string | null>;
    email: FormControl<string | null>;
    password: FormControl<string | null>;
    confirmPassword: FormControl<string | null>;
};

@Component({
    selector: 'app-registration',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterLink],
    templateUrl: './registration.html',
    styles: [],
})
export class RegistrationComponent {
    isSubmitted = false;

    form: FormGroup<RegistrationFormControls>;

    constructor(
        private formBuilder: FormBuilder,
        private service: AuthService,
        private toastr: ToastrService,
        private router: Router,
        private route: ActivatedRoute
    ) {
        this.form = this.formBuilder.group(
            {
                fullName: this.formBuilder.control('', Validators.required),
                email: this.formBuilder.control('', [
                    Validators.required,
                    Validators.email,
                ]),
                password: this.formBuilder.control('', [
                    Validators.required,
                    Validators.minLength(6),
                    Validators.pattern(/(?=.*[^a-zA-Z0-9 ])/),
                ]),
                confirmPassword: this.formBuilder.control(''),
            },
            { validators: this.passwordMatchValidator }
        );
    }

    passwordMatchValidator: ValidatorFn = (control: AbstractControl): null => {
        const password = control.get('password');
        const confirmPassword = control.get('confirmPassword');

        if (password && confirmPassword) {
            if (password.value !== confirmPassword.value) {
                confirmPassword.setErrors({ passwordMismatch: true });
            } else if (confirmPassword.hasError('passwordMismatch')) {
                confirmPassword.setErrors(null);
            }
        }

        return null;
    };

    hasDisplayableError(controlName: keyof RegistrationFormControls): boolean {
        const control = this.form.get(controlName);
        return !!(
            control?.invalid &&
            (this.isSubmitted || control.touched || control.dirty)
        );
    }

    onSubmit(): void {
        console.log('onSubmit called');
        this.isSubmitted = true;

        if (this.form.valid) {
            console.log('Form is valid, submitting:', this.form.getRawValue());

            this.service.createUser(this.form.getRawValue()).subscribe({
                next: (res: any) => {
                    console.log('Registration response:', res);
                    if (res.flag) {
                        this.form.reset();
                        this.isSubmitted = false;
                        this.toastr.success(
                            'New user created!',
                            'Registration Successful'
                        );
                        console.log(
                            'User created successfully, navigating to /signin'
                        );

                        this.router
                            .navigate(['/signin'])
                            .then((success) => {
                                console.log('Navigation success:', success);
                            })
                            .catch((err) => {
                                console.error('Navigation error:', err);
                            });
                    } else {
                        console.log(
                            'Registration response did not succeed:',
                            res
                        );
                    }
                },
                error: (err) => {
                    console.log('Registration error:', err);
                    if (err.error?.errors) {
                        err.error.errors.forEach((x: any) => {
                            console.log('Error code:', x.code);
                            switch (x.code) {
                                case 'DuplicateEmail':
                                    this.toastr.error(
                                        'Email is already taken.',
                                        'Registration Failed'
                                    );
                                    break;
                                default:
                                    this.toastr.error('Registration Failed');
                                    console.error(x);
                                    break;
                            }
                        });
                    } else {
                        console.error('Unexpected error:', err);
                    }
                },
            });
        } else {
            console.log('Form is invalid:', this.form.errors);
        }
    }
}
