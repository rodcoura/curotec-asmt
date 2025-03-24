import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { OrdersComponent } from './orders/orders.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { ComponentsModule } from '../components/components.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthInterceptorInterceptorProviders } from '../authorization/auth-interceptor.interceptor';

@NgModule({
  declarations: [
    LoginComponent,
    OrdersComponent,
    OrderDetailComponent,
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    MatButtonModule,
    MatIconModule
  ],
  exports: [
    LoginComponent,
    OrdersComponent,
    OrderDetailComponent,
  ],
  providers: [
    AuthInterceptorInterceptorProviders
  ]
})
export class PagesModule { }
