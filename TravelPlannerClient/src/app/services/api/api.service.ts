import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { UrlsService } from '../urls/urls.service';
import { Trip } from '../../model/trip';
import { User } from '../../model/user';

@Injectable()
export class ApiService {

  constructor(private http:Http, private url:UrlsService) { 

  }

  

  uploadImage(fileToUpload: any) {
    let input = new FormData();
    input.append("file", fileToUpload);

    return this.http
        .post(this.url.uploadProfilePicture, input);
  }

  submitTrip(trip:Trip, updating:boolean){
    if(updating)
      return this.http.put(this.url.trips+"/"+trip.id,trip);
    return this.http.post(this.url.trips,trip);
  }
  inviteNewUser(email:string){
    return this.http.post(this.url.invite+"/"+email,'')
  }
  getTripById(id:number){
    return this.http.get(this.url.trips+"/"+id);
  }

  getTrips(filters:string){
    return this.http.get(this.url.trips+filters);
  }

  deleteTrip(id){
    return this.http.delete(this.url.trips+"?id="+id);
  }

  getUsers(filters:string){
    return this.http.get(this.url.users+filters);
  }
  updateUser(user:User){
      return this.http.put(this.url.users+"/"+user.id,user);
  }

  deleteUser(id){
    return this.http.delete(this.url.users+"?id="+id);
  }
}
