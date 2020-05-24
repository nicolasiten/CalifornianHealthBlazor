
# CalifornianHealthBlazor
.Net Core Microservice Application to book doctor appointments. Appointment requests are processed with the help of an AMQP Queue.
![Architecture Overview](https://github.com/nicolasiten/CalifornianHealthBlazor/blob/master/images/Architecture.PNG)
# Projects
## Project Structure: Microservice Architecture
## Common
Code that's common throughout the project. Examples: Exception classes, Models, AMQP Objects.
## AppointmentBooking
Appointment requests from the frontend are routed to this service and appended to an AMQP Queue. They are processed in the Calendar service (AMQP Request/Response Pattern).
## Calendar
Microservice to read and write data to the Database (Doctors, Patients, Availability, Appointments). A HostedService is processing Appointment Requests coming from the AppointmentBooking Service. 
## CalifornianHealthBlazor
Frontend Blazor Application (server side rendering) to book appointments and provide some information about the health clinic and doctors.
![Blazor GUI](https://github.com/nicolasiten/CalifornianHealthBlazor/blob/master/images/WebGui.PNG)

# Logging
Logs are saved to an Elasticsearch NoSQL database and can be visualized through a Kibana Dashboard.
To browse through Logs create an index pattern:
1. Go to Kibana Dashboard --> http://localhost:5601
2. Click "Index Patterns"
3. Click "Create index pattern"
4. To get all Logs use "*" (you can also filter by Service, Environment and Date --> see https://www.elastic.co/guide/en/kibana/current/tutorial-define-index.html)
![Kibana Index Pattern](https://github.com/nicolasiten/CalifornianHealthBlazor/blob/master/images/Kibana1.PNG)
5. Click "Next step"
6. Select "@timestamp" from dropdown
7. Click "Create index pattern"
8. Click on "Discover" (left menu) to see the logs
![Kibana Logs](https://github.com/nicolasiten/CalifornianHealthBlazor/blob/master/images/Kibana2.PNG)

# Setup
## VS Startup
All the services (Microservices, Database, RabbitMQ, Blazor) are running within Docker containers so the following steps are required to start the project up:
 - Make sure docker-compose is the startup project
 - Build the full solution (First build takes a while)
 - Start (if an Exception is shown in the browser on startup just wait a few seconds and refresh the page --> means that a backend service wasn't fully ready yet)
