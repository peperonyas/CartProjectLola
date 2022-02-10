CREATE TABLE Products(
   ProductId int  NOT NULL 
  ,Quantity  int  NOT NULL
);

CREATE TABLE Cart(
   ProductId int  NOT NULL 
  ,Quantity  int  NOT NULL
);

INSERT INTO Products (ProductId, Quantity) VALUES(121123, 10);
INSERT INTO Products (ProductId, Quantity) VALUES(121124, 10);
INSERT INTO Products (ProductId, Quantity) VALUES(121125, 10);
INSERT INTO Products (ProductId, Quantity) VALUES(121126, 10);
INSERT INTO Products (ProductId, Quantity) VALUES(121127, 10);

CREATE OR REPLACE PROCEDURE CheckQuantityAndInsert (Id int, Cnt int)
LANGUAGE plpgsql
AS
$$
BEGIN
   IF EXISTS(SELECT 1 FROM Products WHERE ProductId = Id and Quantity >= Cnt) THEN
       INSERT INTO Cart (ProductId, Quantity) VALUES(Id, Cnt);
       SELECT  1;
   END IF;
        SELECT 0;
END
$$;

