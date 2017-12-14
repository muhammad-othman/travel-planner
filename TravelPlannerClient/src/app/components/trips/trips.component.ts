import {
  Component,
  OnInit
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Trip } from '../../model/trip';
import { ApiService } from '../../services/api/api.service';

import 'rxjs/add/operator/map';

import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import { Router } from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-trips',
  templateUrl: './trips.component.html',
  styleUrls: ['./trips.component.css']
})
export class TripsComponent implements OnInit {

  constructor(private api:ApiService, private router:Router) {}

  trips:Trip[] = new Array<Trip>();
  totalItems:number;
  pageIndex:number = 1;
  filter:boolean = false;
  destination:string;

  ngOnInit() {
    arrangeDates();
    this.getTrips();
  }
  addTrip(){
    this.router.navigate(['/addtrip'])
  }
  reset(){
    this.filter = false;
    this.destination = ''
    this.pageIndex = 1;
    this.getTrips()
  }
  filterTrips(){
    this.filter = true;
    this.pageIndex = 1;
    this.getTrips()
  }
  updateTrip(id:number){
    this.router.navigate(["/updatetrip/"+id])
  }
  deleteTrip(id){
    this.api.deleteTrip(id).subscribe(e=> this.getTrips())
  }
  getTrips() {
    let url = "?pageIndex="+this.pageIndex;
    if (this.filter) {
      url+= ("&destination="+this.destination)
      url += ("&from=" + $("#datepicker").datepicker({ dateFormat: 'dd-mm-yyyy' }).val());
      url += ("&to=" + $("#datepicker2").datepicker({ dateFormat: 'dd-mm-yyyy' }).val());
   }
    this.api.getTrips(url).subscribe(response => {
      let data = response.json();
      if (data) {
        this.trips.length = 0;
        this.totalItems = data.totalCount;
        (data.trips as Array<any>).forEach(e=>this.trips.push(new Trip(e)));
      }
    })
  }
}


function arrangeDates() {
  $("#datepicker2").datepicker({
    onSelect: function (selectedDate) {
      $("#datepicker").datepicker("option", "maxDate", selectedDate);
    }
  });
  $("#datepicker").datepicker({
    onSelect: function (selectedDate) {
      $("#datepicker2").datepicker("option", "minDate", selectedDate);
    }
  });
}
