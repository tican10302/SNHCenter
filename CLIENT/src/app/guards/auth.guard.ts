import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  var token = localStorage.getItem("token");
  if(token)
  {
    return true;
  }

  router.navigate(['/']);
  return false;
};
