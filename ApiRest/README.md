# Estructura de directorios correspondiente al BackEnd
### Contenido
Se tiene tiene 3 endpoints.
- El primero se encarga de recuperar el nombre y el título asociados a la cedula, es un metodo GET con path:

GET http://localhost:58683/api/cedula/{cedula}

![image](https://github.com/DennisPerez97/WeeCompany-Dennis/assets/99489937/c6da3d9d-2fe5-4987-95c8-d1b6f2187d99)


- El segundo se encarga de recuperar todos los registros, es un metodo GET con path:
  
GET http://localhost:58683/api/user

![image](https://github.com/DennisPerez97/WeeCompany-Dennis/assets/99489937/c792fc53-c365-43fd-b5a4-3e7487839f3b)


- El tercero se encarga de guardar un registro, es un metodo POST con path:
  
POST http://localhost:58683/api/user

![image](https://github.com/DennisPerez97/WeeCompany-Dennis/assets/99489937/713f7704-2f43-4964-b9c6-95034fc4b3c0)

### Colección de Postman
[WeeCompanyTest](https://github.com/DennisPerez97/WeeCompany-Dennis/blob/master/ApiRest/Wee%20Company%20Test.postman_collection.json)
