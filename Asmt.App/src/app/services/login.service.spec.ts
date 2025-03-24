import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { LoginService } from './login.service';
import { LocalStorageService } from './local-storage.service';
import { UserDto } from '../dtos/user.dto';

describe('LoginService', () => {
  let service: LoginService;
  let httpMock: HttpTestingController;
  let localStorageService: jasmine.SpyObj<LocalStorageService>;

  const mockUser: UserDto = {
    id: 1,
    name: 'Test User',
    email: 'test@example.com',
    role: 'user',
    token: 'mock-token'
  };

  beforeEach(() => {
    const localStorageSpy = jasmine.createSpyObj('LocalStorageService', ['setItem', 'getItem', 'removeItem']);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        LoginService,
        { provide: LocalStorageService, useValue: localStorageSpy }
      ]
    });

    service = TestBed.inject(LoginService);
    httpMock = TestBed.inject(HttpTestingController);
    localStorageService = TestBed.inject(LocalStorageService) as jasmine.SpyObj<LocalStorageService>;
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('login', () => {
    it('should make POST request to login endpoint and store token', () => {
      const credentials = { email: 'test@example.com', password: 'password123' };

      service.login(credentials).subscribe(user => {
        expect(user).toEqual(mockUser);
      });

      const req = httpMock.expectOne('http://localhost:5094/api/token');
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(credentials);
      req.flush(mockUser);

      expect(localStorageService.setItem).toHaveBeenCalledWith('token', mockUser.token as string);
    });
  });

  describe('storeToken', () => {
    it('should store token in localStorage', () => {
      const token = 'test-token';
      service.storeToken(token);
      expect(localStorageService.setItem).toHaveBeenCalledWith('token', token);
    });
  });

  describe('logout', () => {
    it('should remove token from localStorage', () => {
      service.logout();
      expect(localStorageService.removeItem).toHaveBeenCalledWith('token');
    });
  });

  describe('isLoggedIn', () => {
    it('should return true when token exists', () => {
      localStorageService.getItem.and.returnValue('test-token');
      expect(service.isLoggedIn()).toBeTrue();
    });

    it('should return false when token does not exist', () => {
      localStorageService.getItem.and.returnValue(null);
      expect(service.isLoggedIn()).toBeFalse();
    });
  });

  describe('getToken', () => {
    it('should return token from localStorage', () => {
      const token = 'test-token';
      localStorageService.getItem.and.returnValue(token);
      expect(service.getToken()).toBe(token);
    });

    it('should return null when token does not exist', () => {
      localStorageService.getItem.and.returnValue(null);
      expect(service.getToken()).toBeNull();
    });
  });
});
