# Tech Masters .Net Core Back - End Bootcamp Shopping App
----
That project created for Tech Masters .Net Core Back - End Bootcamp final project which is subjected to create ShoppingList App in order to make softer our daily jobs and helpful to our shopping.
------
### Features 
- There is an admin and user role which is able to register and login
- Admin have an abilities to create new categories, products (price,images, name, inherit to category etc), shoppinglist etc, and can update the existing datas from user interface.
- User can create,update or delete shoppinglists, can add products from website
- System's authentication and authorization is created with Microsoft.AspNetCore.Authentication.Cookies and built with EF Core data access technology.
- ASP.NET MVC(Model-View-Controller) which is web-application frameworked used as an architectural pattern.
----
### Project built with
- ASP.NET Backend(C#,.NET 7)
- MSSQL Database
- HTML-CSS-JavaScript
----
> Manual Guide

- Edit to connection string from Program.cs
```
builder.Services.AddDbContext<ShoppingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```
### Database 
- I have defined an entity models in code, create a database from the model, and then add data to the database by Code-First approach and with the supportiong of EF Core.

  ---

Created Database diagram can be found below.

![](https://i.imgur.com/fPuxnry.png)

---

### FrontEnd
- Fronted built with the HTML/CSS and JavaScript.
