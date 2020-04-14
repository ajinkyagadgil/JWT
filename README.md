# JWT
- This project demonstrates the implementation of JWT Token based Authorization.
- This project uses .net core 3.1 
- For sample there are 2 API endpoint 
  - First is the Login API which accepts User model(from body) which has Email and Password. For demo purpose I have hardcoded email Id to be        'admin@test.com' and password is 'admin'.
  - Second is the GetUsers in order to test if the token based authentication works.
  
 # Steps
 1. Run the project. By default the GetUsers will start and give error code 401 - Unauthorized.
 2. Call the Login API endpoint by sending the email and password. Once authenticated you would get back the token.
 3. Use the token in the header to call the GetUsers() method which would now be authorized.
