USE SalesTrackingSystem
GO
CREATE TABLE Batch(
	BatchID				BIGINT			PRIMARY KEY,	
	BatchName			VARCHAR(200)	NOT NULL,
	ProductCategoryId	BIGINT			NULL,			/*FK*/
	QunatityProduced	DECIMAL(10,2)	NOT NULL,
	UnitPrice			DECIMAL(10,2)	NOT NULL,
	StockLeft			BIGINT			NULL,
	DateProduced		NVARCHAR(10)	NOT NULL,
	ExpiryDate			NVARCHAR(10)	NOT NULL,
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_Batch on Batch FOR UPDATE AS            
BEGIN
    UPDATE Batch
    SET DateUpdated=getdate()
    FROM Batch INNER JOIN deleted d
    ON Batch.BatchID = d.BatchID
END
GO
CREATE TABLE Distributors(
	DistrubitorID		BIGINT			PRIMARY KEY		IDENTITY(1,1),
	DistrubitorName		VARCHAR(200)	NOT NULL,
	OwnerName			VARCHAR(200)	NOT NULL,
	RegestrationID		VARCHAR(200)	NOT NULL		UNIQUE,
	MobileNo			BIGINT			NOT NULL		UNIQUE,
	Phone				BIGINT			NOT NULL		UNIQUE,
	Fax					VARCHAR(40)		NULL			UNIQUE,
	Email				VARCHAR(200)	NOT NULL		UNIQUE,
	State				VARCHAR(50)		NULL,
	District			VARCHAR(50)		NULL,
	Address				VARCHAR(50)		NULL,
	Latitude			VARCHAR(50)		NULL,
	Longitude			VARCHAR(50)		NULL,	
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_Distributors on Distributors FOR UPDATE AS            
BEGIN
    UPDATE Distributors
    SET DateUpdated=getdate()
    FROM Distributors INNER JOIN deleted d
    ON Distributors.DistrubitorID = d.DistrubitorID
END
GO
CREATE TABLE DistributorArea(
	DistributorAreaID	BIGINT			PRIMARY KEY,
	DistrubitorID		BIGINT			NULL,			/*FK*/
	State				VARCHAR(50)		NULL,
	District			VARCHAR(50)		NULL,
	City				VARCHAR(50)		NULL,
	Address				VARCHAR(50)		NULL,
	Latitude			VARCHAR(50)		NULL,
	Longitude			VARCHAR(50)		NULL,	
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL	
);
GO
CREATE TRIGGER Trigger_UPDATE_DistributorArea on DistributorArea FOR UPDATE AS            
BEGIN
    UPDATE DistributorArea
    SET DateUpdated=getdate()
    FROM DistributorArea INNER JOIN deleted d
    ON DistributorArea.DistributorAreaID = d.DistributorAreaID
END
GO
CREATE TABLE Module(
	ModuleID			BIGINT			PRIMARY KEY		IDENTITY(1,1),	
	ModuleName			VARCHAR(100)	NOT NULL		UNIQUE,
	ControllerName		VARCHAR(100)	NOT NULL		UNIQUE,
	ModuleStatus		BIT				DEFAULT(0),	
	Description			NVARCHAR(MAX)	NULL,
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL	
);
GO
CREATE TRIGGER Trigger_UPDATE_Module on Module FOR UPDATE AS            
BEGIN
    UPDATE Module
    SET DateUpdated=getdate()
    FROM Module INNER JOIN deleted d
    ON Module.ModuleID = d.ModuleID
END
GO
CREATE TABLE ModuleAction(
	ModuleActionID		BIGINT			PRIMARY KEY		IDENTITY(1,1),
	ModuleID			BIGINT			NULL,			/*FK*/
	ActionName			VARCHAR(100)	NOT NULL,	
	ActionStatus		BIT				DEFAULT(0),	
	Description			NVARCHAR(MAX)	NULL,
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL	
);
GO
CREATE TRIGGER Trigger_UPDATE_ModuleAction on ModuleAction FOR UPDATE AS            
BEGIN
    UPDATE ModuleAction
    SET DateUpdated=getdate()
    FROM ModuleAction INNER JOIN deleted d
    ON ModuleAction.ModuleActionID = d.ModuleActionID
END
GO
CREATE TABLE ProductCategory(
	ProductCategoryID	BIGINT			PRIMARY KEY		IDENTITY(1,1),
	ProductCategoryName	VARCHAR(200)	NOT NULL,
	IsSubCategory		BIT				DEFAULT(0),
	SubCategoryOf		BIGINT			NULL,
	CategoryStatus		BIT				NULL,
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_ProductCategory on ProductCategory FOR UPDATE AS            
BEGIN
    UPDATE ProductCategory
    SET DateUpdated=getdate()
    FROM ProductCategory INNER JOIN deleted d
    ON ProductCategory.ProductCategoryID = d.ProductCategoryID
END
GO
CREATE TABLE Products(
	ProductID			BIGINT			PRIMARY KEY		IDENTITY(1,1),
	ProductCategoryID	BIGINT			NULL,			/*FK*/
	ProductName			VARCHAR(200)	NOT NULL,
	Description			NVARCHAR(MAX)	NULL,
	PackRate			DECIMAL(10,2)	NOT NULL,
	PackSize			DECIMAL(10,2)	NOT NULL,	
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_Products on Products FOR UPDATE AS            
BEGIN
    UPDATE Products
    SET DateUpdated=getdate()
    FROM Products INNER JOIN deleted d
    ON Products.ProductID = d.ProductID
END
GO
CREATE TABLE UserProfile(
	UserProfileID		BIGINT			PRIMARY KEY		IDENTITY(1,1),
	ProfileName			VARCHAR(200)	NOT NULL,
	UserProfileStatus	BIT				DEFAULT(0),	
	Description			NVARCHAR(MAX)	NULL,
	CreatedBy			BIGINT			NULL,			/*FK*/
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_UserProfile on UserProfile FOR UPDATE AS            
BEGIN
    UPDATE UserProfile
    SET DateUpdated=getdate()
    FROM UserProfile INNER JOIN deleted d
    ON UserProfile.UserProfileID = d.UserProfileID
END
GO
CREATE TABLE UserProfileDetails(
	UserProfileDetailID	BIGINT			PRIMARY KEY		IDENTITY(1,1),
	UserProfileID		BIGINT			NULL,			/*FK*/
	ModuleID			BIGINT			NULL,			/*FK*/
	ModuleActionID		BIGINT			NULL,			/*FK*/
	ProfileDetailStatus	BIT				NULL,
	Description			NVARCHAR(MAX)	NULL,
	CreatedBy			BIGINT			NULL,			/*FK*/
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_UserProfileDetails on UserProfileDetails FOR UPDATE AS            
BEGIN
    UPDATE UserProfileDetails
    SET DateUpdated=getdate()
    FROM UserProfileDetails INNER JOIN deleted d
    ON UserProfileDetails.UserProfileDetailID = d.UserProfileDetailID
END
GO
CREATE TABLE Users(
	UserID				BIGINT			PRIMARY KEY,
	UserProfileID		BIGINT			NULL,			/*FK*/
	DistrubitorID		BIGINT			NULL,			/*FK*/
	FullName			VARCHAR(100)	NOT NULL,
	PasswordHash		VARCHAR(200)	NOT NULL,
	Email				VARCHAR(200)	NOT NULL		UNIQUE,	
	Token				VARCHAR(250)	NULL,	
	MobileNo			BIGINT			NOT NULL		UNIQUE,
	ImageString			VARCHAR(200)	NULL,
	UsersStatus			Int				DEFAULT(1),		/* 1=Active, 2=Inactive, 2=Blocked*/
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_Users on Users FOR UPDATE AS            
BEGIN
    UPDATE Users
    SET DateUpdated=getdate()
    FROM Users INNER JOIN deleted d
    ON Users.UserID = d.UserID
END
GO
CREATE TABLE Resellers(
	ResellerID			BIGINT			PRIMARY KEY		IDENTITY(1,1),
	ResellerName		VARCHAR(200)	NOT NULL,
	OwnerName			VARCHAR(200)	NOT NULL,
	RegestrationID		VARCHAR(200)	NOT NULL		UNIQUE,
	DistrubitorID		BIGINT			NULL,
	Mobile				BIGINT			NULL			UNIQUE,
	Phone				BIGINT			NOT NULL		UNIQUE,	
	Email				VARCHAR(200)	NOT NULL		UNIQUE,
	State				VARCHAR(50)		NULL,
	District			VARCHAR(50)		NULL,
	Address				VARCHAR(50)		NULL,
	Latitude			VARCHAR(50)		NULL,
	Longitude			VARCHAR(50)		NULL,	
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_Resellers on Resellers FOR UPDATE AS            
BEGIN
    UPDATE Resellers
    SET DateUpdated=getdate()
    FROM Resellers INNER JOIN deleted d
    ON Resellers.ResellerID = d.ResellerID
END
GO
CREATE TABLE Transactions(
	TransactionID		BIGINT			PRIMARY KEY		IDENTITY(1,1),
	TransactionLevel	TINYINT			NOT NULL,	
	SupplierID			BIGINT			NOT NULL,
	ReceiverID			BIGINT			NOT NULL,	
	InvoiceNo			VARCHAR(100)	NOT NULL,
	InvoiceDate			DATE			NOT NULL,
	InvoiceEntryDate	DATETIME		DEFAULT			GETDATE(),	
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_Transactions on Transactions FOR UPDATE AS            
BEGIN
    UPDATE Transactions
    SET DateUpdated=getdate()
    FROM Transactions INNER JOIN deleted d
    ON Transactions.TransactionID = d.TransactionID
END
GO
CREATE TABLE TransactionDetails(
	TransactionDetailsID	BIGINT			PRIMARY KEY		IDENTITY(1,1),
	TransactionID			BIGINT			NULL,			/*FK*/	
	ProductID				BIGINT			NULL,			/*FK*/
	Quantity				DECIMAL(10,2)	NOT NULL,
	Units					DECIMAL(10,2)	NOT NULL,
	DateCreated				DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated				DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_TransactionDetails on TransactionDetails FOR UPDATE AS            
BEGIN
    UPDATE TransactionDetails
    SET DateUpdated=getdate()
    FROM TransactionDetails INNER JOIN deleted d
    ON TransactionDetails.TransactionDetailsID = d.TransactionDetailsID
END
GO

/*---------------------------------1	FK_Batch_ProductCategory	-------------------------------------*/
ALTER TABLE Batch
   ADD CONSTRAINT FK_Batch_ProductCategory FOREIGN KEY (ProductCategoryID)
      REFERENCES ProductCategory (ProductCategoryID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO


/*---------------------------------2	FK_DistributorArea_Distributors	-------------------------------------*/
ALTER TABLE DistributorArea
   ADD CONSTRAINT FK_DistributorArea_Distributors FOREIGN KEY (DistrubitorID)
      REFERENCES Distributors (DistrubitorID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------3	FK_ModuleAction_Module	-------------------------------------*/
ALTER TABLE ModuleAction
   ADD CONSTRAINT FK_ModuleAction_Module FOREIGN KEY (ModuleID)
      REFERENCES Module (ModuleID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------4	FK_ProductCategory_ProductCategory	-------------------------------------*/
ALTER TABLE ProductCategory
   ADD CONSTRAINT FK_ProductCategory_ProductCategory FOREIGN KEY (SubCategoryOf)
      REFERENCES ProductCategory (ProductCategoryID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO

/*---------------------------------5	FK_Products_ProductCategory	-------------------------------------*/
ALTER TABLE Products
   ADD CONSTRAINT FK_Products_ProductCategory FOREIGN KEY (ProductCategoryID)
      REFERENCES ProductCategory (ProductCategoryID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO


/*---------------------------------6	FK_UserProfile_Users	-------------------------------------*/
ALTER TABLE UserProfile
   ADD CONSTRAINT FK_UserProfile_Users FOREIGN KEY (CreatedBy)
      REFERENCES Users (UserID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO


/*---------------------------------7	FK_UserProfileDetails_UserProfile	-------------------------------------*/
ALTER TABLE UserProfileDetails
   ADD CONSTRAINT FK_UserProfileDetails_UserProfile FOREIGN KEY (UserProfileID)
      REFERENCES UserProfile (UserProfileID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------8	FK_UserProfileDetails_ModuleAction	-------------------------------------*/
ALTER TABLE UserProfileDetails
   ADD CONSTRAINT FK_UserProfileDetails_ModuleAction FOREIGN KEY (ModuleActionID)
      REFERENCES ModuleAction (ModuleActionID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------9	FK_UserProfileDetails_Users	-------------------------------------*/
ALTER TABLE UserProfileDetails
   ADD CONSTRAINT FK_UserProfileDetails_Users FOREIGN KEY (CreatedBy)
      REFERENCES Users (UserID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO


/*---------------------------------10	FK_Users_UserProfile	-------------------------------------*/
ALTER TABLE Users
   ADD CONSTRAINT FK_Users_UserProfile FOREIGN KEY (UserProfileID)
      REFERENCES UserProfile (UserProfileID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO

/*---------------------------------11	FK_Users_Distributors	-------------------------------------*/
ALTER TABLE Users
   ADD CONSTRAINT FK_Users_Distributors FOREIGN KEY (DistrubitorID)
      REFERENCES Distributors (DistrubitorID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------12	FK_Resellers_Distributors	-------------------------------------*/
ALTER TABLE Resellers
   ADD CONSTRAINT FK_Resellers_Distributors FOREIGN KEY (DistrubitorID)
      REFERENCES Distributors (DistrubitorID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------13	FK_TransactionDetails_Transactions	-------------------------------------*/
ALTER TABLE TransactionDetails
   ADD CONSTRAINT FK_TransactionDetails_Transactions FOREIGN KEY (TransactionID)
      REFERENCES Transactions (TransactionID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------14	FK_TransactionDetails_Products	-------------------------------------*/
ALTER TABLE TransactionDetails
   ADD CONSTRAINT FK_TransactionDetails_Products FOREIGN KEY (ProductID)
      REFERENCES Products (ProductID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------15	FK_UserProfileDetails_Module	-------------------------------------*/
ALTER TABLE UserProfileDetails
   ADD CONSTRAINT FK_UserProfileDetails_Module FOREIGN KEY (ModuleID)
      REFERENCES Module (ModuleID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO
CREATE TABLE Verification(
	VerificationID		BIGINT			PRIMARY KEY,
	UserID				BIGINT			NULL,	
	IsVerified			BIT				DEFAULT(0),
	DateVerified		DATETIME		NULL,	
	VerifiedToken		VARCHAR(200)	NULL,
	ResetToken			VARCHAR(200)	NULL,
	ResetTriggered		DATETIME		NULL,
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_Verification on Verification FOR UPDATE AS            
BEGIN
    UPDATE Verification
    SET DateUpdated=getdate()
    FROM Verification INNER JOIN deleted d
    ON Verification.VerificationID = d.VerificationID
END
GO
/*---------------------------------16	FK_Verification_Users	-------------------------------------*/
ALTER TABLE Verification
   ADD CONSTRAINT FK_Verification_Users FOREIGN KEY (UserID)
      REFERENCES Users (UserID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO
Alter TABLE Batch
ADD ProductID BIGINT NULL
GO
/*---------------------------------17	FK_Batch_Products	-------------------------------------*/
Alter TABLE Batch
ADD CONSTRAINT FK_Batch_Products FOREIGN KEY (ProductID)
      REFERENCES Products (ProductID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO

/*-------------------------------------------------------------------------------------------------*/
/*----------------------------------(March 22 2020) Dashboard Add ---------------------------------*/
/*-------------------------------------------------------------------------------------------------*/
CREATE TABLE Dashboard(
	DashboardID			BIGINT			PRIMARY KEY,
	UserID				BIGINT			NULL,/*FK*/	
	DashboardTableId	BIGINT			NULL,/*FK*/	
	DashboardTypeId		BIGINT			NULL,/*FK*/	
	ShowInHome			BIT				NOT NULL		DEFAULT(0),
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_Dashboard on Dashboard FOR UPDATE AS            
BEGIN
    UPDATE Dashboard
    SET DateUpdated=getdate()
    FROM Dashboard INNER JOIN deleted d
    ON Dashboard.DashboardID = d.DashboardID
END
GO
CREATE TABLE DashboardType(
	DashboardTypeID		BIGINT			PRIMARY KEY,
	TypeName			VARCHAR(100)	NOT NULL,	
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_DashboardType on DashboardType FOR UPDATE AS            
BEGIN
    UPDATE DashboardType
    SET DateUpdated=getdate()
    FROM DashboardType INNER JOIN deleted d
    ON DashboardType.DashboardTypeID = d.DashboardTypeID
END
GO
CREATE TABLE DashboardTable(
	DashboardTableId	BIGINT			PRIMARY KEY,
	TableName			VARCHAR(100)	NOT NULL,	
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_DashboardTable on DashboardTable FOR UPDATE AS            
BEGIN
    UPDATE DashboardTable
    SET DateUpdated=getdate()
    FROM DashboardTable INNER JOIN deleted d
    ON DashboardTable.DashboardTableId = d.DashboardTableId
END
GO
CREATE TABLE DashboardGivenColumn(
	DashboardGivenColumnId	BIGINT			PRIMARY KEY,
	DashboardTableId	BIGINT			NULL,/*FK*/	
	ColumnName			VARCHAR(100)	NOT NULL,	
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_DashboardGivenColumn on DashboardGivenColumn FOR UPDATE AS            
BEGIN
    UPDATE DashboardGivenColumn
    SET DateUpdated=getdate()
    FROM DashboardGivenColumn INNER JOIN deleted d
    ON DashboardGivenColumn.DashboardGivenColumnId = d.DashboardGivenColumnId
END
GO
CREATE TABLE DashboardColumn(
	DashboardColumnId	BIGINT			PRIMARY KEY,
	DashboardTableId	BIGINT			NULL,/*FK*/	
	DashboardID			BIGINT			NULL,/*FK*/	
	DashboardGivenColumnId			BIGINT			NULL,/*FK*/	
	Color				VARCHAR(100)	NOT NULL,
	DateCreated			DATETIME		NOT NULL		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_DashboardColumn on DashboardColumn FOR UPDATE AS            
BEGIN
    UPDATE DashboardColumn
    SET DateUpdated=getdate()
    FROM DashboardColumn INNER JOIN deleted d
    ON DashboardColumn.DashboardColumnId = d.DashboardColumnId
END
GO
/*---------------------------------18	FK_Dashboard_Users	-------------------------------------*/
Alter TABLE Dashboard
ADD CONSTRAINT FK_Dashboard_Users FOREIGN KEY (UserID)
      REFERENCES Users (UserID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO
/*---------------------------------19	FK_Dashboard_DashboardTable	-------------------------------------*/
Alter TABLE Dashboard
ADD CONSTRAINT FK_Dashboard_DashboardTable FOREIGN KEY (DashboardTableId)
      REFERENCES DashboardTable (DashboardTableId)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO
/*---------------------------------20	FK_Dashboard_DashboardType	-------------------------------------*/
Alter TABLE Dashboard
ADD CONSTRAINT FK_Dashboard_DashboardType FOREIGN KEY (DashboardTypeId)
      REFERENCES DashboardType (DashboardTypeId)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO
/*---------------------------------21	FK_DashboardColumn_DashboardTable	-------------------------------------*/
Alter TABLE DashboardColumn
ADD CONSTRAINT FK_DashboardColumn_DashboardTable FOREIGN KEY (DashboardTableId)
      REFERENCES DashboardTable (DashboardTableId)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO
/*---------------------------------22	FK_DashboardColumn_Dashboard	-------------------------------------*/
Alter TABLE DashboardColumn
ADD CONSTRAINT FK_DashboardColumn_Dashboard FOREIGN KEY (DashboardID)
      REFERENCES Dashboard (DashboardID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO
/*---------------------------------23	FK_DashboardGivenColumn_DashboardTable	-------------------------------------*/
Alter TABLE DashboardGivenColumn
ADD CONSTRAINT FK_DashboardGivenColumn_DashboardTable FOREIGN KEY (DashboardTableId)
      REFERENCES DashboardTable (DashboardTableId)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO
/*---------------------------------24	FK_DashboardColumn_DashboardGivenColumn	-------------------------------------*/
Alter TABLE DashboardColumn
ADD CONSTRAINT FK_DashboardColumn_DashboardGivenColumn FOREIGN KEY (DashboardGivenColumnId)
      REFERENCES DashboardGivenColumn (DashboardGivenColumnId)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO

/*------------------------------------ Inserting Datas ------------------------------------------------------------*/
GO

/*------------------------------------- DashboardType-------------------------------------------------------------*/
INSERT INTO DashboardType (DashboardTypeID,TypeName) VALUES(1,'Overview'),(2,'Recent report'),
(3,'Percent chart'),(4,'Bar chart'),(5,'Doughut chart'),(6,'Line chart'),(7,'Pie chart'),
(8,'Polar chart'),(9,'Single bar chart'),(10,'Radar chart'),(11,'Single line graph'),(12,'Multi line graph');
GO

/*------------------------------------- DashboardTable-------------------------------------------------------------*/
INSERT INTO DashboardTable (DashboardTableId,TableName) VALUES(1,'Distributors'),(2,'Products'),
(3,'Product Categories'),(4,'Batch'),(5,'Distribution area'),(6,'Resellers'),(7,'Transactions'),
(8,'Users'),(9,'User profile');
GO

/*------------------------------------- DashboardGivenColumn-------------------------------------------------------*/
INSERT INTO DashboardGivenColumn (DashboardGivenColumnId,DashboardTableId,ColumnName) values (1,4,'BatchName'),
(2,4,'UnitPrice'),(3,4,'DateProduced'),(4,4,'StockLeft'),(5,4,'QunatityProduced'),(6,8,'UsersStatus'),
(7,8,'UserProfile'),(8,9,'ProfileName');
GO

/*------------------------------------- UserProfile-------------------------------------------------------*/
INSERT INTO UserProfile (ProfileName,UserProfileStatus,Description,CreatedBy) 
VALUES('Developer',1,'This profile is made for developers to create and modify access.',1),
('Company',1,'This profile is made for company and can access everything except developers contents.',1),
('Distributor',1,'This profile allows distributor access and with limited control.',1);
GO

/*------------------------------------- Users-------------------------------------------------------------*/
INSERT INTO Users(UserID,UserProfileID,FullName,PasswordHash,Email,MobileNo,ImageString,UsersStatus) 
VALUES(1,NULL,'Dibesh Raj Subedi','2B84F11E7DE7DA72572486C6289A041E1E1E7292','kingraj530@gmail.com',9861315234,'/UserInformation/kingraj530@gmail.com/Images/22.jpg',1),
(2,NULL,'Dibesh Subedi','9048C6011D4BEFD8222807FD74CF861DC3BD71DD','dsubedi@ismt.edu.np',98613152340,'/UserInformation/dsubedi@ismt.edu.np/Images/77127096.jpg',2);

GO
/*------------------------------------- Verification-------------------------------------------------------------*/
INSERT INTO Verification (VerificationID,UserID,IsVerified,DateVerified,VerifiedToken,ResetToken,ResetTriggered)
VALUES(1,1,1,2020-03-14,'lq8xNiLPdrIDsiBeEKPFgHTA','DqiHGqRta_4kC_V@6123JmGcRyhXEXCvsB_cs7g3lAJOpXZv_',2020-03-14),
(2,2,1,2020-03-14,'SD$cn4K3t9slpi17$x6EHXUCC2ShekjLBdEzPd7Mmfr_ENIk','n7nPatFp$CIcMJADf$bHTz4wdVubazbW_OxZemXDD65BZu3t79jh7XiZd1Lp9DpAAWm7nLz2@6123JmGcRyhXEXCvsB_cs7g3lAJOpXZv_',2020-03-14);
