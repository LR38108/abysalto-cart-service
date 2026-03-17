IF DB_ID('cartdb') IS NULL
BEGIN
    CREATE DATABASE cartdb;
END
GO

USE cartdb;
GO

IF OBJECT_ID('dbo.cart_items', 'U') IS NOT NULL
    DROP TABLE dbo.cart_items;
GO

IF OBJECT_ID('dbo.carts', 'U') IS NOT NULL
    DROP TABLE dbo.carts;
GO

CREATE TABLE dbo.carts (
    id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    user_id NVARCHAR(100) NULL,
    created_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    updated_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

CREATE TABLE dbo.cart_items (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    cart_id UNIQUEIDENTIFIER NOT NULL,
    product_id INT NOT NULL,
    product_name NVARCHAR(200) NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    currency NVARCHAR(10) NOT NULL DEFAULT 'EUR',
    CONSTRAINT FK_cart_items_carts
        FOREIGN KEY (cart_id) REFERENCES dbo.carts(id) ON DELETE CASCADE
);
GO

INSERT INTO dbo.carts (id, user_id)
VALUES ('11111111-1111-1111-1111-111111111111', 'demo-user');
GO

INSERT INTO dbo.cart_items (cart_id, product_id, product_name, quantity, price, currency)
VALUES
('11111111-1111-1111-1111-111111111111', 1001, 'Demo Sneakers', 2, 79.99, 'EUR'),
('11111111-1111-1111-1111-111111111111', 2002, 'Demo Hoodie', 1, 49.50, 'EUR');
GO
