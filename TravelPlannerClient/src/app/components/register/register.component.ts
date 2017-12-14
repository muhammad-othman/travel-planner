import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private authService:AuthenticationService, private router:Router) { }

  ngOnInit() {
  }
  register(credentials) {
    this.authService.register(credentials).subscribe(
          r=>  this.router.navigate(['/login']),
          e=>this.alerts.push(e._body)
    )
  }
  alerts = new Array<String>();
  closeAlert(alert) {
    const index: number = this.alerts.indexOf(alert);
    this.alerts.splice(index, 1);
  }
}
