import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api/api.service';
import { User } from '../../model/user';
import { AuthenticationService } from '../../services/authentication/authentication.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
    constructor(private api:ApiService,private auth:AuthenticationService) {}
  
    users:User[] = new Array<User>();
    totalItems:number;
    pageIndex:number = 1;
    filter:boolean = false;
    searchMail:string;

    roles = ['admin','manager','user'];
    ngOnInit() {
      this.getUsers();
    }

    isAuthorized(user:User):boolean{
      if(!this.auth.isAdmin && user.role == 'admin')
            return false;
       return true;
    }
    alerts = new Array<String>();
    closeAlert(alert) {
      const index: number = this.alerts.indexOf(alert);
      this.alerts.splice(index, 1);
    }
    reset(){
      this.filter = false;
      this.searchMail = ''
      this.pageIndex = 1;
      this.getUsers()
    }
    filterUsers(){
      this.filter = true;
      this.pageIndex = 1;
      this.getUsers()
    }
    updateUser(user){
      this.api.updateUser(user).subscribe(e=>this.alerts.push("User Has been Updated"));
    }
    deleteUser(id){
      this.api.deleteUser(id).subscribe(e=> {this.alerts.push("User Has been Updated");this.getUsers();})
    }
    getUsers() {
      let url = "?pageIndex="+this.pageIndex;
      if (this.filter) {
        url+= ("&email="+this.searchMail)
     }
      this.api.getUsers(url).subscribe(response => {
        let data = response.json();
        if (data) {
          this.users.length = 0;
          this.totalItems = data.totalCount;
          (data.users as Array<any>).forEach(e=>{
            if(e.picture && !e.picture.includes('https://'))
              e.picture = environment.origin.replace('api/','')+e.picture;
              this.users.push(new User(e))});
        }
      })
    }
  }
  