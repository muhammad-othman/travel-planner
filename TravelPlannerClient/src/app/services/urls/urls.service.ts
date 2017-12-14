import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";

@Injectable()
export class UrlsService {

  public trips = "trips";
  public users = "users";
  public login = "account/login";
  public logout = "account/logout";
  public register = "account/register";
  public invite = "account/invite";
  public uploadProfilePicture = "account/Picture";
  public externalLogin = environment.origin+"account/externallogin?provider=";

  constructor() { }

}
