import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LogisticsTableComponent } from './logistics-table/logistics-table';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.html',
    standalone: true, 
    imports: [LogisticsTableComponent]
})
export class DashboardComponent {
    username: string = '';

    constructor(private router: Router) {
        const email = localStorage.getItem('email');
        this.username = email ? email : 'User';
    }

    logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('email');
        this.router.navigate(['/signin']);
    }
}
