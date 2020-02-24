USE SalesTrackingSystem
GO
CREATE TABLE Products(
	ProductID			BIGINT			PRIMARY KEY		IDENTITY(1,1),
	ProductionBatch		VARCHAR(50)		NOT NULL,
	ProductName			VARCHAR(200)	NOT NULL,
	QuantityProduced	BIGINT			NOT NULL,
	QunatitySupplied	BIGINT			NOT NULL,
	SellingPricePerUnit	BIGINT			NOT NULL,
	StockLeft			BIGINT			NULL,
	ISDeleted			BIT				DEFAULT(0),
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
CREATE TABLE Distributors(
	DistrubitorID		BIGINT			PRIMARY KEY		IDENTITY(1,1),
	DistrubitorName		VARCHAR(200)	NOT NULL,
	OwnerName			VARCHAR(200)	NOT NULL,
	RegestrationID		VARCHAR(200)	NOT NULL		UNIQUE,
	Contact				VARCHAR(20)		UNIQUE,
	Phone				VARCHAR(20)		NOT NULL		UNIQUE,
	Fax					VARCHAR(40)		NOT NULL		UNIQUE,
	Email				VARCHAR(200)	NOT NULL		UNIQUE,
	ISDeleted			BIT				DEFAULT(0),
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
CREATE TABLE DistributonAreas(
	DistributonAreaID	BIGINT			PRIMARY KEY,
	DistrubitorID		BIGINT,
	ProvinceNo			INT				NOT NULL,
	DistrictName		VARCHAR(50)		NOT NULL,
	CityName			VARCHAR(50)		NOT NULL,
	AreaName			VARCHAR(50)		NOT NULL,
	ISDeleted			BIT				DEFAULT(0),	
	DateCreated			DATETIME		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL,
	CONSTRAINT	FK_DistributonAreas_Distributors FOREIGN KEY (DistributonAreaID) REFERENCES Distributors (DistrubitorID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);
GO
CREATE TRIGGER Trigger_UPDATE_DistributonAreas on DistributonAreas FOR UPDATE AS            
BEGIN
    UPDATE DistributonAreas
    SET DateUpdated=getdate()
    FROM DistributonAreas INNER JOIN deleted d
    ON DistributonAreas.DistributonAreaID = d.DistributonAreaID
END
GO
CREATE TABLE DistributorOrders(
	DistributorOrderID	BIGINT			PRIMARY KEY,
	DistrubitorID		BIGINT,
	ProductName			VARCHAR(200)	NOT NULL,
	QunatityOrdered		BIGINT			NOT NULL,
	UnitPrice			BIGINT			NOT NULL,
	ISDeleted			BIT				DEFAULT(0),	
	DateCreated			DATETIME		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL,
	CONSTRAINT	FK_DistributorOrders_Distributors FOREIGN KEY (DistributorOrderID) REFERENCES Distributors (DistrubitorID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);
GO
CREATE TRIGGER Trigger_UPDATE_DistributorOrders on DistributorOrders FOR UPDATE AS            
BEGIN
    UPDATE DistributorOrders
    SET DateUpdated=getdate()
    FROM DistributorOrders INNER JOIN deleted d
    ON DistributorOrders.DistributorOrderID = d.DistributorOrderID
END
GO
CREATE TABLE Resellers(
	ResellerID			BIGINT			PRIMARY KEY		IDENTITY(1,1),
	ResellerName		VARCHAR(200)	NOT NULL,
	OwnerName			VARCHAR(200)	NOT NULL,
	RegestrationID		VARCHAR(200)	NOT NULL		UNIQUE,
	Contact				VARCHAR(20)		UNIQUE,
	Phone				VARCHAR(20)		NOT NULL		UNIQUE,
	Fax					VARCHAR(40)		NOT NULL		UNIQUE,
	Email				VARCHAR(200)	NOT NULL		UNIQUE,
	ISDeleted			BIT				DEFAULT(0),
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
CREATE TABLE DistributorSupplies(
	DistributorSupplyID	BIGINT			PRIMARY KEY,
	DistrubitorID		BIGINT,
	ResellerID			BIGINT,
	ProductName			VARCHAR(200)	NOT NULL,
	QunatitySupplied	BIGINT			NOT NULL,
	UnitPrice			BIGINT			NOT NULL,
	ISDeleted			BIT				DEFAULT(0),	
	DateCreated			DATETIME		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL,
	CONSTRAINT	FK_DistributorSupplies_Distributors FOREIGN KEY (DistributorSupplyID) REFERENCES Distributors (DistrubitorID)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	CONSTRAINT	FK_DistributorSupplies_Resellers FOREIGN KEY (DistributorSupplyID) REFERENCES Resellers (ResellerID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);
GO
CREATE TRIGGER Trigger_UPDATE_DistributorSupplies on DistributorSupplies FOR UPDATE AS            
BEGIN
    UPDATE DistributorSupplies
    SET DateUpdated=getdate()
    FROM DistributorSupplies INNER JOIN deleted d
    ON DistributorSupplies.DistributorSupplyID = d.DistributorSupplyID
END
GO
CREATE TABLE ResellerOrders(
	ResellerOrderID		BIGINT			PRIMARY KEY,
	DistrubitorID		BIGINT,
	ProductName			VARCHAR(200)	NOT NULL,
	QunatityOrdered		BIGINT			NOT NULL,
	UnitPrice			BIGINT			NOT NULL,
	ISDeleted			BIT				DEFAULT(0),	
	DateCreated			DATETIME		DEFAULT			GETDATE(),
	DateUpdated			DATETIME		NULL,
	CONSTRAINT	FK_ResellerOrders_Distributors FOREIGN KEY (ResellerOrderID) REFERENCES Distributors (DistrubitorID)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);
GO
CREATE TRIGGER Trigger_UPDATE_ResellerOrders on ResellerOrders FOR UPDATE AS            
BEGIN
    UPDATE ResellerOrders
    SET DateUpdated=getdate()
    FROM ResellerOrders INNER JOIN deleted d
    ON ResellerOrders.ResellerOrderID = d.ResellerOrderID
END
GO
CREATE TABLE Users(
	UserID				BIGINT			PRIMARY KEY,
	UserName			VARCHAR(50)		NOT NULL,
	PasswordHash		VARCHAR(400)	NOT NULL,
	Email				VARCHAR(200)	NOT NULL		UNIQUE,
	Phone				VARCHAR(20)		NOT NULL		UNIQUE,
	ImageString			VARCHAR(200)	NOT NULL,
	UserRole			TINYINT			NOT NULL,
	ReadAccess			BIT				DEFAULT(1),	
	CreateAccess		BIT				DEFAULT(0),
	UpdateAccess		BIT				DEFAULT(0),
	DeleteAccess		BIT				DEFAULT(0),
	ISDeleted			BIT				DEFAULT(0),	
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