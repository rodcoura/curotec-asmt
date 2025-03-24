import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Subject, takeUntil } from 'rxjs';
import { OrderDto } from 'src/app/dtos/order.dto';
import { createOrder, updateOrder } from 'src/app/state/orders/order.actions';
import { selectOrderSuccess } from 'src/app/state/orders/order.selectors';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit, OnDestroy {
  private readonly route = inject(ActivatedRoute);
  private readonly store = inject(Store);
  private readonly router = inject(Router);
  private snackBar = inject(MatSnackBar);

  //TODO: Use ngneat UntilDestroy to unsubscribe
  private ngUnSub = new Subject<void>();

  orderId: number | null = null;

  constructor() {
    this.orderId = Number(this.route.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {
    this.store.select(selectOrderSuccess).pipe(takeUntil(this.ngUnSub)).subscribe((success) => {
      if (success === true) {
        this.snackBar.open('Order successfully saved', 'Close', {
          duration: 5000
        });
        this.router.navigate(['/orders']);
      } else if (success === false) {
        this.snackBar.open('Order save failed', 'Close', {
          duration: 5000
        });
      }
    });
  }

  handleOrderFormSuccess(order: OrderDto) {
    if (this.orderId) {
      this.store.dispatch(updateOrder({ order: order }));
    } else {
      this.store.dispatch(createOrder({ order: order }));
    }
  }

  ngOnDestroy(): void {
    this.ngUnSub.next();
    this.ngUnSub.complete();
  }
}
