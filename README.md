# JWT
- This project demonstrates the implementation of JWT Token based Authorization.
- This project uses .net core 3.1.
- This project helps understand the use of JWT for token based authorization and also role wise access to the API using JWT token.
- Also added expiry time for token so that token would not work after the specified time.
- For demo purpose I have hardcoded emails and passwords in the list you could see in the controller.
- You could use postman to test these endpoints.

  
 # Steps
 1. Run the API,initially you would be redirected to 'user' route but you wont see anything as you are not authorized to view the content.
 2. Call the Login API endpoint by sending the email and password from the hardcoded list for the demo. Once authenticated you would get back the token.
 3. Use the token in the header to send it to the API as [Shown](https://prnt.sc/s8odah)
 3. There are 3 users so I created three API endpoints for accessing Super Admin, Admin, and user contents respectively.
 4. If you logged in as super admin then you would be able to access the API endpoint for getting the super user contents only and not contents for other users and respectively for Admin and user.
 5. I have demonstrated 3 seperate API to understand the role based authorization.
 6. You could add multiple roles for an endpoint accordingly by appending to the Role attribute as "[Authorize(Roles = Roles.Admin +"," + Roles.SuperAdmin)]" so in this case the endpoint would be accessible for Admin and Super Admin roles but not user roles.
