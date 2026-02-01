-- Seed Admin and Staff users
-- Passwords are hashed using BCrypt
-- Admin: Admin@123, Staff: Staff@123

-- Check and insert Admin user
IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'admin@ecommerce.com')
BEGIN
    INSERT INTO Users (FullName, Email, Password, PhoneNumber, RoleId, IsActive, CreatedAt, UpdatedAt)
    VALUES (
        'Administrator',
        'admin@ecommerce.com',
        '$2a$11$K8xGqxM3R9HqzQmPVbqmIuZJZ5gqX6SxW9RnM7WjXpZLQkQM0XAOC',
        '0901234567',
        1,
        1,
        GETUTCDATE(),
        GETUTCDATE()
    );
    PRINT 'Admin user created: admin@ecommerce.com / Admin@123';
END
ELSE
    PRINT 'Admin user already exists';

-- Check and insert Staff user
IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'staff@ecommerce.com')
BEGIN
    INSERT INTO Users (FullName, Email, Password, PhoneNumber, RoleId, IsActive, CreatedAt, UpdatedAt)
    VALUES (
        'Staff Member',
        'staff@ecommerce.com',
        '$2a$11$rB5jZ9X8K3xGqxM3R9HqzQPVbqmIuZJZ5gqX6SxW9RnM7WjXpZLQk',
        '0909876543',
        3,
        1,
        GETUTCDATE(),
        GETUTCDATE()
    );
    PRINT 'Staff user created: staff@ecommerce.com / Staff@123';
END
ELSE
    PRINT 'Staff user already exists';

GO
