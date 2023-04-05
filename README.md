Case Control is a web app build with Asp.Net MVC (.Net Framework 4.7.1) that allows for medical statistical data gathering. 

UTILITY:

* This project is useful for a myriad of applications in which random events and their side effects need to be reported and aggregated;
* The naming of the domain classes and its list of properties is related to the specific medical domain for which it was created, but a simple 
refactoring can turn this project into a generic version of itself or a specific version for another domain.

PURPOSE:

It allows doctors to: 

a) report collateral effects that their patients seemed to notice after receiving vaccination doses; and 

b) browse aggregated reports on the collective data input.

FUNCTIONALITIES / FUNCTIONAL REQUIREMENTS:

The functionalities include:

A) For doctors:

* User Sign-Up and Access Control;
* Register Case:
    1. Patient medical information and medica history, with no patient identification data);
    2. Patient's vaccination doses received;
    3. Patient's reactions perceived;
* Aggregated Statistical Reports;

To report patient's reactions, the doctor is presented with a step-by-step registration process.

B) For system administrators:

* Laboratory Registration;
* Reaction Type Registration (for instance, "headache");
* Group of Reaction Type Registration (for instance, "mild");
* Country, State and City Registration.

C) For users:

* User sign-up;
* Access control.

NOTICE / DISCLOSURE:

The development of this app was requested by a freelance client. It doesn't represent any political or scientific view of this developer towards vaccination.

It is being shared in Github as a showcase of my work, since this was a job that I have executed alone, in a very short period of time. It is also being shared for academic purposes and it is small enough to be able to be comprehended end-to-end pretty quickly, and because it can be refactored into a more generic version or domain-specific versions and provide utility for different applications.

DEVELOPMENT PROCESS:

Tech steps included:

* Creating a Template Asp.Net MVC app;
* Bulding the Model and inserting user-friendly Strings to be used in UI with Property Attributes;
* Enabling and configuring Migrations;
* Scaffolding Controllers and Views;
* Creating a Database in SQL Azure;
* Creating and updating database schema using Code First Migrations;
* Customizing Controllers with business rules, validations and workflows;
* Customizing Views with workflows and validations.

FUTURE UPGRADES:

As a small MVP-like product resulting of a short deadline project, it showcases basic architecture. It can be refactored for further growth 
using CQRS, SOLID principles, CLEAN architecture principles and so on.

On a very specific note, some Yes/No/Unaware or Mild/Moderate/Severe properties were represented as Integers. It would be a best practice to turn them into Enums in further versions.

As an MVP, this product used bare bones tamplate designs with Bootstrap. There is no custom design or branding. You can upgrade that.

Feel free to ask any questions and to contribute.

USAGE:

1) In root file Web.config, insert the connection string to a SQL Azure / SQL Server Database:

      ```
      <connectionStrings>
        <add name="DefaultConnection" connectionString="[INSERT CONNECTION STRING HERE, WITHOUT BRACKETS]"
          providerName="System.Data.SqlClient" />
      </connectionStrings>
      ```
      
      ATTENTION: If your SQL Server or SQL Azure is not running on the same server or desktop instance as your web application (for instance, you are testing the web  
      app locally in Visual Studio using IIS and the database service is one that you created in Azure), then make sure to whitelist your web app server IP (if you are 
      testing it locally, your local IP) on the firewall of your server providing SQL Server services. If you created an SQL Azure Database, then you can register the 
      IP directly on the SQL Database management page on the Azure Portal (it even detects your local IP for you).

2) Create a new Migration.

    Command: `add-migration InitialCreate` (to be executed in Nuget Package Manager Console.
    
    ATTENTION: 
    
    a) Make sure Migrations are enabled. If not, run `enable-migrations`;
    b) To create a new Migration, run `add-migration InitialCreate`, where InitialCreate is the name you give to the initial Migration;
    c) Make sure Automatic Migrations are enabled in the Configuration. Check the reference code below.



```
        internal sealed class Configuration : DbMigrationsConfiguration<Context>
        {
            public Configuration()
            {
                AutomaticMigrationsEnabled = true;
            }
        }
```


3) Generate database physical mode (scructure) from the project's Model classes in the DBSet. 
    For this step, open Package Manager Console and type `update-database -verbose`.

COMMENTS:

 Comments in code are in both English (marked by "EN-US") and Portuguese (marked by "PT-BR"). It was built in Portuguese, so the name of the entities are ins this 
 language. Nevertheless, you can see the translation of the entities' names and their properties' names inside the Model classes.

LEARNING:

 The easiest way to understand the whole system, its definitions and business rules is to start at Models/Caso.cs
