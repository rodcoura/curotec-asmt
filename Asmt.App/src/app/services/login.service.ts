import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { UserDto } from '../dtos/user.dto';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5094/api/token';
  private readonly localStorageService = inject(LocalStorageService);

  /**
   * Logs in a user with the given credentials.
   * @param credentials The login credentials.
   * @returns A promise that resolves to the user data.
   */
  login(credentials: { email: string; password: string }): Observable<UserDto> {
    return this.http.post<UserDto>(`${this.apiUrl}`, credentials).pipe(
      tap((user: UserDto) => {
        user.token && this.storeToken(user.token);
      })
    );
  }

  /**
   * Stores the user's token in localStorage.
   * @param token The user's token.
   */
  storeToken(token: string): void {
    this.localStorageService.setItem('token', token);
  }

  /**
   * Logs out the current user.
   */
  logout(): void {
    this.localStorageService.removeItem('token');
  }

  /**
   * Checks if the user is logged in.
   * @returns True if the user is logged in, false otherwise.
   */
  isLoggedIn(): boolean {
    return this.localStorageService.getItem('token') !== null;
  }

  /**
   * Retrieves the user's token from localStorage.
   * @returns The user's token or null if no token is found.
   */
  getToken(): string | null {
    return this.localStorageService.getItem('token');
  }
}