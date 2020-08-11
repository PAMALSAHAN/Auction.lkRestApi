IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200621100704_initMigration')
BEGIN
    CREATE TABLE [CategoryTbl] (
        [Id] int NOT NULL IDENTITY,
        [Type] nvarchar(max) NULL,
        CONSTRAINT [PK_CategoryTbl] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200621100704_initMigration')
BEGIN
    CREATE TABLE [UserTbl] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [ImageURL] nvarchar(max) NULL,
        CONSTRAINT [PK_UserTbl] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200621100704_initMigration')
BEGIN
    CREATE TABLE [vehiclesTbl] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Price] float NOT NULL,
        [Model] nvarchar(max) NULL,
        [Engine] nvarchar(max) NULL,
        [Color] nvarchar(max) NULL,
        [Company] nvarchar(max) NULL,
        [DatePosted] datetime2 NOT NULL,
        [IsHotandNew] bit NOT NULL,
        [IsFeatured] bit NOT NULL,
        [Location] nvarchar(max) NULL,
        [Condition] nvarchar(max) NULL,
        [UserId] int NOT NULL,
        [CategoryId] int NOT NULL,
        CONSTRAINT [PK_vehiclesTbl] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_vehiclesTbl_CategoryTbl_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [CategoryTbl] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_vehiclesTbl_UserTbl_UserId] FOREIGN KEY ([UserId]) REFERENCES [UserTbl] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200621100704_initMigration')
BEGIN
    CREATE TABLE [ImageTbl] (
        [Id] int NOT NULL IDENTITY,
        [ImageURL] nvarchar(max) NULL,
        [VehicleId] int NOT NULL,
        CONSTRAINT [PK_ImageTbl] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ImageTbl_vehiclesTbl_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [vehiclesTbl] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200621100704_initMigration')
BEGIN
    CREATE INDEX [IX_ImageTbl_VehicleId] ON [ImageTbl] ([VehicleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200621100704_initMigration')
BEGIN
    CREATE INDEX [IX_vehiclesTbl_CategoryId] ON [vehiclesTbl] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200621100704_initMigration')
BEGIN
    CREATE INDEX [IX_vehiclesTbl_UserId] ON [vehiclesTbl] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200621100704_initMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200621100704_initMigration', N'3.1.5');
END;

GO

