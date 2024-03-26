INSERT INTO [User](Id, EmailAddress, FullName, RoleId, CreatedById, UpdatedById, CreatedOn, UpdatedOn)
VALUES('7164F1E8-B5EF-4CCB-86CB-F600892C4CB1', 'scheduler@user.com', 'Scheduler user', '028E686D-51DE-4DD9-91E9-DFB5DDDE97D0', '7164F1E8-B5EF-4CCB-86CB-F600892C4CB1', '7164F1E8-B5EF-4CCB-86CB-F600892C4CB1', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)

INSERT INTO Product(Id, Name, Code, Type, Description, Price, Amount, FreeShipping, HasDiscount, ContactEmail, ContactPhone, InfoLink, CreatedById, UpdatedById, CreatedOn, UpdatedOn)
VALUES('8A690056-DE31-4203-830F-8E08E4A22A75', 'Some product', 'Some code', 2, 'Some description', 50, 100, 0, 0, '', '', '', '7164F1E8-B5EF-4CCB-86CB-F600892C4CB1', '7164F1E8-B5EF-4CCB-86CB-F600892C4CB1', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
