import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import { jwtDecode } from "jwt-decode";

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  var token = localStorage.getItem("token");
  if (token) {
    try {
      const decodedToken: any = jwtDecode(token);
      const currentTime = Math.floor(Date.now() / 1000);

      if (decodedToken.exp && decodedToken.exp < currentTime) {
        localStorage.clear();
        router.navigate(['/login']);
        return false;
      }

      return true;

    } catch (error) {
      localStorage.clear();
      router.navigate(['/login']);
      return false;
    }
  }

  router.navigate(['/login']);
  return false;
};
