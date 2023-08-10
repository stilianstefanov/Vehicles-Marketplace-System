# ThinkElectric - Online marketplace system for electric vehicles

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-blue)

Welcome to ThinkElectric! Project for educational purposes.

## Table of Contents
- [Overview](#overview)
- [Technologies used](#technologiesused)
- [Requirements](#requirements)
- [Usage](#usage)
- [Configuration](#configuration)
- [Users](#users)

## Overview
- "Think Electric" is a web application with educational and demonstrative purposes. 
The project is an online marketplace for various types of electric scooters, bicycles and accessories. Users can register in 2 main ways - as buyers and as companies, there are different functionalities for the two types of users. The project also contains an admin area that facilitates control and maintenance of the application. There is also a forum area that allows users to communicate.

## Technologies used
<ul>
  <li>.NET Core 6.0</li>
  <li>ASP.NET Core</li>
  <li>Entity Framework Core</li>
  <li>HTML, CSS, Bootstrap</li>
  <li>MS SQL Server</li>
  <li>NUnit</li>
  <li>MongoDB</li>
  <li>JS</li>
</ul>

## Requirements
1. Microsoft Sql Server
2. MongoDb

## Usage
1. Update the configuration settings for the sql server and the mongoDb database
2. Update the Sql Server database according to the migrations
3. Run the project
4. The project will automatically seed some example data in both databases

## Users
-You can test the application with pre-seeded data using the following credentials:

### Administrator User
Username: admin@abv.bg
Password: 123456

### Company User
Username: companyuser1@abv.bg
Password: 123456

### Company User
Username: companyuser2@abv.bg
Password: 123456

### Regular User(Buyer)
Username: buyeruser@abv.bg
Password: 123456
