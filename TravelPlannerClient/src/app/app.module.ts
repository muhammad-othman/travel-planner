import { BrowserModule } from '@angular/platform-browser';
import { NgModule, enableProdMode } from '@angular/core';
import {RouterModule} from '@angular/router';
import { AppRoutingModule } from './app.routing.module';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http, XHRBackend, RequestOptions } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import {RegisterComponent} from './components/register/register.component';
import {UsersComponent} from './components/users/users.component';
import {TripsComponent} from './components/trips/trips.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ExternalLoginComponent } from './components/external-login/external-login.component';
import { AddTripComponent } from './components/add-trip/add-trip.component';
import { AllTripsComponent } from './components/all-trips/all-trips.component';
import { InviteComponent } from './components/invite/invite.component';

import { UrlsService } from './services/urls/urls.service';
import { AuthenticationService } from './services/authentication/authentication.service';
import { AuthenticationGuard, AuthenticationManagerGuard, AuthenticationAdminGuard } from './services/authentication-guards/authentication-guards.service';
import { httpFactory } from './services/http-interceptor';
import { ApiService } from './services/api/api.service';
import { NextMonthPlanComponent } from './components/next-month-plan/next-month-plan.component';
import { ProfilePictureComponent } from './components/profile-picture/profile-picture.component';
enableProdMode();




@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    TripsComponent,
    UsersComponent,
    ProfileComponent,
    ExternalLoginComponent,
    AddTripComponent,
    AllTripsComponent,
    InviteComponent,
    NextMonthPlanComponent,
    ProfilePictureComponent
  ],

  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    NgbModule.forRoot()
  ],

  providers: [
    {
      provide: Http,
      useFactory: httpFactory,
      deps: [XHRBackend, RequestOptions]
  },
    AuthenticationService,
    UrlsService,
    AuthenticationGuard,
    AuthenticationManagerGuard,
    AuthenticationAdminGuard,
    ApiService,
    ProfilePictureComponent
  ],

  bootstrap: [HomeComponent]
})
export class AppModule { }