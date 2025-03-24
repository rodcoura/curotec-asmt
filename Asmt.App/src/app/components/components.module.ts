import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card'
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StateModule } from '../state/state.module';
import { LoginFormComponent } from './login/login-form/login-form.component';
import { AppRoutingModule } from '../app-routing.module';
import { AuthInterceptorInterceptorProviders } from '../authorization/auth-interceptor.interceptor';
import { OrderFormComponent } from './orders/order-form/order-form.component';
import { OrderListComponent } from './orders/order-list/order-list.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
@NgModule({
  declarations: [
    LoginFormComponent,
    OrderFormComponent,
    OrderListComponent
  ],
  imports: [
    CommonModule,
    StateModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatSelectModule,
    AppRoutingModule,
  ],
  providers: [
    AuthInterceptorInterceptorProviders
  ],
  exports: [
    LoginFormComponent,
    OrderFormComponent,
    OrderListComponent
  ]
})
export class ComponentsModule { }
