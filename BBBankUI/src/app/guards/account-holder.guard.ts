import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const accountHolderGuard: CanActivateFn = () => {
   const router = inject(Router);
   if (typeof localStorage !== 'undefined' ) {
     const loggedInUserRole = JSON.parse(localStorage.getItem('loggedInUser') || '{}').roles[0];
     if (loggedInUserRole == 'account-holder') {
       return true;
     }
     return false;
   }
   else {
     // navigate to login page
     return router.navigate(['/login']);
   }
};
