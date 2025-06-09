import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private baseURL = 'https://localhost:7259/api';

    constructor(private http: HttpClient) {}

    createUser(formData: any) {
        return this.http.post(`${this.baseURL}/Users/register`, formData);
    }

    login(credentials: { email: string; password: string }) {
        return this.http.post(`${this.baseURL}/Users/login`, credentials);
    }
}
