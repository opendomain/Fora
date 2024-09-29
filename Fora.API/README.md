# Fora

Fora coding challenge V2

For full description, see Fora Coding Challenge PDF

There are 3 api endpoints 
1. api/Company
	1. show all companies
	2. with optional 1 letter to filter out companies that start with that letter
2. api/Company/Cik
	1. Show one company by CIK number
3. api/Edgar
	1. Call Edgar API and get raw EdgarCompanyInfo

The data source for this project uses LocalDB. 
To get it to work, copy the repo into C:\repos\Fora\
This allows the connection string to find the data file.
There is a second connection string for direct connection to a SQL Server.


This project is packaged with the data retrieved and all calculations run
To force the application to get the Edgar data and calculate the Fundable amounts, run the "Clear Edgar Company Data.sql" to clear the data.
If you do this, the UpdateDbBackgroundService runs when the app starts and retrieves data from Edgar.  This can take several minutes.

To start the database completely from scratch, delete all tables and then run "Update-Database" from the Package manager console.
