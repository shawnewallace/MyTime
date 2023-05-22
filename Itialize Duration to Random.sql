select count(*)
from [dbo].[Entries];

select *
from
    [dbo].[Entries]
order by whenCreated DESC


update dbo.Entries
set Duration = 0
-- where id = 'c6533f15-2bf8-4f87-9f2d-f1ca3f7e65b3' 



WHILE 0 = 0
BEGIN

    DECLARE @wholePart decimal(5, 2) = FLOOR(RAND() * 4) + 1;
    DECLARE @incremental decimal(5, 2) = FLOOR (RAND() * 4) + 1;
    DECLARE @decimalPart decimal(5, 2) = 0.00;
    DECLARE @isUtilized bit = CAST(ROUND(RAND(), 0) as BIT);
    DECLARE @id UNIQUEIDENTIFIER;

    SET @id = (select top 1 id from dbo.Entries where Duration = 0);
    if @id IS NULL BREAK;

    IF (@incremental = 0) SET @decimalPart = 0.0;
    IF (@incremental = 1) SET @decimalPart = 0.25;
    IF (@incremental = 2) SET @decimalPart = 0.50;
    IF (@incremental = 3) SET @decimalPart = 0.75;

    -- SELECT @wholePart, @incremental, @decimalPart, @wholePart + @decimalPart

    update dbo.Entries set Duration = @wholePart + @decimalPart, IsUtilization = @isUtilized where id = @id;

END


select Duration, count(*)
from dbo.Entries
group by Duration
order by Duration asc;

