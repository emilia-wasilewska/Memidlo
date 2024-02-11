Memidlo is an application with mems created in Razor Pages framework (7.0) using Boostrap (5.0), the data for which are stored in MS SQL Server 2019 and mapped with use of Entity Framework (7+). 

It has all main functionalities of similar apps such as: main page, waiting room, top ten, show a random mem and find a mem by tag.

It contains user registration and management solution basing on Microsoft.AspNetCore.Identity and Microsoft.AspNetCore.Identity.EntityFrameworkCore:
- users can register their own accounts, write comments, give likes, add new mems to the waiting room section; they can also edit their credentials and edit mems,
- admins can move mems to the main page or delete them, they can also create, edit and delete user accounts,
- superadmin can grant the admin role.

Images are stored in Cloudinary.
