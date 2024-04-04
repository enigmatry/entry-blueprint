-- an Example of how to seed additional users into the database (for testing purposes)
INSERT INTO [User](Id, EmailAddress, FullName, RoleId, CreatedById, UpdatedById, CreatedOn, UpdatedOn)
VALUES('guid-here', 'email-here', 'full-name-here', '028E686D-51DE-4DD9-91E9-DFB5DDDE97D0', 'dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc', 'dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)