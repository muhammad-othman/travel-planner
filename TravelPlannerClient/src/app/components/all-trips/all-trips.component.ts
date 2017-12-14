import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api/api.service';
import { Router } from '@angular/router';
import { Trip } from '../../model/trip';
declare var $: any;

@Component({
  selector: 'app-all-trips',
  templateUrl: './all-trips.component.html',
  styleUrls: ['./all-trips.component.css']
})

export class AllTripsComponent implements OnInit {

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
      url+="&alltrips=true";
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