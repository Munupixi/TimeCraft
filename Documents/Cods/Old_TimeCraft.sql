CREATE DATABASE Time_Craft;

\c Time_Craft;

CREATE TABLE IF NOT EXISTS "User" (
    "UserId" SERIAL PRIMARY KEY,
    "Login" VARCHAR(50) UNIQUE NOT NULL,
    "Password" VARCHAR(20) NOT NULL,
    "Name" VARCHAR(20) NOT NULL,
    "Surname" VARCHAR(50),
    "Patronymic" VARCHAR(50),
    "Age" INT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Category" (
    "CategoryId" SERIAL PRIMARY KEY,
    "Title" VARCHAR(35) UNIQUE NOT NULL,
    "Description" TEXT, 
  "Color" VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS "Task" (
    "TaskId" SERIAL PRIMARY KEY,
    "Title" VARCHAR(35) UNIQUE NOT NULL,
    "Description" TEXT,
    "StartDate" DATE,
    "StartTime" TIME,
    "Repeat"  INT,
    "EndDate" DATE,
    "EndTime" TIME,
    "Priority" INT,
    "IsDone" BOOL NOT NULL,
    "CategoryId" INT,
    "UserId" INT NOT NULL,
    FOREIGN KEY ("CategoryId") REFERENCES "Category"("CategoryId"),
    FOREIGN KEY ("UserId") REFERENCES "User"("UserId")
);

CREATE TABLE IF NOT EXISTS "Event" (
    "EventId" SERIAL PRIMARY KEY,
    "Title" VARCHAR(35) UNIQUE NOT NULL,
    "Description" TEXT,
    "StartDate" DATE NOT NULL,
    "StartTime" TIME NOT NULL,
    "EndDate" DATE NOT NULL,
    "EndTime" TIME NOT NULL,
    "Location" VARCHAR(100),
    "DressCode" VARCHAR(35),
    "Priority" INT,
    "CategoryId" INT,
    "UserId" INT NOT NULL,
    FOREIGN KEY ("CategoryId") REFERENCES "Category"("CategoryId"),
    FOREIGN KEY ("UserId") REFERENCES "User" ("UserId")
);

CREATE TABLE IF NOT EXISTS "Participant" (
    "ParticipantId" SERIAL PRIMARY KEY,
    "IsAccepted" BOOL NOT NULL,
    "Role" VARCHAR(30),
    "IdEvent" INT NOT NULL,
    "IdUser" INT NOT NULL,
    FOREIGN KEY ("IdEvent") REFERENCES "Event"("EventId"),
    FOREIGN KEY ("IdUser") REFERENCES "User"("UserId")
);