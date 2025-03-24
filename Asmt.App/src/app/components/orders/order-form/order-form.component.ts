import { Component, OnInit, Output, EventEmitter, Input, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable, of, Subject, takeUntil } from 'rxjs';
import { OrderDto, OrderStatusType } from 'src/app/dtos/order.dto';
import { selectOrderError, selectSelectedOrder } from 'src/app/state/orders/order.selectors';
import { getOrderById, updateOrder, createOrder } from 'src/app/state/orders/order.actions';
import { Router } from '@angular/router';

interface Customer {
  id: number;
  name: string;
}

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss']
})
export class OrderFormComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly fb = inject(FormBuilder);
  private readonly store = inject(Store);

  @Input() orderId: number | null = null;
  @Output() onSuccessOrderSave = new EventEmitter<OrderDto>();

  orderForm: FormGroup;
  error$: Observable<string | null>;
  customers$: Observable<Customer[]> = of([
    { id: 1, name: 'John Doe' },
    { id: 2, name: 'Jane Doe' },
    { id: 3, name: 'Jim Doe' },
    { id: 4, name: 'Jill Doe' },
  ]);

  //TODO: Use ngneat UntilDestroy to unsubscribe
  private ngUnSub = new Subject<void>();

  // Expose OrderStatusType to template
  protected readonly OrderStatusType = OrderStatusType;

  constructor() {
    this.error$ = this.store.select(selectOrderError);
    this.orderForm = this.createForm();
  }

  ngOnInit(): void {
    if (this.orderId) {
      this.store.dispatch(getOrderById({ id: this.orderId }));

      this.store.select(selectSelectedOrder).pipe(takeUntil(this.ngUnSub)).subscribe((order) => {
        if (order) {
          if (order.orderItems && order.orderItems.length > 0) {
            this.orderItems.clear();
            order.orderItems.forEach(() => this.addOrderItem());
          }

          this.orderForm.patchValue(order);
        }
      });
    }
  }

  private createForm(): FormGroup {
    const form = this.fb.group({
      customerId: ['', Validators.required],
      status: [OrderStatusType.Pending, Validators.required],
      pricePreTax: [0, [Validators.required, Validators.min(0), Validators.max(1000000)]],
      tax: [null, [Validators.required, Validators.min(0), Validators.max(100)]],
      totalPrice: [0],
      orderItems: this.fb.array([])
    });

    form.get('orderItems')?.valueChanges.pipe(takeUntil(this.ngUnSub)).subscribe((value) => {
      if(value && value.length > 0) {
        form.get('pricePreTax')?.patchValue(Number(value.reduce((acc: number, item: any) => acc + item.price, 0)));
      }
    });

    form.get('pricePreTax')?.valueChanges.pipe(takeUntil(this.ngUnSub)).subscribe((value) => {
      form.get('totalPrice')?.patchValue(Number(value || 0) + Number((form.get('tax')?.value || 0) / 100));
    });

    form.get('tax')?.valueChanges.pipe(takeUntil(this.ngUnSub)).subscribe((value) => {
      form.get('totalPrice')?.patchValue(Number(form.get('pricePreTax')?.value || 0) + Number((value || 0) / 100));
    });

    form.get('totalPrice')?.disable();

    return form;
  }

  get orderItems(): FormArray {
    return this.orderForm.get('orderItems') as FormArray;
  }

  addOrderItem(): void {
    const orderItem = this.fb.group({
      price: [null, [Validators.required, Validators.min(0)]]
    });

    this.orderItems.push(orderItem);
  }

  removeOrderItem(index: number): void {
    if (this.orderItems.length > 0) {
      this.orderItems.removeAt(index);
    }
  }

  onSubmit(): void {
    if (this.orderForm.valid) {
      const orderData: OrderDto = {
        ...this.orderForm.value,
        id: this.orderId || 0,
        orderItems: this.orderItems.value.map((item: any) => ({
          ...item,
          orderId: this.orderId || 0
        }))
      };

      this.onSuccessOrderSave.emit(orderData);
    }
  }

  clearForm(): void {
    this.orderForm.reset({
      status: OrderStatusType.Pending,
      pricePreTax: 0,
      tax: 0,
      totalPrice: 0
    });
    while (this.orderItems.length) {
      this.orderItems.removeAt(0);
    }
  }

  goBack(): void {
    this.router.navigate(['/orders']);
  }

  ngOnDestroy(): void {
    this.ngUnSub.next();
    this.ngUnSub.complete();
  }
}
