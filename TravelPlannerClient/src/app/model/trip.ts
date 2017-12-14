export class Trip{
   
    id:number;
    destination:string;
    startDate:string;
    endDate:string;
    comment:string;
    sightseeings:string[];
    lng:number;
    lat:number;
    sights:string;
    userEmail:string;
    counter:string;
    
    constructor(private object?:any) {
        if(object){
        this.id=object.id;
        this.destination=object.destination;
        this.startDate=new Date(object.startDate).toLocaleDateString();
        this.endDate=new Date(object.endDate).toLocaleDateString();
        this.comment=object.comment;
        this.sightseeings=object.sightseeings;
        this.sights = object.sightseeings.join(", ")
        this.lng=object.lng;
        this.lat=object.lat;
        this.userEmail = object.userEmail;
        this.counter = this.getCounter(object.startDate);
        }
        }

        getCounter(startDate): string {
            let oneDay = 24*60*60*1000; // hours*minutes*seconds*milliseconds
            let today = new Date();
            let start = new Date(startDate);
            if(start < today)
                return 'Already Started' 
            let diffDays = Math.floor(Math.abs((today.getTime() - start.getTime())/(oneDay)))+1;
            return diffDays.toString()+' days left';
        }
    }
