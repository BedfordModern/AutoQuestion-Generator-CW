START TRANSACTION;

CREATE TABLE IF NOT EXISTS Subsriptions(
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
    Subscription_Type INT REFERENCES Subscriptions(SubscriptionID),
    Subscription_Renew_Date DATE
);

CREATE TABLE IF NOT EXISTS Users(
	UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password TEXT NOT NULL,
    First_Name VARCHAR(50) NOT NULL,
    Last_Name VARCHAR(70) NOT NULL,
	OrganisationID INT REFERENCES Organisations(OrganisationID),
    Last_Logged_In DATETIME
);

CREATE TABLE IF NOT EXISTS Roles(
	RoleID INT AUTO_INCREMENT PRIMARY KEY,
    Role_Name VARCHAR(20) NOT NULL UNIQUE,
    Role_Description TEXT
);

CREATE TABLE IF NOT EXISTS UserRoles(
	UserRoleID INT AUTO_INCREMENT PRIMARY KEY,
	UserID INT REFERENCES Users(UserID),
    RoleID INT REFERENCES Roles(RoleID)
);

CREATE TABLE IF NOT EXISTS AccessTypes(
	AccessTypeID INT AUTO_INCREMENT PRIMARY KEY,
    AccessType_Name VARCHAR(30) NOT NULL,
    AccessType_Description TEXT
);

CREATE TABLE IF NOT EXISTS Groups(
	GroupID INT AUTO_INCREMENT PRIMARY KEY,
    Group_Name VARCHAR(50) NOT NULL,
    CreatedBy INT REFERENCES Users(UserID),
    AccessType INT REFERENCES AccessTypes(AccessTypeID)
);
CREATE TABLE IF NOT EXISTS GroupUsers(
	GroupUserID INT auto_increment PRIMARY KEY,
	UserID INT REFERENCES Users(UserID),
    GroupID INT REFERENCES Groups(GroupID)
);

CREATE TABLE IF NOT EXISTS QuestionTypes(
	TypeID INT AUTO_INCREMENT PRIMARY KEY,
    Type_Name VARCHAR(30) NOT NULL,
    Class VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS QuestionSets(
	QuestionSetID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT REFERENCES Users(UserID),
    Date_Asked DATETIME NOT NULL
);

CREATE TABLE IF NOT EXISTS Questions(
	QuestionID INT AUTO_INCREMENT PRIMARY KEY,
    Seed INT NOT NULL,
    Difficulty INT NOT NULL,
    Question_Type INT REFERENCES QuestionTypes(TypeID),
    QuestionSetID INT REFERENCES QuestionSets(QuestionSetID),
    AnswerCorrect BOOL
);

CREATE TABLE IF NOT EXISTS Worksets(
	WorksetID INT AUTO_INCREMENT PRIMARY KEY,
    GroupID INT REFERENCES Groups(GroupID),
	SetBy INT REFERENCES Users(UserID),
    Time_Allowed INT UNSIGNED,
    Date_Set DATE NOT NULL,
    Date_Due DATE
);

CREATE TABLE IF NOT EXISTS Work(
	WorkID INT AUTO_INCREMENT PRIMARY KEY,
    WorkSetID INT REFERENCES Worksets(WorksetID),
    Seed INT NOT NULL,
    QuestionType INT REFERENCES QuestionTypes(TypeID)
);

COMMIT;