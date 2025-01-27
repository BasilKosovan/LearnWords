IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'LearnWords')
BEGIN
    CREATE DATABASE LearnWords;
    PRINT 'Database created successfully.';
END
ELSE
BEGIN
    PRINT 'Database already exists.';
END
