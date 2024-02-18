# Sprout Exam

- [Setup](#setup)
- [Enhancements](#enhancements)
- [Future Enhancements](#futureenhancements)

## Setup<a name="setup"></a>  
- Import `db/SproutExamDb.bacpac`
- modify [appsettings.json](./src/Sprout.Exam.WebApp/appsettings.json) and set your connection string.
```json
{
  "ConnectionStrings": {
	"DefaultConnection": "connection_string_here"
  }
}
```
- run `dotnet run --project src/Sprout.Exam.WebApp/Sprout.Exam.WebApp.csproj`

---
## Enhancements<a name="enhancements"></a>  
- Add Tax and Salary database entry. This gives an ability to dynamically calculate based on Employee.
- Created EmployeeFormComponent and EmployeeTypeComponent
- Implemented testing for all business logics
- Apply CQRS for better scalability for future enhancements.
- Apply FluentValidation for english like structure of validations.
- Apply CLEAN Architecture.
- Apply update to NuGet packages.

---

## Future Enhancements<a name="futureenhancements"></a>  
- .NET 8 update (.NET6 will be deprecated this year)
- upgrade react project to latest and use redux for state management. 
- e2e testing.
- Admin page for adding users instead of registration since this is internal software.
- Maintenance page to manipulate Employee Types
- Dynamic formula calculation feature.
- Employee Reimbursement feature.
- Leave credits management feature.
- Add employee profile such as addresses and contacts.
- Detach react project to backend project for better maintainability in terms of Repository management.

