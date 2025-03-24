import { TestBed } from '@angular/core/testing';

import { LocalStorageService } from './local-storage.service';

describe('LocalStorageService', () => {
  let service: LocalStorageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocalStorageService);
    // Clear localStorage before each test
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setItem', () => {
    it('should store a value in localStorage', () => {
      const key = 'testKey';
      const value = 'testValue';

      service.setItem(key, value);

      expect(localStorage.getItem(key)).toBe(value);
    });
  });

  describe('getItem', () => {
    it('should retrieve a value from localStorage', () => {
      const key = 'testKey';
      const value = 'testValue';

      localStorage.setItem(key, value);
      const result = service.getItem(key);

      expect(result).toBe(value);
    });

    it('should return null for non-existent key', () => {
      const result = service.getItem('nonExistentKey');
      expect(result).toBeNull();
    });
  });

  describe('removeItem', () => {
    it('should remove a value from localStorage', () => {
      const key = 'testKey';
      const value = 'testValue';

      localStorage.setItem(key, value);
      service.removeItem(key);

      expect(localStorage.getItem(key)).toBeNull();
    });

    it('should not throw error when removing non-existent key', () => {
      expect(() => service.removeItem('nonExistentKey')).not.toThrow();
    });
  });

  describe('clear', () => {
    it('should remove all items from localStorage', () => {
      localStorage.setItem('key1', 'value1');
      localStorage.setItem('key2', 'value2');

      service.clear();

      expect(localStorage.length).toBe(0);
      expect(localStorage.getItem('key1')).toBeNull();
      expect(localStorage.getItem('key2')).toBeNull();
    });
  });
});
