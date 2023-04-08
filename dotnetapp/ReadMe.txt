sqlcmd -U sa 
password : examlyMssql@123

select @@servername - to get the servername
create table employees before u run the code.

Open a terminal and navigate to the root directory of your solution.

Run the following command to create a new NUnit test project:

arduino
Copy code
dotnet new nunit
This will create a new NUnit test project with the default project name.

Rename the project folder and .csproj file to a meaningful name. For example, if your project to be tested is named EmployeeProject, you could name the test project EmployeeProject.Tests.

Add a reference to the project that contains the code you want to test by running the following command:

css
Copy code
dotnet add <path-to-test-project>.csproj reference <path-to-project-to-be-tested>.csproj
For example, if your test project is in a folder named EmployeeProject.Tests and your project to be tested is in a folder named EmployeeProject, and both folders are in the root directory of your solution, the command would be:

csharp
Copy code
dotnet add EmployeeProject.Tests/EmployeeProject.Tests.csproj reference EmployeeProject/EmployeeProject.csproj
Now you can write NUnit test cases in the test project and run them using the NUnit test runner.