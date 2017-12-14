import {
  Component,
  OnInit
} from '@angular/core';
import {} from '@types/googlemaps';
import {} from '@types/jqueryui'
import {Trip} from '../../model/trip'
import { Http } from '@angular/http';
import { ApiService } from '../../services/api/api.service';
import { ActivatedRoute, Router } from '@angular/router';
declare var $:any;

@Component({
  selector: 'app-add-trip',
  templateUrl: './add-trip.component.html',
  styleUrls: ['./add-trip.component.css']
})
export class AddTripComponent implements OnInit {

  constructor(private route: ActivatedRoute,private api:ApiService,private router:Router) {}

  updating:boolean = false;
  trip:Trip= new Trip();;
  place:google.maps.places.PlaceResult = null;
  sightseeing:string;
  header:string;
  button:string;
  alerts = new Array<String>();

  closeAlert(alert) {
    const index: number = this.alerts.indexOf(alert);
    this.alerts.splice(index, 1);
  }
  
  ngOnInit() {
    this.route.paramMap.subscribe(params=>{
      let id = params.get('id');
      if(id){
        this.getTripData(+id);
        this.updating = true;
        this.header = 'Update Your Trip'
        this.button = 'Update Trip';
        
      }
      else{
        this.header = 'Add new Trip'
        this.button = 'Add Trip'
      }
    })
    this.initMap();
    arrangeDates();
  }

  getTripData(id:number){
   this.api.getTripById(id).subscribe(e=> {
     this.trip = e.json();
     let lng = this.trip.lng;
     let lat = this.trip.lat;
     this.trip.endDate = new Date(this.trip.endDate).toLocaleDateString();
     this.trip.startDate = new Date(this.trip.startDate).toLocaleDateString();
     this.map.setCenter({
      lat,
      lng
    });
    this.map.setZoom(8);
    this.marker.setPosition({
      lat,
      lng
    });
    this.marker.setVisible(true);
    })
    this.updating = true;
  }

  removeSightseeing(index){
    this.trip.sightseeings.splice(index, 1);
  }

  addSightseeing(){
    this.trip.sightseeings.push(this.sightseeing);
    this.sightseeing = "";
  }


  addTrip(){
    this.trip.startDate = $('#datepicker').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
    this.trip.endDate = $('#datepicker2').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();

    
    if((!this.place && !this.updating) || !this.trip.startDate ||!this.trip.endDate)
    {
      if(!this.place && !this.updating)
        this.alerts.push("Please Choose a valid Destination");
      if(!this.trip.startDate)
        this.alerts.push("Please Choose the starting date of your trip");
      if(!this.trip.startDate)
        this.alerts.push("Please Choose the ending date of your trip");
      return;
    }
    if(this.place){
      this.trip.destination = this.place.name;
      this.trip.lat = this.place.geometry.location.lat();
      this.trip.lng = this.place.geometry.location.lng();
    }
    this.api.submitTrip(this.trip,this.updating).subscribe(e=>this.router.navigate(['/trips']),e=>{
      this.alerts.push(e.body);
    })

  }
 map:google.maps.Map;
 marker : google.maps.Marker;
  initMap() {
    this.trip.sightseeings = new Array<string>();
    let input = document.getElementById('pac-input') as HTMLInputElement;
     this.map = new google.maps.Map(document.getElementById('map'), {
      center: {
        lat: 25,
        lng: 0
      },
      zoom: 2
    });
     this.marker = new google.maps.Marker();
    this.marker.setMap(this.map);
    this.marker.setVisible(false);

    let autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', this.map);
    autocomplete.addListener('place_changed',  () => {
      this.place = autocomplete.getPlace();
      let lat = this.place.geometry.location.lat();
      let lng = this.place.geometry.location.lng();
      this.map.setCenter({
        lat,
        lng
      });
      this.map.setZoom(8);
      this.marker.setPosition({
        lat,
        lng
      });
      this.marker.setVisible(true);
    })
  }
}

function arrangeDates(){
  $("#datepicker2").datepicker({
      onSelect: function(selectedDate) {
          $("#datepicker").datepicker("option", "maxDate", selectedDate);
      }
  });
  $("#datepicker").datepicker({
      onSelect: function(selectedDate) {
          $("#datepicker2").datepicker("option", "minDate", selectedDate);
      }
  });
}