GO
CREATE DATABASE SwordChallenge

GO
USE [SwordChallenge]

GO
CREATE TABLE [dbo].[Role]
(
	RoleId bigint IDENTITY (1,1), 
    RoleName nvarchar(50) NOT NULL,
	Deleted bit DEFAULT 0 NOT NULL,
	CONSTRAINT PK_ROLE PRIMARY KEY (RoleId),
);

INSERT INTO [dbo].[Role]
			([RoleName])
	VALUES
			('Manager'),('Technician')

GO
CREATE TABLE [dbo].[ApplicationUser]
(
	ApplicationUserId bigint IDENTITY (1,1), 
	RoleId bigint NOT NULL,
    FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	UserName nvarchar(50) NOT NULL,
	Password nvarchar(50) NOT NULL,
	Deleted bit DEFAULT 0 NOT NULL,
	CONSTRAINT PK_USER PRIMARY KEY (ApplicationUserId),
	CONSTRAINT FK_USER_ROLE FOREIGN KEY (RoleId) REFERENCES [dbo].[Role](RoleId),
);

INSERT INTO [dbo].[ApplicationUser]
			([RoleId], [FirstName], [LastName], [UserName], [Password])
	VALUES
			(1, 'Joaquim', 'Fonseca', 'manager', 'manager'),(2, 'Tiago', 'Meireles', 'tech', 'tech')

GO
CREATE TABLE [dbo].[Task]
(
	TaskId bigint IDENTITY (1,1), 
    ApplicationUserId bigint NOT NULL,
	Summary nvarchar(2500) NOT NULL,
    PerformedDate datetime2 NULL,
	CreatedAt datetime2 NOT NULL,
	CreateUser bigint NOT NULL,
	UpdatedAt datetime2 NULL,
	UpdateUser bigint NULL,
	DeletedAt datetime2 NULL,
	DeleteUser bigint NULL,
	Deleted bit DEFAULT 0 NOT NULL,
	CONSTRAINT PK_TASK PRIMARY KEY (TaskId),
);

CREATE TABLE [dbo].[Notification]
(
	NotificationId bigint IDENTITY (1,1),
	TaskId bigint NOT NULL,
	Message nvarchar(MAX),
	IsRead bit DEFAULT 0,
	CreatedAt datetime2 NOT NULL,
	CreateUser bigint NOT NULL,
	UpdatedAt datetime2 NULL,
	UpdateUser bigint NULL,
	DeletedAt datetime2 NULL,
	DeleteUser bigint NULL,
	Deleted bit DEFAULT 0 NOT NULL,
	CONSTRAINT PK_NOTIFICATION PRIMARY KEY (NotificationId)
);