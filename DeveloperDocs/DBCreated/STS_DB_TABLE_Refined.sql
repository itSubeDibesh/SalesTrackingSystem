USE SalesTrackingSystem
GO
CREATE TABLE Batch(
	BatchID				BIGINT			PRIMARY KEY,	
	BatchName			VARCHAR(200)	NOT NULL,
	ProductCategoryId	BIGINT			NULL,			/*FK*/
	QunatityProduced	DECIMAL(10,2)	NOT NULL,
	UnitPrice			DECIMAL(10,2)	NOT NULL,
	StockLeft			BIGINT			NULL,
	DateProduced		DATETIME		NOT NULL,
	ExpiryDate			DATETIME		NOT NULL,
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	IsDeleted			BIT				NULL,
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
CREATE TABLE DistributonArea(
	DistributonAreaID	BIGINT			PRIMARY KEY,
	DistrubitorID		BIGINT			NULL,			/*FK*/
	State				VARCHAR(50)		NULL,
	District			VARCHAR(50)		NULL,
	Ciry				VARCHAR(50)		NULL,
	Address				VARCHAR(50)		NULL,
	Latitude			VARCHAR(50)		NULL,
	Longitude			VARCHAR(50)		NULL,
	IsDeleted			BIT				DEFAULT(0),	
	DateCreated			DATETIME		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL	
);
GO
CREATE TRIGGER Trigger_UPDATE_DistributonArea on DistributonArea FOR UPDATE AS            
BEGIN
    UPDATE DistributonArea
    SET DateUpdated=getdate()
    FROM DistributonArea INNER JOIN deleted d
    ON DistributonArea.DistributonAreaID = d.DistributonAreaID
END
GO
CREATE TABLE Module(
	ModuleID			BIGINT			PRIMARY KEY		IDENTITY(1,1),	
	ModuleName			VARCHAR(100)	NOT NULL		UNIQUE,
	ControllerName		VARCHAR(100)	NOT NULL		UNIQUE,
	ModuleStatus		BIT				DEFAULT(0),	
	Description			NVARCHAR(MAX)	NULL,
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	IsDeleted			BIT				NULL,
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	ModuleID		BIGINT			NULL,			/*FK*/
	ModuleActionID		BIGINT			NULL,			/*FK*/
	ProfileDetailStatus	BIT				NULL,
	Description			NVARCHAR(MAX)	NULL,
	CreatedBy			BIGINT			NULL,			/*FK*/
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
CREATE TABLE ExceptionUserProfile(
	ExceptionProfileID	BIGINT			PRIMARY KEY		IDENTITY(1,1),
	UserID				BIGINT			NULL,			/*FK*/
	ModuleID			BIGINT			NULL,			/*FK*/
	ModuleActionID		BIGINT			NULL,			/*FK*/
	ExceptionProfileStatus	BIT				NULL,
	Description			NVARCHAR(MAX)	NULL,
	CreatedBy			BIGINT			NULL,			/*FK*/
	DateCreated			DATETIME		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL
);
GO
CREATE TRIGGER Trigger_UPDATE_ExceptionUserProfile on ExceptionUserProfile FOR UPDATE AS            
BEGIN
    UPDATE ExceptionUserProfile
    SET DateUpdated=getdate()
    FROM ExceptionUserProfile INNER JOIN deleted d
    ON ExceptionUserProfile.ExceptionProfileID = d.ExceptionProfileID
END
GO
CREATE TABLE Users(
	UserID				BIGINT			PRIMARY KEY,
	UserProfileID		BIGINT			NULL,			/*FK*/
	DistrubitorID		BIGINT			NULL,			/*FK*/
	ExeceptionProfile   BIT				DEFAULT(0),
	FullName			VARCHAR(100)	NOT NULL,
	PasswordHash		VARCHAR(200)	NOT NULL,
	Email				VARCHAR(200)	NOT NULL		UNIQUE,	
	Token				VARCHAR(250)	NULL,	
	MobileNo			BIGINT			NOT NULL		UNIQUE,
	ImageString			VARCHAR(200)	NULL,
	UsersStatus			TINYINT			DEFAULT(1),		/* 1=Active, 2=Inactive, 2=Blocked*/
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	IsDeleted			BIT				NULL,
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
	DateCreated				DATETIME		DEFAULT			GETDATE(),
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


/*---------------------------------2	FK_DistributonArea_Distributors	-------------------------------------*/
ALTER TABLE DistributonArea
   ADD CONSTRAINT FK_DistributonArea_Distributors FOREIGN KEY (DistrubitorID)
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
   ADD CONSTRAINT FK_Products_ProductCategory FOREIGN KEY (ProductID)
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

/*---------------------------------10	FK_ExceptionUserProfile_SelectedUser	-------------------------------------*/
ALTER TABLE ExceptionUserProfile
   ADD CONSTRAINT FK_ExceptionUserProfile_SelectedUser FOREIGN KEY (UserID)
      REFERENCES Users (UserID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------11	FK_ExceptionUserProfile_ModuleAction	-------------------------------------*/
ALTER TABLE ExceptionUserProfile
   ADD CONSTRAINT FK_ExceptionUserProfile_ModuleAction FOREIGN KEY (ModuleActionID)
      REFERENCES ModuleAction (ModuleActionID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------12	FK_ExceptionUserProfile_Users	-------------------------------------*/
ALTER TABLE ExceptionUserProfile
   ADD CONSTRAINT FK_ExceptionUserProfile_Users FOREIGN KEY (CreatedBy)
      REFERENCES Users (UserID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO

/*---------------------------------13	FK_Users_UserProfile	-------------------------------------*/
ALTER TABLE Users
   ADD CONSTRAINT FK_Users_UserProfile FOREIGN KEY (UserProfileID)
      REFERENCES UserProfile (UserProfileID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO

/*---------------------------------14	FK_Users_Distributors	-------------------------------------*/
ALTER TABLE Users
   ADD CONSTRAINT FK_Users_Distributors FOREIGN KEY (DistrubitorID)
      REFERENCES Distributors (DistrubitorID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------15	FK_Resellers_Distributors	-------------------------------------*/
ALTER TABLE Resellers
   ADD CONSTRAINT FK_Resellers_Distributors FOREIGN KEY (DistrubitorID)
      REFERENCES Distributors (DistrubitorID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------16	FK_TransactionDetails_Transactions	-------------------------------------*/
ALTER TABLE TransactionDetails
   ADD CONSTRAINT FK_TransactionDetails_Transactions FOREIGN KEY (TransactionID)
      REFERENCES Transactions (TransactionID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------17	FK_TransactionDetails_Products	-------------------------------------*/
ALTER TABLE TransactionDetails
   ADD CONSTRAINT FK_TransactionDetails_Products FOREIGN KEY (ProductID)
      REFERENCES Products (ProductID)
      ON DELETE CASCADE
      ON UPDATE CASCADE
GO

/*---------------------------------18	FK_ExceptionUserProfile_Module	-------------------------------------*/
ALTER TABLE ExceptionUserProfile
   ADD CONSTRAINT FK_ExceptionUserProfile_Module FOREIGN KEY (ModuleID)
      REFERENCES Module (ModuleID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO


/*---------------------------------18	FK_UserProfileDetails_Module	-------------------------------------*/
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
	DateCreated			DATETIME		DEFAULT			GETDATE(),
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
/*---------------------------------19	FK_Verification_Users	-------------------------------------*/
ALTER TABLE Verification
   ADD CONSTRAINT FK_Verification_Users FOREIGN KEY (UserID)
      REFERENCES Users (UserID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
GO
