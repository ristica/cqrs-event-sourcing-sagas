# cqrs with es and saga(s) as process manager

As event store / snapshoting => NEventStore (in my case I'm using SQL Server as the DB)

As state store => SQL Server

---------------------------------------------------------------------------------------

How to use it?

Just change the connection strings in the BankAccount.Client project (web.config). 

Build the solution (enable restoring of the nuget packages in visual studio).

That's it!

--------------------------------------------------------------------------------------
It is the "same" project as the one under "cqrs" repository (about bank accounts) 
but with a process manager instead of CommandHandler and EventHandler as workflow manager
