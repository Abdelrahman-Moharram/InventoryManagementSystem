# Summary
This web application provides several functionalities for different types of users (Suppliers, Customers, Admins, and SuperAdmins).
I used The following technologies
- ASP.NET WebAPI
  - EFCore & Linq
  - Identity & JWT Bearer
  - Permissions Based Authorizations
  - Logging
  - Repository Pattern with Unit of work + Services
  - Soft delete
  - Data Seeding
  - Auto Mappers
  - Filters
- SQL Server

-----
  
### Super Admins
- Manage Users Roles (Add to Role, Delete From Role)
- Manage Roles (Add Role, Delete Role)
- Edit Roles Permissions
- Manage Products, Inventories, Categories, Brands, and Product Items (Read, Add, Update, Delete)

### Admins
- Manage Products, Inventories, Categories, Brands, and Product Items (Read, Add, Update) Only

### Suppliers 
- Can manage their stocks of several products with different types (Categories) and Brands In different ways like (Adding stock items, Deleteing stock items)
- Can add new products
- Can update and delete items of products he added

### Customers 
- Can Browse products in different inventories
- Make orders
- Cancel their orders

-----
# Technics
  
## Database
As I mentioned, I used SQL Server as the database engine, there are some core entities like product and inventories, and the relation between them is a Many to Many relation generates (ProductsInventories) table. This table Can Have Many product Items to specify each item's data (Color and serial number) and the collection of these items amount should automatically reflect The Amount of Value in the Product table and the ProductsInventories table you can find this in 
 - InventoryManagementSystem.Infrastructure > Services > Product > ProductService.cs
   
here is the Entity Diagram, it can be clearer 
![image](https://github.com/Abdelrahman-Moharram/InventoryManagementSystem/assets/41553398/b750abb7-64da-4c92-a8d5-bf593aadb20c)

---
## First Launch
At the first run of this web application, the System seeds the following data
 - Users (UserName, Email, Password)
   - Customer, Customer@site.com, 12345678
   - Supplier, Supplier@site.com, 12345678
   - Admin, admin@site.com, 12345678
   - Super-Admin, superadmin@site.com, 12345678
     
 - Roles (This project uses Permissions based authorizations, Each role automatically has its permissions added), I used it in Endpoints like the next image
   
     ![image](https://github.com/Abdelrahman-Moharram/InventoryManagementSystem/assets/41553398/651b41ac-29d6-492c-8f1b-aa15846f6f8d)

   - Customer -> Make orders and manage these orders update/delete
   - Supplier -> Add products and items of products, can only update/remove products he added 
   - Admin -> Has All Permissions for all Modules Except delete, and can't Edit roles permissions or Add/Delete a role 
   - SuperAdmin -> Has All Permissions for all Modules (Read, Add, Update, Delete)
----
## Flow
So as we know we can use routes as configured in program.cs, so the route calls the controller, and the controller calls the ActionResutl (endpoint). The controller takes DTOs and Return DTO (what should appear to the end-user), but after the controller takes the DTO, it turns it into an Entity instance and sends it to The controller Service, and The service has the business logic to all CRUD operations for each Entity and Case and its Related. The Services Call the Repository related to the operation it does. and The repository has the DBVontext that converts the operations into SQL Statements to be applied to the database. 
<strong>Note</strong>: that the Services have the logic that ensures the correctness of each operations user fire, including (try/catch), loggers, and return the results in DTOs in case of GET request, and return Responses (some way I created to show if the operations performed or the problem with a message that express that problem) in case of POST/PUT/DELETE.
