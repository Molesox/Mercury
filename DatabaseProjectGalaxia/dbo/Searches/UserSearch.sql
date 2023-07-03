IF EXISTS (SELECT * FROM sys.objects WHERE type = 'IF' AND name = 'UserSearch')
    DROP FUNCTION dbo.GetAllUsers
GO

CREATE FUNCTION dbo.UserSearch
(
    @UserName nvarchar(256) = NULL, --Filter by username
    @Email nvarchar(256) = NULL --Filter by email
)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
(
    SELECT
        Id AS AspNetUserID,
        UserName,
        Email,
        PhoneNumber,
        LockoutEnabled,
        LockoutEnd
    FROM
        dbo.AspNetUsers
    WHERE
        (UserName = @UserName OR @UserName IS NULL)
        AND (Email = @Email OR @Email IS NULL)
)
GO
