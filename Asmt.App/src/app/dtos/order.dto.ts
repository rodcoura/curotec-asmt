/**
 * Interface representing an order for API operations.
 */
export interface OrderDto {
    /**
     * The unique identifier for the order.
     */
    id: number;

    /**
     * The price of the order before tax.
     */
    pricePreTax?: number;

    /**
     * The tax amount for the order.
     */
    tax?: number;

    /**
     * The total price of the order including tax.
     */
    totalPrice?: number;

    /**
     * The status of the order.
     */
    status: OrderStatusType;

    /**
     * The identifier of the customer who placed the order.
     */
    customerId: number;

    /**
     * The name of the customer who placed the order.
     */
    customerName: string;

    /**
     * The collection of items included in this order.
     */
    orderItems: OrderItemDto[];
}

/**
 * Interface representing an order item for API operations.
 */
export interface OrderItemDto {
    /**
     * The unique identifier for the order item.
     */
    id: number;

    /**
     * The price of the order item.
     */
    price: number;

    /**
     * The ID of the parent order.
     */
    orderId: number;
}

/**
 * Enum representing the possible statuses of an order.
 */
export enum OrderStatusType {
    Pending = 0,    // The order is pending.
    Completed = 1,  // The order is completed.
    Cancelled = 2  // The order is cancelled.
}