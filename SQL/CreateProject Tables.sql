CREATE SCHEMA IF NOT EXISTS `question-database`;
USE `question-database`;
START TRANSACTION;

CREATE TABLE IF NOT EXISTS Subscriptions(
	SubscriptionID INT AUTO_INCREMENT PRIMARY KEY,
    Subsription_Length INT NOT NULL,
    Subscription_Name VARCHAR(50) NOT NULL,
    Subscription_Description TEXT
);

CREATE TABLE IF NOT EXISTS Organisations(
	OrganisationID INT AUTO_INCREMENT PRIMARY KEY,
    Organisation_Username VARCHAR(50) NOT NULL UNIQUE,
    Organisation_Password TEXT NOT NULL,
    Organisation_Name VARCHAR(100) NOT NULL,
    Subscription_Type INT,
    Subscription_Renew_Date DATE,
    FOREIGN KEY (Subscription_Type) REFERENCES Subscriptions(SubscriptionID)
);

CREATE TABLE IF NOT EXISTS Users(
	UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password TEXT NOT NULL,
    First_Name VARCHAR(50) NOT NULL,
    Last_Name VARCHAR(70) NOT NULL,
	OrganisationID INT,
    Last_Logged_In DATETIME,
    FOREIGN KEY (OrganisationID) REFERENCES Organisations(OrganisationID)
);

CREATE TABLE IF NOT EXISTS Roles(
	RoleID INT AUTO_INCREMENT PRIMARY KEY,
    Role_Name VARCHAR(20) NOT NULL UNIQUE,
    Role_Description TEXT
);

CREATE TABLE IF NOT EXISTS UserRoles(
	UserRoleID INT AUTO_INCREMENT PRIMARY KEY,
	UserID INT,
    RoleID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

CREATE TABLE IF NOT EXISTS AccessTypes(
	AccessTypeID INT AUTO_INCREMENT PRIMARY KEY,
    AccessType_Name VARCHAR(30) NOT NULL,
    AccessType_Description TEXT
);

CREATE TABLE IF NOT EXISTS Group_Table(
	GroupID INT AUTO_INCREMENT PRIMARY KEY,
    Group_Name VARCHAR(50) NOT NULL,
    CreatedBy INT,
    AccessType INT,
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (AccessType) REFERENCES AccessTypes(AccessTypeID)
);
CREATE TABLE IF NOT EXISTS GroupUsers(
	GroupUserID INT auto_increment PRIMARY KEY,
	UserID INT,
    GroupID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (GroupID) REFERENCES Group_Table(GroupID)
);

CREATE TABLE IF NOT EXISTS QuestionTypes(
	TypeID INT AUTO_INCREMENT PRIMARY KEY,
    Type_Name VARCHAR(30) NOT NULL,
    Class VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS SetTypes(
	SetType_ID INT AUTO_INCREMENT PRIMARY KEY,
    SetType_Name VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS Worksets(
	WorksetID INT AUTO_INCREMENT PRIMARY KEY,
    GroupID INT,
	SetBy INT,
    SetType INT,
    Time_Allowed INT UNSIGNED,
    Date_Set DATE NOT NULL,
    Date_Due DATE,
    FOREIGN KEY (GroupID) REFERENCES Group_Table(GroupID),
    FOREIGN KEY (SetBy) REFERENCES Users(UserID),
    FOREIGN KEY (SetType) REFERENCES SetTypes(SetType_ID)
);

CREATE TABLE IF NOT EXISTS Work(
	WorkID INT AUTO_INCREMENT PRIMARY KEY,
    WorkSetID INT,
	Difficulty INT,
    Seed INT NOT NULL,
    QuestionType INT,
    FOREIGN KEY (WorkSetID) REFERENCES Worksets(WorksetID),
    FOREIGN KEY (QuestionType)  REFERENCES QuestionTypes(TypeID)
);

CREATE TABLE IF NOT EXISTS QuestionSets(
	QuestionSetID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    WorkSetID INT NULL,
    Date_Asked DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (WorkSetID) REFERENCES Worksets(WorksetID)
);

CREATE TABLE IF NOT EXISTS Questions(
	QuestionID INT AUTO_INCREMENT PRIMARY KEY,
    Seed INT NOT NULL,
    Difficulty INT NOT NULL,
    Question_Type INT,
    QuestionSetID INT,
    AnswerCorrect BOOL,
    FOREIGN KEY (Question_Type) REFERENCES QuestionTypes(TypeID),
    FOREIGN KEY (QuestionSetID) REFERENCES QuestionSets(QuestionSetID)
);


COMMIT;