import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthenticationService } from '../authentication/authentication.service';


@Injectable()
export class AuthenticationGuard implements CanActivate {
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    if(!this.auth.isLoggedIn)
      this.router.navigate(['/login']);
    return this.auth.isLoggedIn;
  }
  constructor(private auth : AuthenticationService,private router:Router) { }
}

@Injectable()
export class AuthenticationManagerGuard implements CanActivate {
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    if(!this.auth.isManager)
    this.router.navigate(['/login']);
    return this.auth.isManager;
  }
  constructor(private auth : AuthenticationService,private router:Router) { }
}

@Injectable()
export class AuthenticationAdminGuard implements CanActivate {
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    if(!this.auth.isAdmin)
      this.router.navigate(['/login']);
    return this.auth.isAdmin;
  }
  constructor(private auth : AuthenticationService,private router:Router) { }
}
