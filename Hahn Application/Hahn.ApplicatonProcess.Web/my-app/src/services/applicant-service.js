
import {HttpClient, json} from 'aurelia-fetch-client';

let httpClient = new HttpClient();
let baseurl='https://localhost:5001/api/'
export class ApplicantService {
  
  getCountries() {
   return httpClient.fetch('countries.json')    
     .then(response => response.json())
      .catch(error=>console.log(error));      
   }

   getData(){
      return httpClient.fetch(baseurl+'applicant')
               .then(response => response.json())
  }
   
   postData(myPostData) {
    return  httpClient.fetch(baseurl+'applicant', {
         method: "POST",
         body: json(myPostData)
      })		
   }

   updateData(myUpdateData) {      
      return  httpClient.fetch(baseurl+'applicant/'+myUpdateData.id, {
         method: "PUT",
         body: json(myUpdateData)
      })		
      
   }

   deleteData(id) {
     return  httpClient.fetch(baseurl+'applicant/'+id, {
         method: "DELETE"
      })
      
   }

        

}