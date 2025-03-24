import { createFeatureSelector, createSelector } from '@ngrx/store';
import { OrderState, orderReducer } from './order.reducer';
import { OrderDto } from 'src/app/dtos/order.dto';

// Base selector for the entire order state
export const selectOrderState = createFeatureSelector<OrderState>('orderState');

// Base selectors for individual state properties
export const selectOrders = createSelector(
  selectOrderState,
  (state: OrderState) => state.orders
);

export const selectSelectedOrder = createSelector(
  selectOrderState,
  (state: OrderState) => state.selectedOrder
);

export const selectOrderError = createSelector(
  selectOrderState,
  (state: OrderState) => state.error
);

// Derived selectors
export const selectOrdersCount = createSelector(
  selectOrders,
  (orders: OrderDto[]) => orders.length
);

export const selectOrderSuccess = createSelector(
  selectOrderState,
  (state: OrderState) => state.success
);

