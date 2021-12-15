# Hahn.Applicant
Demo application for Aurelia with Dotnet core API


# Project Structure
- 1.Hahn.ApplicatonProcess.Data
- 2.Hahn.ApplicatonProcess.Domain
- 3.Hahn.ApplicatonProcess.Web (Kestrel Host)
  - web-app (Aurelia App)
  
  
In dot net core 
-Hahn.ApplicatonProcess.Web

is start-up project
It will open swagger UI ,where you can see all REST endpoints

In 
- web-app 
run this aurelia app by "au run --open " command

In 
-web-app
 project under services=>applicant-service.js file base url for Web API is provided 
 as  let
 baseurl='https://localhost:5001/api/'
  
  - Change this url to connect with Web api.
  
  
  
