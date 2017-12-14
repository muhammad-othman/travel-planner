import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthenticationService } from '../../services/authentication/authentication.service';
import { ApiService } from '../../services/api/api.service';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { ProfilePictureComponent } from '../profile-picture/profile-picture.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  
  constructor(private api:ApiService, private router :Router,private auth:AuthenticationService) { }
  setDefaultPic(){
   
  }
  ngOnInit() {

  }
  @ViewChild("fileInput") fileInput;
  uploadImage(): void {
    let fi = this.fileInput.nativeElement;
    if (fi.files && fi.files[0]) {
        let fileToUpload = fi.files[0];
        this.api
            .uploadImage(fileToUpload)
            .subscribe(res => {
              this.auth.picture = environment.origin.replace('api/','')+res["_body"];
              this.router.navigate(['/trips']);
            });
    }
  }
}
