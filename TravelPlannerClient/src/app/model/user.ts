export class User{
    id:string;
    email:string;
    isLocked:boolean;
    picture:string;
    role:string;
    creationDate:Date;
    emailConfirmed:boolean;

    constructor(object:any) {
        this.id= object.id;
        this.email= object.email;
        this.isLocked= object.isLocked;
        this.picture= object.picture;
        this.role= object.role;
        this.creationDate= object.creationDate;
        this.emailConfirmed= object.emailConfirmed;
    }
}