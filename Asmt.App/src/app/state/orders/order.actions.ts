import { createAction, props } from '@ngrx/store';
import { OrderDto } from 'src/app/dtos/order.dto';

export const getOrders = createAction('[Orders] Get Orders');

export const getOrdersSuccess = createAction(
  '[Orders] Get Orders Success',
  props<{ orders: OrderDto[] }>()
);

export const getOrdersFailure = createAction(
  '[Orders] Get Orders Failure',
  props<{ error: string }>()
);

export const createOrder = createAction(
  '[Orders] Create Order',
  props<{ order: OrderDto }>()
);

export const createOrderSuccess = createAction(
  '[Orders] Create Order Success',
  props<{ order: OrderDto }>()
);

export const createOrderFailure = createAction(
  '[Orders] Create Order Failure',
  props<{ error: string }>()
);

export const updateOrder = createAction(
  '[Orders] Update Order',
  props<{ order: OrderDto }>()
);

export const updateOrderSuccess = createAction(
  '[Orders] Update Order Success',
  props<{ order: OrderDto }>()
);

export const updateOrderFailure = createAction(
  '[Orders] Update Order Failure',
  props<{ error: string }>()
);

export const deleteOrder = createAction(
  '[Orders] Delete Order',
  props<{ id: number }>()
);

export const deleteOrderSuccess = createAction(
  '[Orders] Delete Order Success',
  props<{ id: number }>()
);

export const deleteOrderFailure = createAction(
  '[Orders] Delete Order Failure',
  props<{ error: string }>()
);

export const getOrderById = createAction(
  '[Orders] Get Order By Id',
  props<{ id: number }>()
);

export const getOrderByIdSuccess = createAction(
  '[Orders] Get Order By Id Success',
  props<{ order: OrderDto }>()
);

export const getOrderByIdFailure = createAction(
  '[Orders] Get Order By Id Failure',
  props<{ error: string }>()
);






















