import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import {RegisterComponent} from './components/register/register.component';
import {TripsComponent} from './components/trips/trips.component';
import { UsersComponent } from './components/users/users.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ExternalLoginComponent } from './components/external-login/external-login.component';
import { AuthenticationGuard, AuthenticationManagerGuard, AuthenticationAdminGuard } from './services/authentication-guards/authentication-guards.service';
import { AddTripComponent } from './components/add-trip/add-trip.component';
import { InviteComponent } from './components/invite/invite.component';
import { AllTripsComponent } from './components/all-trips/all-trips.component';
import { NextMonthPlanComponent } from './components/next-month-plan/next-month-plan.component';

const routes: Routes = [

    { path:'', component : LoginComponent},
    { path:'login', component : LoginComponent,  },
    { path:'register', component : RegisterComponent },
    { path:'externallogin/:token', component : ExternalLoginComponent },

    { path:'trips', component : TripsComponent, canActivate : [AuthenticationGuard] },
    { path:'plan', component : NextMonthPlanComponent, canActivate : [AuthenticationGuard] },
    { path:'profile', component : ProfileComponent, canActivate : [AuthenticationGuard] },

    { path:'users', component : UsersComponent, canActivate : [AuthenticationManagerGuard] },
    
    { path:'addtrip', component : AddTripComponent, canActivate : [AuthenticationGuard] },
    { path:'updatetrip/:id', component : AddTripComponent, canActivate : [AuthenticationGuard] },
    { path:'alltrips', component : AllTripsComponent, canActivate : [AuthenticationAdminGuard] },
    { path:'invite', component : InviteComponent, canActivate :[ AuthenticationAdminGuard ]},
    
    { path:'**', component : LoginComponent},
    
    
]


@NgModule({
    imports: [ RouterModule.forRoot(routes)],
    exports: [ RouterModule ]
})

export class AppRoutingModule {}