Paltrack Application

Overview

The Paltrack application consists of a frontend built with Angular and a backend API built with ASP.NET Core. The backend uses PostgreSQL as the primary database for storing data, with an in-memory database as a fallback when PostgreSQL is unavailable. The application automatically runs migrations and seeds data into the LogisticsPartners table when the API starts.
Prerequisites
•	Backend API:
•	Ensure the API is set as the startup project in Visual Studio.
•	PostgreSQL should be running and accessible. If not, the application will use an in-memory database.
•	Frontend:
•	Node.js and Angular CLI installed.

Setup Instructions
Backend API
1.	Open the solution in Visual Studio.
2.	Set the API project (Paltrack.WebApi) as the startup project.
3.	Run the API using Visual Studio

 Frontend
1.	Navigate to the frontend project and run the commands below:
2.	cd Paltrack-Frontend
3.	 ng serve -o

Application Workflow
1.	Landing on the Login Page:
•	When you first access the application, you will land on the login page.
2.	Sign Up:
•	Click on the Sign Up button and register with your details.
•	After successful registration, you will be redirected to the Sign In page.
3.	Sign In:
•	Use the credentials you registered to log in.
4.	Dashboard:
•	After logging in, you will be welcomed by a dashboard.
•	The dashboard contains a table displaying Logistics Partners.
•	You can interact with the table and explore its features.

Notes
•	Ensure the API is running before starting the frontend application.
•	If PostgreSQL is unavailable, the application will use an in-memory database for temporary data storage.
•	The application automatically handles database migrations and data seeding.
   
