
Create table tblMain
(
MainID int Primary key identity,
aDate date,
aTime Varchar(15),
TableName Varchar(10),
WaiterName Varchar(15),
status Varchar(15),
orderType Varchar(15),
total float,
received float,
change float
)

create table tblDetails
(
DetailID int Primary key  identity,
MainID int,
proId int,
qty int,
price float,
amount float
)
truncate table  tblDetails;
truncate table tblMain;

select * from tblMain m
inner join tblDetails d on m.MainID = d.main
