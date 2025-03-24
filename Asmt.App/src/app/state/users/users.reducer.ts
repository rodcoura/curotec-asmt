import { createReducer, on } from '@ngrx/store';
import { UserDto } from 'src/app/dtos/user.dto';
import * as loginActions from './user.actions';

export type UserState = {
  user: UserDto | null;
  error: string | null;
};

export const initialState: UserState = {
  user: null,
  error: null
};

export const userReducer = createReducer(
  initialState,
  on(loginActions.logoff, (state) => ({
    ...state,
    user: null,
    error: null
  })),
  on(loginActions.login, (state) => ({
    ...state,
    user: null,
    error: null
  })),
  on(loginActions.loginHydrate, (state, action) => ({
    ...state,
    user: action.user,
    error: null
  })),
  on(loginActions.loginSuccess, (state, action) => ({
    ...state,
    user: action.user,
    error: null
  })),
  on(loginActions.loginFailure, (state, action) => ({
    ...state,
    user: null,
    error: action.error || 'Login or passowrd are invalid'
  })),
);