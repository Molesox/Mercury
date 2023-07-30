CREATE TABLE [Mercury].Address (
    AddressID INT PRIMARY KEY,
    PersonID INT,
    AddressTypeID INT,
    AddressLine1 VARCHAR(255),
    AddressLine2 VARCHAR(255),
    AddressLine3 VARCHAR(255),
    PostalCode VARCHAR(20),
    City VARCHAR(100),
    Region VARCHAR(100),
    Country VARCHAR(100),
    SortKey INT,
    Remarks TEXT,
    IsDefault BOOLEAN,
    FOREIGN KEY (AddressTypeID) REFERENCES AddressType(AddressTypeID)
);
