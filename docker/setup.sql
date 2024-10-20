IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'project')
BEGIN
    CREATE DATABASE project;
END
