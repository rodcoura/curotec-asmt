import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { userReducer } from './users/users.reducer';
import * as userEffects from './users/user.effects';
import { orderReducer } from './orders/order.reducer';
import * as orderEffects from './orders/order.effects';
import { AuthInterceptorInterceptorProviders } from '../authorization/auth-interceptor.interceptor';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StoreModule.forRoot({
      userState: userReducer,
      orderState: orderReducer
    }, {}),
    EffectsModule.forRoot([
      userEffects,
      orderEffects
    ])
  ],
  providers: [
    AuthInterceptorInterceptorProviders
  ],
})
export class StateModule { }
