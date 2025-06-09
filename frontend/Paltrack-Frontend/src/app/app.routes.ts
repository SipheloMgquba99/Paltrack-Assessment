import { Routes } from '@angular/router';
import { UserComponent } from './user/user';
import { RegistrationComponent } from './user/registration/registration';
import { LoginComponent } from './user/login/login';
import { DashboardComponent } from './dashboard/dashboard';

export const routes: Routes = [
    { path: '', redirectTo: '/signin', pathMatch: 'full' },
    {
        path: '',
        component: UserComponent,
        children: [
            { path: 'signup', component: RegistrationComponent },
            { path: 'signin', component: LoginComponent },
        ],
    },
    { path: 'dashboard', component: DashboardComponent },
];
