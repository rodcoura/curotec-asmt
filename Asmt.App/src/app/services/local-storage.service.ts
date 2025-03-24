import { Injectable } from '@angular/core';

/**
 * Service for handling local storage operations in the application.
 * Provides methods to interact with the browser's localStorage API.
 */
@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  /**
   * Stores a value in localStorage with the specified key.
   * @param key - The key to store the value under
   * @param value - The value to store
   */
  setItem(key: string, value: string): void {
    localStorage.setItem(key, value);
  }

  /**
   * Retrieves a value from localStorage by its key.
   * @param key - The key to look up
   * @returns The stored value if found, null otherwise
   */
  getItem(key: string): string | null {
    return localStorage.getItem(key);
  }

  /**
   * Removes a value from localStorage by its key.
   * @param key - The key of the value to remove
   */
  removeItem(key: string): void {
    localStorage.removeItem(key);
  }

  /**
   * Removes all items from localStorage.
   * Use with caution as this will clear all locally stored data.
   */
  clear(): void {
    localStorage.clear();
  }
}
