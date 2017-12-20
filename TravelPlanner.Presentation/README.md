
###### Jogging Tracker Info ########

###### Viewing the serverside
`http://localhost:44388`

###### Viewing the clientside
`http://localhost:4222`

# Schema

## Trip

- id  (integer)
- destination  (string)
- comment  (string)
- startDate  (date)
- endDate  (date)
- lat  (double)
- lng  (double)
- sightseeings  (Array<string>)
- userEmail  (string)



## User

- id  (string)
- email  (string)
- isLocked  (boolean)
- picture  (string)
- role  (string)
- creationDate  (date)
- emailConfirmation  (boolean)

#########################################################################################

# API Resources

## Trips
```
GET|POST          /api/trips
GET|PUT|DELETE    /api/trips/{id}
GET 			  /api/trips/{pageIndex}{pageSize}      //Server Side Pagination
GET 			  /api/trips/{from}{to}       			//Date Filtering
GET 			  /api/trips/{destination}      		//Destination Filtering
GET 			  /api/trips/{alltrips}      			//for Admins


## Users
```
GET		          /api/users
GET|PUT|DELETE    /api/users/{id}
GET 			  /api/users/{pageIndex}{pageSize}      //Server Side Pagination
GET|PUT|DELETE    /api/users/{email}					//Email Filtering


## Account
```
POST          	  /api/account/register
GET         	  /api/account/logout
GET         	  /api/account/login
POST          	  /api/account/picture 					//Upload user picture
Get            	  /api/account/externalLogin/{provider}	//Facebook & Google login
POST          	  /api/account/invite/{email}			//Invite new user for admins only
