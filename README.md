# ThinkElectric - Online marketplace system for electric vehicles

![License](https://img.shields.io/badge/license-MIT-blue)

Welcome to ThinkElectric! Project for educational purposes.

## Table of Contents
- [Overview](#overview)
- [Technologies](#technologies)
- [Requirements](#requirements)
- [Usage](#usage)
- [Users](#users)
- [DatabaseDiagram](#databasediagram)
- [Achievements](#achievements)

## Overview
- "Think Electric" is a web application with educational and demonstrative purposes. 
The project represents an online marketplace for different types of electric scooters, bicycles and accessories. Users can register in two main ways - as buyers and as companies. There are various types of functionalities for the two types of users. The project utilizes Ajax for dynamic filtering and pagination, and EP Plus for generating Excel files. It also includes an admin area that facilitates control and maintenance of the application, and a forum area that allows users to communicate.

The application is deployed at: https://thinkelectric.azurewebsites.net/

## Technologies
<ul>
  <li>.NET Core 6.0</li>
  <li>ASP.NET Core</li>
  <li>Entity Framework Core</li>
  <li>HTML, CSS, Bootstrap</li>
  <li>MS SQL Server</li>
  <li>NUnit</li>
  <li>MongoDB</li>
  <li>JS</li>
  <li>Ajax</li>
  <li>Moq</li>
  <li>EP Plus (.NET library for managing Excel files)</li>
</ul>

## Requirements
1. Microsoft Sql Server
2. MongoDb

## Usage
1. Update the configuration settings for the MSSQL and the MongoDb database
2. Update the MSSQL database according to the migrations
3. Run the project
4. The project will automatically seed some example data in both databases

## Users
-The application can be tested with pre-seeded data using the following credentials:

### Administrator User
Username: admin@abv.bg </br>
Password: 123456

### Company User
Username: companyuser1@abv.bg </br>
Password: 123456

### Company User
Username: companyuser2@abv.bg </br>
Password: 123456

### Regular User(Buyer)
Username: buyeruser@abv.bg  </br>
Password: 123456

## DatabaseDiagram
![Diagram](https://github.com/stilianstefanov/ThinkElectric_ElectricVehiclesMarketPlaceSystem/blob/main/assets/DatabaseDiagram.png)

## Achievements
- Excellent grade in C# MVC Frameworks - ASP.NET Advanced course
    
![Certificate](https://github.com/stilianstefanov/ThinkElectric_ElectricVehiclesMarketPlaceSystem/blob/main/assets/ASP.NET%20Advanced%20-%20June%202023%20-%20Certificate.jpeg)
