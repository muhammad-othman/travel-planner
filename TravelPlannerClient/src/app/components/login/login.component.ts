import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  alerts = new Array<String>();
  closeAlert(alert) {
    const index: number = this.alerts.indexOf(alert);
    this.alerts.splice(index, 1);
  }
    constructor(private authService :AuthenticationService, private router:Router) { 
        if(authService.isLoggedIn)
          this.router.navigate(["/trips"]);
    }
    socialMediaLogin(sm){
      this.authService.externalLogin(sm)
    }
    signIn(credentials) {
      this.authService.login(credentials).subscribe(
        r=> {
          if(r === true)
            this.router.navigate(["/trips"]);
          else
            this.alerts.push((r as any)._body);
        },e=>{ 
            this.alerts.push(e._body);
          }
      )
      ;
    }

  ngOnInit() {
  }

}
