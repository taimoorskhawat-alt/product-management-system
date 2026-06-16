import { CanActivateFn,Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from './services/authservice';
export const authGuard: CanActivateFn = (route, state) => {
     const router = inject(Router);
      const authService = inject(AuthService);

  const token = localStorage.getItem('token');

   if (!token) {
    router.navigate(['/login']);
    return false;
  }
    if (!authService.getTokenExpiry()) {
    localStorage.clear();
    router.navigate(['/login']);
    return false;
  }

  
  return true;
};
