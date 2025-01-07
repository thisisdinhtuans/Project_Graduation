IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [AppRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AppRoleClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [AppRoles] (
        [Id] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NULL,
        [NormalizedName] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AppRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [AppUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AppUserClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [AppUserLogins] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(max) NULL,
        [ProviderKey] nvarchar(max) NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        CONSTRAINT [PK_AppUserLogins] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [AppUserRoles] (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AppUserRoles] PRIMARY KEY ([UserId], [RoleId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [AppUsers] (
        [Id] uniqueidentifier NOT NULL,
        [FullName] nvarchar(200) NOT NULL,
        [Dob] datetime2 NOT NULL,
        [RefreshToken] nvarchar(max) NULL,
        [RestaurantID] int NOT NULL,
        [Status] int NOT NULL,
        [Gender] bit NULL,
        [CCCD] nvarchar(max) NULL,
        [RecoveryToken] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [NormalizedUserName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [NormalizedEmail] nvarchar(max) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AppUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [AppUserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AppUserTokens] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Blogs] (
        [BlogID] int NOT NULL IDENTITY,
        [Image] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [SubTitle] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Status] bit NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Blogs] PRIMARY KEY ([BlogID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Categories] (
        [IdCategory] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([IdCategory])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Modules] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Order] int NOT NULL,
        [IsShow] bit NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Modules] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Operations] (
        [Id] int NOT NULL IDENTITY,
        [ModuleId] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Url] bigint NOT NULL,
        [Code] nvarchar(max) NULL,
        [IsShow] bit NOT NULL,
        [Icon] bigint NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Operations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Orders] (
        [OrderId] int NOT NULL IDENTITY,
        [RestaurantID] int NOT NULL,
        [UserName] nvarchar(max) NOT NULL,
        [PriceTotal] float NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Date] datetime2 NOT NULL,
        [Time] time NOT NULL,
        [From] datetime2 NOT NULL,
        [To] datetime2 NOT NULL,
        [NumberOfCustomer] int NOT NULL,
        [TableID] int NOT NULL,
        [Payment] float NOT NULL,
        [VAT] float NOT NULL,
        [Phone] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [Deposit] bit NOT NULL,
        [Discount] float NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Restaurants] (
        [RestaurantID] int NOT NULL IDENTITY,
        [Address] nvarchar(max) NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Restaurants] PRIMARY KEY ([RestaurantID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [RoleOperations] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(max) NULL,
        [OperationId] int NOT NULL,
        [IsAccess] bit NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_RoleOperations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [UserOperations] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [OperationId] bigint NOT NULL,
        [IsAccess] bit NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_UserOperations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Dishes] (
        [DishId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Price] float NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [CategoryID] int NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Dishes] PRIMARY KEY ([DishId]),
        CONSTRAINT [FK_Dishes_Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Categories] ([IdCategory]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [OrderDetails] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Price] float NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [DishId] int NOT NULL,
        [NumberOfCustomer] int NOT NULL,
        [Quantity] int NOT NULL,
        [OrderId] int NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Areas] (
        [AreaID] int NOT NULL IDENTITY,
        [AreaName] nvarchar(max) NOT NULL,
        [RestaurantID] int NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Areas] PRIMARY KEY ([AreaID]),
        CONSTRAINT [FK_Areas_Restaurants_RestaurantID] FOREIGN KEY ([RestaurantID]) REFERENCES [Restaurants] ([RestaurantID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [Tables] (
        [TableID] int NOT NULL IDENTITY,
        [TableNumber] int NOT NULL,
        [Status] int NOT NULL,
        [NumberOfDesk] nvarchar(max) NOT NULL,
        [AreaID] int NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_Tables] PRIMARY KEY ([TableID]),
        CONSTRAINT [FK_Tables_Areas_AreaID] FOREIGN KEY ([AreaID]) REFERENCES [Areas] ([AreaID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE TABLE [OrderTables] (
        [TableID] int NOT NULL,
        [OrderId] int NOT NULL,
        [CreatedBy] nvarchar(300) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedBy] nvarchar(300) NULL,
        [UpdatedDate] datetime2 NULL,
        CONSTRAINT [PK_OrderTables] PRIMARY KEY ([OrderId], [TableID]),
        CONSTRAINT [FK_OrderTables_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderTables_Tables_TableID] FOREIGN KEY ([TableID]) REFERENCES [Tables] ([TableID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE INDEX [IX_Areas_RestaurantID] ON [Areas] ([RestaurantID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE INDEX [IX_Dishes_CategoryID] ON [Dishes] ([CategoryID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE INDEX [IX_OrderDetails_OrderId] ON [OrderDetails] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE INDEX [IX_OrderTables_TableID] ON [OrderTables] ([TableID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    CREATE INDEX [IX_Tables_AreaID] ON [Tables] ([AreaID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014155029_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241014155029_InitialCreate', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014163248_SeedDât')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdCategory', N'CreatedBy', N'CreatedDate', N'Name', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] ON;
    EXEC(N'INSERT INTO [Categories] ([IdCategory], [CreatedBy], [CreatedDate], [Name], [UpdatedBy], [UpdatedDate])
    VALUES (1, N''Admin'', ''2023-12-25T00:00:00.0000000'', N''HẢI SẢN'', NULL, NULL),
    (2, N''Admin'', ''2023-12-25T00:00:00.0000000'', N''ĐỒ NƯỚNG'', NULL, NULL),
    (3, N''Admin'', ''2023-12-25T00:00:00.0000000'', N''CÁ CÁC MÓN'', NULL, NULL),
    (4, N''Admin'', ''2023-12-25T00:00:00.0000000'', N''MÓN ĂN CHƠI'', NULL, NULL),
    (5, N''Admin'', ''2023-12-25T00:00:00.0000000'', N''MÓN NHẬU'', NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdCategory', N'CreatedBy', N'CreatedDate', N'Name', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Categories]'))
        SET IDENTITY_INSERT [Categories] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014163248_SeedDât')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DishId', N'CategoryID', N'CreatedBy', N'CreatedDate', N'Description', N'Image', N'Name', N'Price', N'Type', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Dishes]'))
        SET IDENTITY_INSERT [Dishes] ON;
    EXEC(N'INSERT INTO [Dishes] ([DishId], [CategoryID], [CreatedBy], [CreatedDate], [Description], [Image], [Name], [Price], [Type], [UpdatedBy], [UpdatedDate])
    VALUES (1, 1, N''Admin'', ''2023-12-25T00:00:00.0000000'', CONCAT(CAST(N''Tên món: Tôm chiên hoàng kim'' AS nvarchar(max)), nchar(13), nchar(10), N''Định lượng: 1 suất 2-4 người ăn'', nchar(13), nchar(10), N''Mô tả: Tôm chiên hoàng kim là một trong những món nhậu cực kỳ tốn bia tại Quán Nhậu Tự Do.'', nchar(13), nchar(10), N''Tôm tươi, căng mọng đem chiên vàng giòn rồi lại đảo qua gia vị đậm đà tạo nên món ăn hấp dẫn, ăn một miếng là không ngừng lại được.''), N''https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/20240424094950598.webp'', N''Tôm chiên hoàng kim'', 225000.0E0, N''Suất'', NULL, NULL),
    (2, 1, N''Admin'', ''2023-12-25T00:00:00.0000000'', CONCAT(CAST(N''Tên món: Tôm sú sốt ớt pattaya'' AS nvarchar(max)), nchar(13), nchar(10), N''Định lượng: 1 suất cho 3-4 người ăn'', nchar(13), nchar(10), N''Mô tả: Tôm sú sốt ớt pattaya là một trong những món ăn mới lạ không thể bỏ qua khi ghé Quán Nhậu Tự Do.'', nchar(13), nchar(10), N''Thịt tôm sú dai giòn ăn cùng rau củ, thấm đẫm nước sốt ớt pattaya “thần thánh" xen lẫn vị chua chua, ngọt ngọt, cay nồng chắc chắn sẽ làm hài lòng mọi thực khách.''), N''https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/06/202406051717238266.webp'', N''Tôm sú sốt ớt pattaya'', 225000.0E0, N''Suất'', NULL, NULL),
    (3, 2, N''Admin'', ''2023-12-25T00:00:00.0000000'', CONCAT(CAST(N''Tên món: Lợn mán nướng mắc khén'' AS nvarchar(max)), nchar(13), nchar(10), N''Định lượng: 1 suất cho 3-4 người ăn'', nchar(13), nchar(10), N''Mô tả: Lợn mán nướng mắc khén là một trong những món nhậu kích thích vị giác của thực khách.'', nchar(13), nchar(10), N''Thịt lợn mềm ngọt, có chút dai dai, không quá mỡ được nướng cùng hạt mắc khén thơm lừng. Sự hòa trộn giữa vị đậm đà của thịt lợn mán cùng các loại rau củ ăn kèm mang đến cho món ăn hương vị tuyệt hảo không thể nào quên.'', nchar(13), nchar(10)), N''https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/202404231915283755.webp'', N''Lợn mán nướng mắc khén'', 185000.0E0, N''Suất'', NULL, NULL),
    (4, 2, N''Admin'', ''2023-12-25T00:00:00.0000000'', CONCAT(CAST(N''Tên món: Gà đen nướng mắc khén'' AS nvarchar(max)), nchar(13), nchar(10), N''Định lượng: Gà nửa con, hạt mắc khén rang thơm, quả ớt cay, lá chanh, muối,củ sả.'', nchar(13), nchar(10), N''Mô tả: Chuẩn gia vị chẩm chéo để chấm gà, thơm phức mùi mắc kén và tê tê đầu lưỡi.''), N''https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/05/20240530101506515.webp'', N''Gà đen nướng mắc khén'', 300000.0E0, N''Suất'', NULL, NULL),
    (5, 5, N''Admin'', ''2023-12-25T00:00:00.0000000'', CONCAT(CAST(N''Tên món: Gà H’Mong rang muối'' AS nvarchar(max)), nchar(13), nchar(10), N''Định lượng: 800g'', nchar(13), nchar(10), N''Mô tả: Gà H’Mong rang muối là món ăn được nhiều anh em sành ăn yêu thích tại Quán Nhậu Tự Do.'', nchar(13), nchar(10), N''Gà H’Mong được tuyển chọn từ giống gà đen nuôi tự nhiên nên thịt rất săn chắc và thơm ngon. Đem gà chiên giòn và xóc đều với bột muối, sả, hành và lá chanh là đủ để có một món nhậu hấp dẫn khó quên.''), N''https://storage.quannhautudo.com/data/thumb_400/Data/images/product/2024/04/202404240940237304.webp'', N''Gà H’Mong rang muối'', 300000.0E0, N''Suất'', NULL, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DishId', N'CategoryID', N'CreatedBy', N'CreatedDate', N'Description', N'Image', N'Name', N'Price', N'Type', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Dishes]'))
        SET IDENTITY_INSERT [Dishes] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241014163248_SeedDât')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241014163248_SeedDât', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241016103726_FixAppUser')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'Status');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [Status] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241016103726_FixAppUser')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'RestaurantID');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [RestaurantID] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241016103726_FixAppUser')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'Dob');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [Dob] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241016103726_FixAppUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241016103726_FixAppUser', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241016105259_DeleteUserName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241016105259_DeleteUserName', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241021144200_UpdateOrderDetail')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderDetails]') AND [c].[name] = N'UserId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [OrderDetails] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [OrderDetails] ALTER COLUMN [UserId] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241021144200_UpdateOrderDetail')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241021144200_UpdateOrderDetail', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241112162417_Azure')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241112162417_Azure', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241129160455_InitialCreate1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241129160455_InitialCreate1', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241129160543_InitialCreate2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241129160543_InitialCreate2', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250101093806_InitialCreate5')
BEGIN
    DROP TABLE [Operations];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250101093806_InitialCreate5')
BEGIN
    DROP TABLE [RoleOperations];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250101093806_InitialCreate5')
BEGIN
    DROP TABLE [UserOperations];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250101093806_InitialCreate5')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250101093806_InitialCreate5', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250103083454_InitialCreate6')
BEGIN
    CREATE INDEX [IX_OrderDetails_DishId] ON [OrderDetails] ([DishId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250103083454_InitialCreate6')
BEGIN
    ALTER TABLE [OrderDetails] ADD CONSTRAINT [FK_OrderDetails_Dishes_DishId] FOREIGN KEY ([DishId]) REFERENCES [Dishes] ([DishId]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250103083454_InitialCreate6')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250103083454_InitialCreate6', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250103083738_InitialCreate7')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250103083738_InitialCreate7', N'6.0.31');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20250103084027_InitialCreate8')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250103084027_InitialCreate8', N'6.0.31');
END;
GO

COMMIT;
GO

