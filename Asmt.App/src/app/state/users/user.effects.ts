import { inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { LoginService } from 'src/app/services/login.service';
import * as loginActions from './user.actions';
import { catchError, exhaustMap, map, of } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UserDto } from 'src/app/dtos/user.dto';

export const loginUser = createEffect(
  (actions$ = inject(Actions), loginService = inject(LoginService)) => {
    return actions$.pipe(
      ofType(loginActions.login),
      exhaustMap((action) =>
        loginService.login({ email: action.email, password: action.password }).pipe(
          map((user: UserDto) => {
            return loginActions.loginSuccess({ user });
          }),
          catchError((httpErrorResponse: HttpErrorResponse) =>
            of(loginActions.loginFailure({ error: httpErrorResponse.error }))
          )
        )
      )
    );
  },
  { functional: true }
);
