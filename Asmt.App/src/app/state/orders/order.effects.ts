import { inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as orderActions from './order.actions';
import { catchError, exhaustMap, map, of } from 'rxjs';
import { OrderService } from 'src/app/services/order.service';
import { HttpErrorResponse } from '@angular/common/http';

export const getOrders = createEffect(
  (actions$ = inject(Actions), orderService = inject(OrderService)) => {
    return actions$.pipe(
      ofType(orderActions.getOrders),
      exhaustMap(() =>
        orderService.getOrders().pipe(
          map((orders) => orderActions.getOrdersSuccess({ orders })),
            catchError((httpErrorResponse: HttpErrorResponse) =>
            of(orderActions.getOrdersFailure({ error: httpErrorResponse.error }))
          )
        )
      )
    );
  },
  { functional: true }
);

export const createOrder = createEffect(
  (actions$ = inject(Actions), orderService = inject(OrderService)) => {
    return actions$.pipe(
      ofType(orderActions.createOrder),
      exhaustMap((action) =>
        orderService.createOrder(action.order).pipe(
          map((order) => orderActions.createOrderSuccess({ order })),
          catchError((httpErrorResponse: HttpErrorResponse) =>
            of(orderActions.createOrderFailure({ error: httpErrorResponse.error }))
          )
        )
      )
    );
  },
  { functional: true }
);

export const updateOrder = createEffect(
  (actions$ = inject(Actions), orderService = inject(OrderService)) => {
    return actions$.pipe(
      ofType(orderActions.updateOrder),
      exhaustMap((action) =>
        orderService.updateOrder(action.order).pipe(
          map((order) => orderActions.updateOrderSuccess({ order })),
          catchError((httpErrorResponse: HttpErrorResponse) =>
            of(orderActions.updateOrderFailure({ error: httpErrorResponse.error }))
          )
        )
      )
    );
  },
  { functional: true }
);

export const getOrderById = createEffect(
  (actions$ = inject(Actions), orderService = inject(OrderService)) => {
    return actions$.pipe(
      ofType(orderActions.getOrderById),
      exhaustMap((action) =>
        orderService.getOrderById(action.id).pipe(
          map((order) => orderActions.getOrderByIdSuccess({ order })),
          catchError((httpErrorResponse: HttpErrorResponse) =>
            of(orderActions.getOrderByIdFailure({ error: httpErrorResponse.error }))
          )
        )
      )
    );
  },
  { functional: true }
);

export const deleteOrderById = createEffect(
  (actions$ = inject(Actions), orderService = inject(OrderService)) => {
    return actions$.pipe(
      ofType(orderActions.deleteOrder),
      exhaustMap((action) =>
        orderService.deleteOrder(action.id).pipe(
          map((order) => orderActions.deleteOrderSuccess({ id: action.id })),
          catchError((httpErrorResponse: HttpErrorResponse) =>
            of(orderActions.deleteOrderFailure({ error: httpErrorResponse.error }))
          )
        )
      )
    );
  },
  { functional: true }
);


