import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { OrderService } from './order.service';
import { OrderDto, OrderStatusType } from '../dtos/order.dto';

describe('OrderService', () => {
  let service: OrderService;
  let httpMock: HttpTestingController;
  const apiUrl = 'http://localhost:5094/api/orders';

  const mockOrder: OrderDto = {
    id: 1,
    customerId: 1,
    customerName: 'John Doe',
    status: OrderStatusType.Pending,
    orderItems: [],
    pricePreTax: 100,
    tax: 20,
    totalPrice: 120
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [OrderService]
    });
    service = TestBed.inject(OrderService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all orders', () => {
    const mockOrders: OrderDto[] = [mockOrder];

    service.getOrders().subscribe(orders => {
      expect(orders).toEqual(mockOrders);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockOrders);
  });

  it('should get order by id', () => {
    service.getOrderById(1).subscribe(order => {
      expect(order).toEqual(mockOrder);
    });

    const req = httpMock.expectOne(`${apiUrl}/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockOrder);
  });

  it('should create order', () => {
    service.createOrder(mockOrder).subscribe(order => {
      expect(order).toEqual(mockOrder);
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockOrder);
    req.flush(mockOrder);
  });

  it('should update order', () => {
    service.updateOrder(mockOrder).subscribe(order => {
      expect(order).toEqual(mockOrder);
    });

    const req = httpMock.expectOne(`${apiUrl}/${mockOrder.id}`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(mockOrder);
    req.flush(mockOrder);
  });

  it('should delete order', () => {
    service.deleteOrder(1).subscribe(order => {
      expect(order).toEqual(mockOrder);
    });

    const req = httpMock.expectOne(`${apiUrl}/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(mockOrder);
  });
});
