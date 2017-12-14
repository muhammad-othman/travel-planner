import { Component, OnInit,Input } from '@angular/core';
import { AuthenticationService } from '../../services/authentication/authentication.service';

@Component({
  selector: 'app-profile-picture',
  templateUrl: './profile-picture.component.html',
  styleUrls: ['./profile-picture.component.css']
})
export class ProfilePictureComponent implements OnInit {
  @Input()
  imageWidth: number = 400;
  pic:string;
  constructor(private auth : AuthenticationService) { }
  setDefaultPic(){
    this.pic =  "../../assets/profile-placeholder.svg";
  }
  ngOnInit() {
    if(this.auth.picture)
      this.pic =this.auth.picture;
    else 
      this.setDefaultPic();
      this.auth.profilePicture.subscribe(e=> this.pic = e as string);
  }
}
