import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
    FormBuilder,
    FormGroup,
    Validators,
    ReactiveFormsModule,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterLink],
    templateUrl: './login.html',
    styles: [],
})
export class LoginComponent {
    isSubmitted = false;
    form: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private service: AuthService,
        private router: Router,
        private toastr: ToastrService
    ) {
        this.form = this.formBuilder.group({
            email: ['', Validators.required],
            password: ['', Validators.required],
        });
    }

    hasDisplayableError(controlName: string): boolean {
        const control = this.form.get(controlName);
        return !!(
            control?.invalid &&
            (this.isSubmitted || control.touched || control.dirty)
        );
    }

    onSubmit(): void {
        this.isSubmitted = true;
        if (this.form.valid) {
            console.log('Form submitted:', this.form.getRawValue());
            this.service.login(this.form.getRawValue()).subscribe({
                next: (res: any) => {
                    localStorage.setItem('token', res.token);
                     localStorage.setItem('email', this.form.get('email')?.value);
                    this.router.navigateByUrl('/dashboard');
                },
                error: (err) => {
                    if (err.status === 400 || err.status === 401) {
                        this.toastr.error(
                            err.error || 'Invalid credentials',
                            'Login failed'
                        );
                    } else {
                        console.error('Error during login:', err);
                        this.toastr.error(
                            'Something went wrong. Try again.',
                            'Login failed'
                        );
                    }
                },
            });
        }
    }
}
