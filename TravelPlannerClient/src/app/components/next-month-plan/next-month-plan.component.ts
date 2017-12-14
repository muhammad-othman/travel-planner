import { Component, OnInit } from '@angular/core';
import { Trip } from '../../model/trip';
import { ApiService } from '../../services/api/api.service';

@Component({
  selector: 'app-next-month-plan',
  templateUrl: './next-month-plan.component.html',
  styleUrls: ['./next-month-plan.component.css']
})
export class NextMonthPlanComponent implements OnInit {

  constructor(private api:ApiService) {}
    map:google.maps.Map;
    trips:Trip[] = new Array<Trip>();
    totalItems:number;
    pageIndex:number = 1;
    filter:boolean = false;
    destination:string;
  
    ngOnInit() {
      this.getTrips();
    }
    
    getTrips() {
      let url = "?pageIndex="+this.pageIndex;
      let today = new Date();

        url += ("&from=" + today.toLocaleDateString());
        today.setDate(today.getDate()+30);
        url += ("&to=" +today.toLocaleDateString());
    
      this.api.getTrips(url).subscribe(response => {
        let data = response.json();
        if (data) {
          this.initMap();
          this.trips.length = 0;
          this.totalItems = data.totalCount;
          (data.trips as Array<any>).forEach(e=>{
              this.trips.push(new Trip(e));
              this.addMarker(e.lng,e.lat,e.destination)
          });
        }
      })
    }
    initMap(){
      this.map = new google.maps.Map(document.getElementById('map'), {
        center: {
          lat: 25,
          lng: 5
        },
        zoom: 2
      });
    }
    addMarker(lng:number,lat:number,label:string){
      let marker = new google.maps.Marker();
      marker.setMap(this.map);
     // marker.setLabel(label);
      marker.setPosition({
       lat,
       lng
     });
      marker.setVisible(true);
     
    }

}
