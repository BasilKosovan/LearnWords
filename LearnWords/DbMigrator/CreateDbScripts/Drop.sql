IF EXISTS (SELECT * FROM sys.databases WHERE name = N'LearnWords')
BEGIN
    -- Set the database to single user mode to drop it
    ALTER DATABASE LearnWords SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE LearnWords;
    PRINT 'Database dropped successfully.';
END
ELSE
BEGIN
    PRINT 'Database does not exist.';
END
