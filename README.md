1. Project Structure- 
       Organize project using the Clean Architecture principles.
   PatientSearchApi
|-- PatientSearch.Domain
|-- PatientSearch.Application
|-- PatientSearch.Infrastructure
|-- PatientSearch.Web
|-- PatientSearch.Tests




Step to use Application-

1- Open Clone repo with your local
2- Open App Setting in Patient.Web Solution and Change Default connection string as per ypur local data
3- Build Application
4- Run the application system with auto migrate the database 
      4.1- create table Patients, Usermaster, Departments
5- Need to adde departments and patient details to check api
6- Once data get prepaired 
7- Runt Token api to get JWT Token using username and password
       
      Exaple-
       Inpute url: 
         http://localhost:5140/api/Security/Token?username=test%40123&password=test%40123
      Reponse :
       {
          "accesstoken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRlc3RAMTIzIiwiTG9jYXRpb24iOiIiLCJJc0FkbWluIjoidHJ1ZSIsImp0aSI6Ijk2YmMwYjg1LWI4MjMtNGM4MC05OTQwLTlmMDM2YjcxODAyMiIsImV4cCI6MTcwODM1MDcyNSwiaXNzIjoiVmFsaWRJc3N1ZXIiLCJhdWQiOiJWYWxpZEF1ZGllbmNlIn0.AZVPXmZgE91-A_cVuIw_9yZa97r3iz9tuiQeM7PVMuQ"
        }  
8- Call Search Ptient API using JWT token 

    Inpute 
    URl- http://localhost:5140/api/Patient/SearchPatients?Query=&DepartmentId=0&SortBy=Name&Page=1&PageSize=2
   status - 200
    Reponse- [
    {
        "PatientId": 9,
        "Name": "Aasvi Jay Singh",
        "AdminsionDate": "2024-02-17T00:00:00",
        "Problem": "Cold and Fever",
        "Address": "Laxmi Nagar East Delhi Delhi India 110093",
        "DepartmentId": 12,
        "DepartmentName": "Pediatrics department"
    },
    {
        "PatientId": 12,
        "Name": "Ajay Jadeja",
        "AdminsionDate": "2024-02-16T00:00:00",
        "Problem": "Back Problem",
        "Address": "Mumbai Mubai East Maharashra India 110092",
        "DepartmentId": 16,
        "DepartmentName": "Orthopaedic department"
    }
]
   
      
