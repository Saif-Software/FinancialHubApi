# 💰 Financials Hub Web API

A comprehensive **ASP.NET Core 8 Web API** for managing financial reports, transaction records, and attachments. Built with **Entity Framework Core**, **SQL Server**, and the **Repository Pattern**.

> 🔗 **Frontend**: [fainancial-hubbb.vercel.app](https://fainancial-hubbb.vercel.app/)

---

## 🚀 Features

- ✅ Full CRUD for **Financial Reports** (create, read, update, delete)
- ✅ Nested CRUD for **Transaction Records** (expenses per report)
- ✅ **File Upload** for attachments on transaction records
- ✅ **Cascade Delete** — deleting a report removes all related records and attachments
- ✅ **Repository Pattern** with generic and specific repositories
- ✅ **Swagger UI** for API exploration and testing
- ✅ **CORS** configured for frontend integration
- ✅ **Seed Data** for testing out of the box

---

## 🛠️ Tech Stack

| Technology | Version |
|---|---|
| .NET | 8.0 |
| ASP.NET Core Web API | 8.0 |
| Entity Framework Core | 8.7 |
| SQL Server (LocalDB) | Latest |
| Swagger / Swashbuckle | 6.6.2 |

---

## 📦 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB or full instance)

### Setup

1. **Clone the repository**

```bash
git clone https://github.com/YOUR_USERNAME/FinancialsHubWebAPI.git
cd FinancialsHubWebAPI
```

2. **Update the connection string** in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Financials_DB;Integrated Security=True;"
  }
}
```

3. **Run the application**

```bash
dotnet run
```

The API will start at `http://localhost:5207` and automatically:
- Add any missing columns to the database
- Seed test data (roles, accounts, categories, reports, records)

4. **Open Swagger UI**

Navigate to `http://localhost:5207/swagger` to explore and test all endpoints.

---

## 📋 API Endpoints

### Transaction Reports

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/TransactionReport` | List all reports with totals, category, creator |
| `GET` | `/api/TransactionReport/{id}` | Get report details with records & attachments |
| `POST` | `/api/TransactionReport` | Create a new financial report |
| `PUT` | `/api/TransactionReport/{id}` | Update report (name, notes, category) |
| `DELETE` | `/api/TransactionReport/{id}` | Delete report + all related data |

### Transaction Records (Expenses)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/TransactionReport/{reportId}/records` | List expenses for a report |
| `POST` | `/api/TransactionReport/{reportId}/records` | Add a new expense row |
| `PUT` | `/api/TransactionReport/{reportId}/records/{recordId}` | Update an expense |
| `DELETE` | `/api/TransactionReport/{reportId}/records/{recordId}` | Delete an expense |

### Attachments

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/TransactionReport/{reportId}/records/{recordId}/attachments` | Upload file |
| `DELETE` | `/api/TransactionReport/attachments/{mediaId}` | Delete attachment |

---

## 📝 Request / Response Examples

### Create a Report

**POST** `/api/TransactionReport`

```json
{
  "reportName": "Monthly Internet Bill",
  "notes": "Internet subscription payments",
  "creatorAccountId": 1,
  "categoryId": 1
}
```

**Response** `201 Created`:

```json
{
  "id": 5,
  "reportName": "Monthly Internet Bill",
  "notes": "Internet subscription payments",
  "creatorNameEn": "Ahmed Yahya",
  "creatorNameAr": "أحمد يحيى",
  "creatorAccountId": 1,
  "categoryName": "Internet Subscriptions",
  "categoryId": 1,
  "createdAt": "2026-03-03T03:30:00",
  "totalAmount": 0,
  "transactionRecords": [],
  "attachments": []
}
```

### Add an Expense

**POST** `/api/TransactionReport/5/records`

```json
{
  "transactionDate": "2026-03-01",
  "categoryId": 2,
  "amount": 150.50,
  "description": "Office supplies purchase"
}
```

### Upload Attachment

**POST** `/api/TransactionReport/5/records/8/attachments`

Send as `multipart/form-data` with a `file` field.

---

## 🏗️ Project Structure

```
FinancialsHubWebAPI/
├── Controllers/
│   └── TransactionReportController.cs    # All API endpoints
├── DTOs/
│   └── TransactionReportDtos.cs          # Request & response DTOs
├── Models/
│   ├── TransactionReport.cs              # Report entity
│   ├── TransactionRecord.cs              # Expense entity
│   ├── Account.cs                        # User account
│   ├── Category.cs                       # Expense categories
│   ├── Status.cs                         # Status types
│   └── Medium.cs                         # Attachments (polymorphic)
├── Repository/
│   ├── IGenericRepo.cs.cs                # Generic repo interface
│   ├── GenericRepo.cs                    # Generic repo implementation
│   ├── ITransactionReportRepo.cs         # Specific repo interface
│   └── TransactionReportRepo.cs          # Eager loading & cascade delete
├── AppDbContext.cs                       # EF Core database context
├── DbSeeder.cs                           # Test data seeder
├── Program.cs                            # App configuration & startup
└── appsettings.json                      # Connection string
```

---

## 🌱 Seed Data

The app seeds the following data on first run (only if tables are empty):

| Entity | Count | Examples |
|--------|-------|---------|
| Roles | 3 | Admin, Accountant, Manager |
| Statuses | 6 | Active, Draft, Under Review, Approved, Rejected, Completed |
| Accounts | 2 | Ahmed Yahya, Sara Ali |
| Categories | 6 | Internet Subscriptions, Various Purchases, Custody, Transfers, Salaries, Maintenance |
| Reports | 3 | Monthly Internet Bill, Office Supplies, Salary Advances |
| Records | 7 | Various expenses across the 3 reports |

---

## 🔗 Frontend Integration

This API is designed to work with the [Financial Hub Frontend](https://fainancial-hubbb.vercel.app/). 

**CORS** is configured to allow all origins. The frontend expects:

| Frontend Field | API Field |
|---|---|
| Report Title (عنوان التقرير) | `reportName` |
| Description (الوصف) | `notes` |
| Category (التصنيف) | `categoryId` / `categoryName` |
| Creator (أنشئ بواسطة) | `creatorNameEn` / `creatorNameAr` |
| Creation Date (تاريخ الإنشاء) | `createdAt` |
| Total Amount (إجمالي المبلغ) | `totalAmount` |
| Last Transaction Date (تاريخ آخر عملية) | `lastTransactionDate` |
| Expense Date (التاريخ) | `transactionDate` |
| Expense Amount (المبلغ) | `amount` |
| Expense Statement (البيان) | `description` |
| Attachments (المرفقات) | `attachments[]` |

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
