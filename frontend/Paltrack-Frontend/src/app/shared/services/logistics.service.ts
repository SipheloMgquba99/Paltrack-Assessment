import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class LogisticsService {
    private baseURL = 'https://localhost:7259/api/LogisticPartners';

    constructor(private http: HttpClient) {}

    getAllLogisticsPartners(): Observable<any> {
        return this.http
            .get<any>(`${this.baseURL}/logistics-partners`)
            .pipe(tap((response) => console.log('API Response:', response)));
    }
}
