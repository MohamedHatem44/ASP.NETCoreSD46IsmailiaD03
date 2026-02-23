# ASP.NETCoreSD46IsmailiaD03

# 🔷 ASP.NET Core MVC – CRUD with Entity Framework Core (.NET 9)

This project demonstrates a full **ASP.NET Core MVC CRUD application** using:

- ✅ ASP.NET Core MVC  
- ✅ Entity Framework Core  
- ✅ SQL Server  
- ✅ ViewModels (Best Practice)  
- ✅ Repository Pattern Concepts  
- ✅ Data Seeding  
- ✅ One-to-Many Relationship  
- ✅ Dropdown Binding  
- ✅ Mapping Between Domain Model & ViewModel  

Built using **.NET 9 + SQL Server**

---

# 📁 Project Structure

```
ASP.NETCoreD03
│
├── Controllers
│   └── EmployeeController
│
├── Models
│   ├── Employee
│   └── Department
│
├── ViewModels
│   ├── EmployeeReadVM
│   ├── EmployeeCreateVM
│   └── EmployeeEditVM
│
├── Data
│   ├── Context
│   │   └── AppDbContext
│   └── Configuration
│       └── EmployeeConfiguration
```

---

# 📌 Architecture Overview

This project follows a simplified layered structure:

- **Domain Models (Models)** → Database representation  
- **DbContext** → Database connection & configuration  
- **Controller** → Handles requests  
- **ViewModels** → UI-safe data models  
- **Views** → Razor UI  

---

# 📌 Database Context

## AppDbContext

```csharp
public class AppDbContext : DbContext
```

### Responsibilities:

- Connect to SQL Server
- Configure Entities
- Seed initial data
- Define DbSets

```csharp
public virtual DbSet<Employee> Employees { get; set; }
public virtual DbSet<Department> Departments { get; set; }
```

---

# 📌 Domain Models

## Employee

```csharp
public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }
    public virtual Department? Department { get; set; }
}
```

---

## Department

```csharp
public class Department
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
```

---

# 📌 Relationship Configuration

Configured using Fluent API:

```csharp
builder.HasOne(e => e.Department)
       .WithMany(d => d.Employees)
       .HasForeignKey(e => e.DepartmentId)
       .IsRequired();
```

✔ One Department → Many Employees  
✔ Foreign Key: `DepartmentId`

---

# 📌 Data Seeding

Inside `OnModelCreating`:

- 4 Departments
- 10 Employees

```csharp
modelBuilder.Entity<Department>().HasData(_departments);
modelBuilder.Entity<Employee>().HasData(_employees);
```

✔ Database auto-filled on migration  

---

# 📌 EmployeeController

Handles full CRUD operations.

---

# 🔹 Index (Read All)

## V01 – Return Domain Model Directly

```csharp
public IActionResult IndexV01()
{
    var employees = db.Employees.Include(e => e.Department);
    return View(employees);
}
```

⚠ Not recommended for real projects.

---

## V02 – Using ViewModel (Best Practice)

```csharp
.Select(e => new EmployeeReadVM
{
    Id = e.Id,
    Name = e.Name,
    Age = e.Age,
    Salary = e.Salary,
    Department = e.Department!.Name
})
```

✔ Separation of concerns  
✔ Secure  
✔ Clean Architecture  

---

# 🔹 Details

## V01 – Domain Model  
## V02 – ViewModel Mapping  

Handles null safely:

```csharp
if (employee == null)
{
    return RedirectToAction("IndexV01");
}
```

---

# 🔹 Create

## V01 – Direct Binding (Domain Model)

```csharp
[HttpPost]
public IActionResult CreateV01(Employee employee)
```

---

## V02 – Using ViewModel (Best Practice)

### GET

```csharp
public IActionResult CreateV02()
{
    var employeeCreateVM = new EmployeeCreateVM
    {
        Departments = GetDepartmentsForDropDown()
    };
    return View(employeeCreateVM);
}
```

### POST

```csharp
var employee = new Employee
{
    Name = employeeCreateVM.Name,
    Age = employeeCreateVM.Age,
    Salary = employeeCreateVM.Salary,
    DepartmentId = employeeCreateVM.DepartmentId
};
```

✔ Manual Mapping  
✔ Clean Separation  

---

# 🔹 Edit

## V01 – Direct Update

Fetch entity then update properties manually.

## V02 – Using ViewModel

```csharp
employeeInDb.Name = employeeEditVM.Name;
employeeInDb.Age = employeeEditVM.Age;
employeeInDb.Salary = employeeEditVM.Salary;
employeeInDb.DepartmentId = employeeEditVM.DepartmentId;
```

✔ Safe  
✔ Controlled update  

---

# 🔹 Delete

```csharp
var employee = db.Employees.FirstOrDefault(e => e.Id == id);
db.Employees.Remove(employee);
db.SaveChanges();
```

---

# 📌 Helper Method (DRY Principle)

```csharp
private List<SelectListItem> GetDepartmentsForDropDown()
```

✔ Reusable  
✔ Clean  
✔ Avoids duplication  

---

# 📌 Include() – Eager Loading

```csharp
db.Employees.Include(e => e.Department)
```

✔ Loads related data  
✔ Avoids Lazy Loading issues  

---

# 📌 ViewModel Benefits

Why use ViewModels?

- 🔒 Security (Prevent Overposting)
- 🎯 Send only required data to UI
- 🧼 Clean architecture
- 📦 Decoupling UI from DB Model

---

# 🎯 Learning Goals (Day 3)

This project teaches:

- ASP.NET Core MVC basics  
- Entity Framework Core integration  
- One-to-Many relationships  
- Fluent API configuration  
- Data seeding  
- CRUD operations  
- ViewModel pattern  
- Mapping between layers  
- DRY principle  
- Eager Loading  

---

# 🛠 Requirements

- .NET 9 SDK  
- Visual Studio 2022+  
- SQL Server  
- SQL Server Management Studio (Optional)

---

# ▶ How to Run

1️⃣ Update connection string if needed:

```csharp
Server=YOUR_SERVER;Database=SDASPNETCore;
```

2️⃣ Apply migrations:

```bash
Add-Migration InitialCreate
Update-Database
```

3️⃣ Run project:

```bash
dotnet run
```

Or press **F5** in Visual Studio.

---

# 📌 Key Takeaway

This project demonstrates the correct way to build:

- Professional CRUD system  
- Clean MVC architecture  
- Proper Entity Framework usage  
- ViewModel separation  
- Scalable project structure  

---

# 👨‍💻 Author

Mohamed Hatem  
Software Engineer

---
