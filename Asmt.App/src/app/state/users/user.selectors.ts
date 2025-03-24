import { createFeatureSelector, createSelector } from '@ngrx/store';
import { UserState } from './users.reducer';

export const selectUserState = createFeatureSelector<UserState>('userState');

export const selectUser = createSelector(
  selectUserState,
  (userState) => userState.user
);

export const selectLoginError = createSelector(
  selectUserState,
  (userState) => userState.error
);
