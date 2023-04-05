Case Control is a web app build with Asp.Net MVC (.Net Framework 4.7.1) that allows for medical statistical data gathering. It allows doctors to: 

a) report collateral effects that their patients seemed to notice after receiving vaccination doses; and 

b) browse aggregated reports on the collective data input.

* This project is useful for a myriad of applications in which random events and their side effects need to be reported and aggregated.
* The naming of the domain classes and its list of properties is related to the specific medical domain for which it was created, but a simple 
* refactoring can turn this project into a generic version of itself or a specific version for another domain.

The functionalities include:

A) For doctors:

* User Sign-Up and Access Control;
* Register Case:
    1. Patient medical information and medica history, with no patient identification data);
    2. Patient's vaccination doses received;
    3. Patient's reactions perceived;
* Aggregated Statistical Reports;

B) For system administrators:

* Laboratory Registration;
* Reaction Type Registration (for instance, "headache");
* Group of Reaction Type Registration (for instance, "mild");
* Country, State and City Registration.

C) For users:

* User sign-up;
* Access control.

To report patient's reactions, the doctor is presented with a step-by-step registration process.

The development of this app was requested by a freelance client. It doesn't represent any political or scientific view of this developer towards vaccination.

It is being shared in Github as a showcase of my work, since this was a job that I have executed alone. It is also being shared for academic purposes and it is small enough
to be able to be comprehended end-to-end pretty quickly. The duration of development was also small.

Tech steps included:

* Creating a Template Asp.Net MVC app;
* Bulding the Model and inserting user-friendly Strings to be used in UI with Property Attributes.
* Enabling and configuring Migrations;
* Scaffolding Controllers and Views;
* Creating a Database in SQL Azure;
* Creating and Updating database schema with Nuget Console commmand-line prompts (using Migrations);
* Customizing Controllers with business rules, validations and workflows;
* Customizing Views with workflows and validations.

UPGRADES:

As a small MVP-like product resulting of a short deadline project, it showcases basic architecture. It can be refactored for further growth 
using CQRS, SOLID principles, CLEAN architecture principles and so on.

On a very specific note, some Yes/No/Unaware or Mild/Moderate/Severe properties were represented as Integers. It would be a best practice to turn them into Enums in further versions.

As an MVP, this product used bare bones tamplate designs with Bootstrap. There is no custom design or branding. You can upgrade that.

Feel free to ask any questions and to contribute.

USAGE:

In root file Web.config, insert the connection string to a SQL Azure / SQL Server Database:

  <connectionStrings>
    <add name="DefaultConnection" connectionString="[INSERT CONNECTION STRING HERE, WITHOUT BRACKETS]"
      providerName="System.Data.SqlClient" />
  </connectionStrings>

  If your SQL Server or SQL Azure is not running on the same server or desktop instance as your web application (for instance, 
  you are testing the web app in Visual Studio using IIS and the database service is one that you created in Azure), then 
  you may want to whitelist your local IP in the database server (or desktop) instance firewall (in Azure, you can do it directly on the SQL 
  database configuration).

 COMMENTS:

  Comments in code are in both English (marked by "EN-US") and Portuguese (marked by "PT-BR"). It was built in Portuguese, so the name of the entities are ins this 
  language. Nevertheless, you can see the translation of the entities' names and their properties' names inside the Model classes.

 LEARNING:

  The easiest way to understand the whole system, its definitions and business rules is to start at Models/Caso.cs
