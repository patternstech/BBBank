import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = () => {
  const router = inject(Router);
  if (typeof localStorage !== 'undefined' && localStorage.getItem('loggedInUser') != null) {
    return true;
  }
  else {
    // navigate to login page
    return router.navigate(['/login']);
  }
};
