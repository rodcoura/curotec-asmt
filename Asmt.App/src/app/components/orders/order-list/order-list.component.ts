import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { MatSnackBar } from '@angular/material/snack-bar';
import { map, Observable } from 'rxjs';
import { OrderDto, OrderStatusType } from 'src/app/dtos/order.dto';
import {
  selectOrders,
  selectOrderError
} from 'src/app/state/orders/order.selectors';
import {
  getOrders,
  deleteOrder
} from 'src/app/state/orders/order.actions';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {
  orders$: Observable<OrderDto[]> = this.store.select(selectOrders);
  error$: Observable<string | null> = this.store.select(selectOrderError);
  hasError$: Observable<boolean> = this.error$.pipe(map((error) => !!error));

  displayedColumns: string[] = [
    'id',
    'pricePreTax',
    'tax',
    'totalPrice',
    'status',
    'customerName',
    'orderItems',
    'actions'
  ];

  constructor(
    private store: Store,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.store.dispatch(getOrders());
    this.error$.subscribe(error => {
      if (error) {
        this.snackBar.open(error, 'Close', {
          duration: 5000,
          horizontalPosition: 'end',
          verticalPosition: 'top'
        });
      }
    });
  }

  getStatusString(status: OrderStatusType): string {
    return OrderStatusType[status];
  }

  onAddOrder(): void {
    this.router.navigate(['/orders/new']);
  }

  onEditOrder(id: number): void {
    this.router.navigate([`/orders/${id}`]);
  }

  onDeleteOrder(id: number): void {
    if (confirm('Are you sure you want to delete this order?')) {
      this.store.dispatch(deleteOrder({ id }));
      this.snackBar.open('Order deleted successfully', 'Close', {
        duration: 3000,
        horizontalPosition: 'end',
        verticalPosition: 'top'
      });
    }
  }
}
