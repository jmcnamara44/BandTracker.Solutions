# _BandTracker_

#### _This webpage will build a database for to hold bands and venues, and the different combinations of both. 05/11/18_

#### By _**Jimmy McNamara**_

## Description

_This project will use C# to build and work with databases for a band-venue tracker.  In this database there will be a many-to-many relationship between 2 tables, bands and venues, that is stored in a join table. Users will have the option to add new bands to each venue, and new venues to each band._

## Project Specs

_Webpage makes a valid connection with the database for each table._
_Users can add a new concert venue._
_Users can add a new band._
_Webpage makes a connection with the join table with venue class._
_Webpage makes a connection with the join table with band class._
_Users can add a new concert venue to a specific band._
_Users can add a new band to a specific concert venue._
_Users can see a list of all concert and band venue combinations._
_Users can update a band they have entered._
_Users can update a concert venue they have entered._
_Users can delete a band they have entered._
_Users can delete a concert venue they have entered._

## Setup/Installation Requirements

* _Clone repository from GitHub_
* _Start the MAMP servers._
* _Navigate into the project folder and run 'dotnet restore'._
* _Run the command 'dotnet run'._
* _Type 'http://localhost:5000' into your web browser._
* _CREATE DATABASE band_tracker;_
* _CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR(50));_
* _CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(50));_
* _CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id INT(11), venue_id INT(11));_

## Known Bugs

_No known bugs as of now._

## Support and contact details

_Contact Jimmy McNamara with any questions or comments_

## Technologies Used

_HTML_
_CSS_
_Bootstrap_
_JavaScript_
_jQuery_
_CSharp_

### License

*Licensed through the MIT open resource agreement*

Copyright (c) 2018 **_Jimmy McNamara_**
