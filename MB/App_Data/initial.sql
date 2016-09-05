INSERT INTO [dbo].UserRole
           ([Name]
			,[CreateTime]
			,[LastTime]
			,[Deleted])
     VALUES
           ('管理员','2016-8-12','2016-8-12',0)
GO

INSERT INTO [dbo].Department
           ([Name]
           ,[Code]
		   ,[CreateTime]
		   ,[LastTime]
		   ,[Deleted])
     VALUES
           ('董事会','1000000000','2016-8-12','2016-8-12',0)
GO

INSERT INTO [dbo].Job
           ([DepartmentId]
           ,[Name]
		   ,[CreateTime]
		   ,[LastTime]
		   ,[Deleted])
     VALUES
           (1
           ,'董事长','2016-8-12','2016-8-12',0)
GO

INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[Email]
           ,[Sex]
           ,[OpenTypeId]
           ,[UserRoleId]
           ,[JobId]
           ,[EmailConfirmed]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[UserName])
     VALUES
           ('9980a214-a26d-4bc0-9e44-4a607b11797c'
           ,'123@qq.com'
           ,0
           ,0
           ,1
           ,1
           ,1
           ,12345678901
           ,1
           ,0
           ,0
           ,0
           ,'AEocIxlpF6SAUKpJnFBGZmhPAkT3q/ju/Fd7QcjWwUnrefVqQW1dtjI6MnhlifwDwg=='
           ,'bb7109eb-dd2d-4fa0-b06f-59e7fea07cc7'
           ,'123@qq.com')
GO