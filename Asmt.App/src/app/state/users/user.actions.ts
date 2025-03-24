import { createAction, props } from '@ngrx/store';
import { UserDto } from 'src/app/dtos/user.dto';

export const loginHydrate = createAction(
  '[User] Login Hydrate',
  props<{ user: UserDto }>()
);

export const login = createAction(
  '[User] Login',
  props<{ email: string; password: string }>()
);

export const logoff = createAction(
  '[User] Logoff'
);

export const loginSuccess = createAction(
  '[User] Login Success',
  props<{ user: UserDto }>()
);

export const loginFailure = createAction(
  '[User] Login Failure',
  props<{ error: string }>()
);





