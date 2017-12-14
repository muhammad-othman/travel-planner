import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api/api.service';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.css']
})
export class InviteComponent implements OnInit {

  constructor(private api: ApiService) { }
  email:string;
  ngOnInit() {
  }
  invite(){
    this.api.inviteNewUser(this.email).subscribe(e=>this.email = '');
  }
}
