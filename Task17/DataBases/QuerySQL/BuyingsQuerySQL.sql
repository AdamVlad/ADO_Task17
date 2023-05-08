CREATE TABLE Buyings
(
	ID int IDENTITY(1, 1) NOT NULL,
	FirstName NVARCHAR(20) NOT NULL,
	MiddleName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	PhoneNumber NVARCHAR(30) NOT NULL,
	Email NVARCHAR(30) NOT NULL	
)

SELECT * FROM Buyings

DELETE FROM Buyings WHERE ID = 2

INSERT INTO Buyings (FirstName, MiddleName, LastName, PhoneNumber, Email)
VALUES (N'Ника', N'Владиславовна', N'Брыкина', '8-917-397-81-81', 'Nika@mail.ru');

INSERT INTO Buyings (FirstName, MiddleName, LastName, PhoneNumber, Email)
VALUES (N'Максим', N'Игоревич', N'Амелин', '8-910-654-26-36', 'Maksim@mail.ru');

INSERT INTO Buyings (FirstName, MiddleName, LastName, PhoneNumber, Email)
VALUES (N'Ольга', N'Денисовна', N'Подшибякина', '8-929-753-11-11', 'Olga@mail.ru');

INSERT INTO Buyings (FirstName, MiddleName, LastName, PhoneNumber, Email)
VALUES (N'Андрей', N'Андреевич', N'Андреев', '8-999-999-99-99', 'Andrey@mail.ru');

INSERT INTO Buyings (FirstName, MiddleName, LastName, PhoneNumber, Email)
VALUES (N'Дмитрий', N'Валентинович', N'Даниленко', '8-987-654-32-31', 'Dima@mail.ru');