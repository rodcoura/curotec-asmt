import { createReducer, on } from '@ngrx/store';
import { OrderDto } from 'src/app/dtos/order.dto';
import * as orderActions from './order.actions';

export type OrderState = {
  orders: OrderDto[];
  selectedOrder: OrderDto | null;
  error: string | null;
  success: boolean | null;
};

export const initialState: OrderState = {
  orders: [],
  selectedOrder: null,
  error: null,
  success: false
};

export const orderReducer = createReducer(
  initialState,
  // Get Orders
  on(orderActions.getOrders, (state, action) => ({
    ...state,
    error: null,
    success: null
  })),
  on(orderActions.getOrdersSuccess, (state, action) => ({
    ...state,
    orders: action.orders,
    error: null,
  })),
  on(orderActions.getOrdersFailure, (state, action) => ({
    ...state,
    error: action.error,
  })),

  // Create Order
  on(orderActions.createOrder, (state, action) => ({
    ...state,
    error: null,
    success: null
  })),
  on(orderActions.createOrderSuccess, (state, action) => ({
    ...state,
    orders: action.order ? [...state.orders, action.order] : state.orders,
    error: null,
    success: true
  })),
  on(orderActions.createOrderFailure, (state, action) => ({
    ...state,
    error: action.error,
    success: false
  })),

  // Update Order
  on(orderActions.updateOrder, (state, action) => ({
    ...state,
    error: null,
    success: null
  })),
  on(orderActions.updateOrderSuccess, (state, action) => ({
    ...state,
    orders: state.orders.map(order =>
      order.id === action.order.id ? action.order : order
    ),
    selectedOrder: state.selectedOrder?.id === action.order.id ? action.order : state.selectedOrder,
    error: null,
    success: true
  })),
  on(orderActions.updateOrderFailure, (state, action) => ({
    ...state,
    error: action.error,
    success: false
  })),

  // Delete Order
  on(orderActions.deleteOrder, (state, action) => ({
    ...state,
    error: null,
    success: null
  })),
  on(orderActions.deleteOrderSuccess, (state, action) => ({
    ...state,
    orders: state.orders.filter(order => order.id !== action.id),
    selectedOrder: state.selectedOrder?.id === action.id ? null : state.selectedOrder,
    error: null,
    success: true
  })),
  on(orderActions.deleteOrderFailure, (state, action) => ({
    ...state,
    error: action.error,
    success: false
  })),

  // Get Order By Id
  on(orderActions.getOrderById, (state, action) => ({
    ...state,
    error: null,
  })),
  on(orderActions.getOrderByIdSuccess, (state, action) => ({
    ...state,
    selectedOrder: action.order,
    error: null,
  })),
  on(orderActions.getOrderByIdFailure, (state, action) => ({
    ...state,
    error: action.error,
  }))
);
