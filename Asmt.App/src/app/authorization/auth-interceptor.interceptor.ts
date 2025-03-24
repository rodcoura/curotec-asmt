import { inject, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginService } from '../services/login.service';

@Injectable()
export class AuthInterceptorInterceptor implements HttpInterceptor {
  loginService = inject(LoginService);

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.loginService.getToken();

    if (token) {
      request = request.clone({
          headers: request.headers.set('Authorization', `Bearer ${token.trim()}`)
      });
    }

    return next.handle(request);
  }
}

export const AuthInterceptorInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorInterceptor, multi: true },
];