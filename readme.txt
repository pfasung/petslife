PetsLife - web application

PetsLife represents web application for demonstrating use of following technologies
- web api
- web api authentication
- web api attribute based routing
- json serialization
- repository pattern
- entity framework code first migration pattern (in folder Migrations)
- unit testing using moq framework
- jquery, knockout, bootstrap, modernizr, moment

Following apis are available:

pets (api/PetsController.cs):
http get  api/pets	- list of all pets
http get  api/pets/5	- get pet by id
http put  api/pets/5	- put pet by id
http post api/pets		- post new pet
http delete api/pets/5	- delete pet by id

http get api/pets/GetPetsByFilter/{Id:int},{underAge:int},{ownerId:int}	- get pets by specified criteria
http get api/PetOwners/{ownerId}/Pets	- get pets by owner id

pet walkers (api/PetWalkersController.cs):
http get  api/petwalkers		- list of all pet walkers
http get  api/petwalkers/5		- get pet walker by id
http put  api/petwalkers/5		- put pet walker by id
http post api/petwalkers		- post new pet walker
http delete api/petwalkers/5	- delete pet by walker id
http get api/petwalkers/IsApprovedByPetOwner/{walkerId:int}/{ownerId:int}	- Is PetWalker approved by pet owner

pet owners (api/PetOwnersController.cs):
http get  api/petowners			- list of all pet owners
http get  api/petowners/5		- get pet owner by id
http put  api/petowners/5		- put pet owner by id
http post api/petowners			- post new pet owner
http delete api/petowners/5		- delete pet owner by id

pet approvals (api/PetApprovalsController.cs):
http get  api/petapprovals		- list of all pet approvals
http get  api/petapprovals/5	- get pet approval by id
http put  api/petapprovals/5	- put pet approval by id
http post api/petapprovals		- post new pet approval
http delete api/petapprovals/5	- delete pet approval by id

all apis use create http response messages with appropriate status code and json serialization
convenient way for observing messages is by using Internet Explorer Developer Tools (Network profiler)

Project PetsLife is organized as follows:
- Api - ApiControllers
- Controllers - MvcControllers
- DAL - Data Access Layer - db context, repositories, db initializer
- Migrations - Code First Migrations
- Models - Entity Model
- ViewModels - knockout javascript view models
- Views - mvc views - all views load data using web api
