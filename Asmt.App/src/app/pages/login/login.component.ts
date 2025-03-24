import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { login } from '../../state/users/user.actions';
import { MatSnackBar } from '@angular/material/snack-bar';
import { selectLoginError, selectUser } from 'src/app/state/users/user.selectors';
import { Subject, takeUntil } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  private store = inject(Store);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  //TODO: Use ngneat UntilDestroy to unsubscribe
  private ngUnSub = new Subject<void>();

  ngOnInit(): void {
    this.store.select(selectUser).pipe(takeUntil(this.ngUnSub)).subscribe({
        next: (user) => {
          if (user) {
            this.snackBar.open('Login successful', 'Close', {
              duration: 5000
            });
            this.router.navigate(['/orders']);
          }
        },
        error: (e) => {
          this.snackBar.open('Login failed', 'Close', {
            duration: 5000
          });
        }
    });

    this.store.select(selectLoginError).pipe(takeUntil(this.ngUnSub)).subscribe({
      next: (error) => {
        console.log(error);
        if (error) {
          this.snackBar.open(error, 'Close', {
            duration: 5000
          });
        }
      },
      error: (e) => {
        this.snackBar.open('Login failed', 'Close', {
          duration: 5000
        });
      }
  });
  }

  ngOnDestroy(): void {
    this.ngUnSub.next();
    this.ngUnSub.complete();
  }

  /**
   * On login success
   * @param credentials - The credentials of the user
   */
  onLoginSuccess(credentials: {email: string, password: string}): void {
    this.store.dispatch(login({ email: credentials.email, password: credentials.password }));
  }
}
