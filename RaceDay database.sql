--Creating a datase--
Create database RaceDayDB;
Use RaceDayDB;

--Creating Tables of the database--
Create Table Organiser
( 
   OrganiserID INT Not Null,
   OrganiserName Varchar(50) Not Null,
   ContactName Varchar(100) Not Null,
   Email Varchar(100) Not Null UNIQUE,

   Constraint PK_Organiser
   Primary Key (OrganiserID)
);

Create Table Event
(
   EventID INT Not Null,
   OrganiserID INT Not Null,
   EventName Varchar(100) Not Null UNIQUE,
   EventDate Date Not Null,

   Constraint PK_Event 
   Primary key (EventID),

   Constraint FK_Event_Organiser
   Foreign Key (OrganiserID)
   References Organiser(OrganiserID)
);
Create table Route
( 
  RouteID INT Not Null,
  EventID INT Not Null,
  Distancekm Decimal(4,2) Not Null,
  RouteMap Varchar(255) Not Null,
  Constraint PK_Route 
  Primary Key (RouteID),

  Constraint Fk_Route_Event
  Foreign key (EventID)
  References Event(EventID)

  
);

Create Table Participant
(
  ParticipantID INT Not Null,
  FullName Varchar(100) Not Null,
  Email Varchar(100) Not Null Unique,
  Gender Varchar(10) Not Null,

  Constraint PK_Participant
  Primary Key (ParticipantID)

);
Create Table Category
( 
  CategoryID INT Not Null,
  EventID INT Not Null ,
  CategoryName Varchar(100) Not Null ,
  CategoryType Varchar(50) Not Null,
  AgeGroup Varchar(20) Not Null,

  Constraint PK_Category
  Primary Key(CategoryID),

  Constraint FK_Category_Event
  Foreign Key (EventID)
  References Event(EventID)
);

Create Table Entry
(
  EntryID INT Not Null,
  ParticipantID INT Not Null,
  EventID INT Not Null,
  CategoryID INT Not Null,
  EntryDate Date Not Null Default GetDate(),
  Status Varchar(20) Not Null Default 'Pending',

  Constraint PK_Entry
  Primary Key (EntryID),

  Constraint FK_Entry_Participant
  Foreign Key (ParticipantID)
  References Participant(ParticipantID),

  Constraint FK_Entry_Event
  Foreign Key (EventID)
  References Event(EventID),

  
  Constraint FK_Entry_Category
  Foreign Key (CategoryID)
  References Category(CategoryID)
);
Create Table Results
(
  ResultsID INT Not Null,
  EntryID INT Not Null UNIQUE,
  FinishTime TIME Not Null,
  Pace Decimal(5,2) Not Null,

  Constraint PK_Results
  Primary Key (ResultsID),

  Constraint Fk_Results_Entry
  Foreign Key (EntryID)
  References Entry(EntryID)
);
--Insert Function--
Insert Into Organiser
(OrganiserID, OrganiserName, ContactName, Email)
Values
(1, 'South African RaceDay Event', 'Njabulo Ndlovu', 'njabulo@ndlovu.co.za'),
(2, 'Running Association', 'Lesego Leqola', 'lesego@leqola.co.za');


Insert Into Event
(EventID, OrganiserID, EventName, EventDate)
Values
(1,1, 'City Run Of Johannesburg', '2026-09-27'),
(2,1, 'Heritage Marathon' , '2026-10-13'),
(3,2, 'Marathon Challenge', '2026-11-10');

Insert Into Route
(RouteID, EventID, Distancekm,RouteMap)
values
(1,1, 10.01, 'Maboneng to Constitution Hill'),
(2,2, 15.04, 'FNB Stadium to Orlando Stadium'),
(3,3, 21.13, 'South Gate to Mall of the South');

Insert Into Participant
(ParticipantID, FullName, Email, Gender)
values
(1,'Zinhle Ngubane', 'z.ngubane@gmail.com', 'Female'),
(2,'Prince Molapo', 'princemolapo2@gmail.com', 'Male');

Insert Into Category
(CategoryID, EventID, CategoryName,CategoryType, AgeGroup)
values
(1, 1, '20 km Open', 'RoadWork','18-35'),
(2 ,1 ,'15 km Masters', 'RoadWork','25-45'),
(3 ,2 ,'Half of Marathon is Open', 'RoadWork','40+'),
(4 ,2 ,'15 km Open', 'RoadWork','18-39'),
(5 ,3,'20 km Master', 'RoadWork','18-20'),
(6,3,'Half of  Masters', 'RoadWork','16-17');

Insert Into Entry
(EntryID, ParticipantID, EventID, CategoryID,EntryDate, Status)
values
(1,1,1,1, '2026-09-04', 'Approved'),
(2,2,1,1, '2026-09-04', 'Approved'),
(3,1,2,3, '2026-09-04', 'Approved'),
(4,2,2,3, '2026-09-04', 'Approved'),
(5,1,3,5, '2026-09-04', 'Approved');


Insert into Results
(ResultsID, EntryID, FinishTime, Pace)
Values
(1,1, '23:52:25',6.25),
(2,2, '23:48:20',5.83);

--View Data in the tables--
Select * From Organiser;
Select * From Event;
Select * From Route;
Select * From Participant;
Select * From Category;
Select * From Entry;
Select * From Results;

Alter table Organiser
Add PasswordHash Varchar(255) not Null
default'';

Select * from Organiser;