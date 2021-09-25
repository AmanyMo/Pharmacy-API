Alter FUNCTION TotalInvoice ()
RETURNS TABLE
AS
RETURN(
select sum(m.Price * i.Quantity)as TOTAL ,i.Invoice_ID
 from Medicine m,[dbo].[Invoice Details]  i 
  where i.Medicine_ID=m.ID
  group by i.Invoice_ID
  )

GO
*********************************************************
SELECT * FROM [dbo].[TotalInvoice] ()

************************************************

insert into [dbo].[Invoice Details](Invoice_ID , Medicine_ID , Quantity , Price)
values(1,2,2, [Quantity]*1.2)

