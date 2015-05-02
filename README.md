# Checkmapp
A windows phone application that allows people to manage their pictures, locations and notes from their trips.

##Description
Checkmapp is an application that let you manage all your trips around the world. You can save notes, pictures and points of interest. That way, you can keep the best memories of your travels and share them with your friends.

You can also backup your travels on OneDrive in case you decide to change your phone and then import it in Checkmapp again. 

## Website
[http://checkmapp.com/](http://checkmapp.com/)

##Windows Phone Store
[Buy the app](https://www.windowsphone.com/en-us/store/app/checkmapp/f4c51a4a-35ef-4e72-a5c5-f9c8d0a4ebbd)

##View local database
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
