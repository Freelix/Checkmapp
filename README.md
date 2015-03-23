# Checkmapp
A windows phone application that allows people to manage their pictures, locations and notes from their trips.

##View local databas
The view our database content, we use 2 programs
* SQL CQA - An open source project that is light, easy to use and can execute sql queries.
* ISETool - A simple application used for extracting the local databse on windows phone 8.

###How to use it
1. Download and install [SQL Compact Query Analyzer](https://sqlcequery.codeplex.com/) to default folder
2. Open your emulator and exit your application
3. Execute those line in the command prompt

```
cd "C:\Program Files (x86)\Microsoft SDKs\Windows Phone\v8.1\Tools\IsolatedStorageExplorerTool"
RD /S /Q "C:\windows-phone-database"
ISETool.exe ts xd 1ec8df13-6ca9-40e9-a465-7931ec2a4ae6 "C:\windows-phone-database"
```

ISETool will generate your application database file in C:\windows-phone-database

The final step is to open SQL Compact Query Analyzer and browse to the previous folder to pick up the database.

Note: You have to execute ISETool each time you modified the database to see the changes.
