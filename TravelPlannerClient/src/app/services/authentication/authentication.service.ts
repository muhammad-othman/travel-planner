import {
  Injectable
} from '@angular/core';
import { environment } from "../../../environments/environment";
import {
  JwtHelper
} from 'angular2-jwt';
import {
  Http,
  RequestOptions,
  Headers
} from '@angular/http'
import 'rxjs/add/operator/map';
import {
  Router
} from '@angular/router';
import {
  UrlsService
} from '../urls/urls.service';
import {Subject} from 'rxjs/Subject'; 

@Injectable()
export class AuthenticationService {

  constructor(private http: Http, private urls: UrlsService, private router: Router) {}

  externalLogin(provider: string) {
    var features = "location=yes,height=570,width=520,scrollbars=yes,status=yes";
    var win = window.open(this.urls.externalLogin + provider, "_blank", features);
    setTimeout(() => {
      this.checkExternalLoginStatus(win);
    }, 100);

  }

  checkExternalLoginStatus(win: Window) {
    try {
      let url = win.location.href;
      if (url.includes('externallogin/')) {
        let token = url.split('externallogin/')[1].split('#')[0];
        localStorage.setItem("token", token);
        win.close();
        this.router.navigate(['/trips']);
      } else {
        setTimeout(() => {
          this.checkExternalLoginStatus(win);
        }, 100);
      }
    } catch (ex) {
      setTimeout(() => {
        this.checkExternalLoginStatus(win);
      }, 100);
    }
  }
  get isLoggedIn(): boolean {
    return this.checkLogin();
  }

  get email(): string {
    return localStorage.getItem("sub");
  }

  get isAdmin(): boolean {
    let roles = localStorage.getItem('roles');
    if (roles && roles.includes('admin'))
      return true;
    return false;
  }

  get isManager(): boolean {
    let roles = localStorage.getItem('roles');
    if (roles && (roles.includes('manager') || roles.includes('admin')))
      return true;
    return false;
  }

  login(logindata) {
    return this.http.post(this.urls.login, logindata).
    map(response => {
      let data = response.json();
      if (data && data.token) {
        localStorage.setItem("token", data.token);
        return true;
      }
    })
  }
  register(registerdata) {
    return this.http.post(this.urls.register, registerdata).
    map(response => {
      if (response.ok)
        return true;
    })
  }

  logout() {
    this.http.get(this.urls.logout)
    localStorage.removeItem("token");
    localStorage.removeItem("roles");
    localStorage.removeItem("sub");
    this.router.navigate(['/login']);
  }
  profilePicture = new Subject();
  get picture():string{
    return localStorage.getItem('picture');
  }
  set picture(value) {
    this.profilePicture.next(value); 
    localStorage.setItem('picture', value);
  }
  checkLogin() {
    let jwt = new JwtHelper();
    let token = localStorage.getItem('token');
    if (token) {
      localStorage.setItem('sub', jwt.decodeToken(token).sub)
      localStorage.setItem('roles', jwt.decodeToken(token).roles)
      let pic = jwt.decodeToken(token).picture;
      if(!pic.includes('https://'))
        pic = environment.origin.replace('api/','')+pic;
      localStorage.setItem('picture', pic)
    }
    if (token && !jwt.isTokenExpired(token))
      return true;
    return false;
  }
}