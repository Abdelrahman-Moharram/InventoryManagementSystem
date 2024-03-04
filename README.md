# Summary
This web application provides several functionalities for different types of users (Suppliers, Customers, Admins, and SuperAdmins).
I used The following technologies
- ASP.NET WebAPI
  - EFCore & Linq
  - Identity & JWT Bearer
  - Permissions Based Authorizations
  - Logging
  - Repository Pattern with Unit of work + Services
  - Data Seeding
  - Auto Mappers
  - Filters
  - Soft delete
- SQL Server

-----
  
### Super Admins
- Manage Users Roles (Add to Role, Delete From Role)
- Manage Roles (Add Role, Delete Role)
- Edit Roles Permissions
- Manage Products, Inventories, Categories, Brands, and Product Items (Add, Update, Delete)

### Admins
- Manage Products, Inventories, Categories, Brands, and Product Items (Add, Update) Only

### Suppliers 
- Can manage their stocks of several products with different types (Categories) and Brands In different ways like (Adding stock items, Deleteing stock items)
- Can add new products
- Can update and delete items of products he added

### Customers 
- Can Browse products in different inventories
- Make orders
- Cancel their orders

-----

