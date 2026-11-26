# Invoice Processing Automation System

An invoice processing automation project that combines an ASP.NET Core Web API, SQL Server, and UiPath RPA to automate invoice ingestion, validation, and storage.

## Overview

The system reads invoice records from an Excel spreadsheet using UiPath and submits them to an ASP.NET Core Web API. The API processes the incoming data and stores valid invoices in SQL Server.

Duplicate invoices are detected and reported separately, while the automation tracks successful, duplicate, and failed requests.

## Architecture

```text
       Excel Invoice Data
               |
               v
       UiPath Automation
       - Read Excel rows
       - Process each invoice
       - Send HTTP POST requests
               |
               v
       ASP.NET Core Web API
       - Receive invoice data
       - Validate requests
       - Detect duplicates
       - Handle errors
               |
               v
          SQL Server
       InvoiceProcessingDB
```

## Technologies Used

| Component | Technology |
|---|---|
| Backend | C#, ASP.NET Core Web API |
| Framework | .NET 10 |
| Database | Microsoft SQL Server |
| ORM | Entity Framework Core 10 |
| Automation | UiPath Studio |
| Input | Microsoft Excel |
| API Documentation | Swagger / OpenAPI |
| Version Control | Git & GitHub |

## Project Structure

```text
InvoiceProcessingSystem/
│
├── InvoiceProcessingAPI/
│   ├── Controllers/
│   ├── Data/
│   ├── Migrations/
│   ├── Models/
│   └── Program.cs
│
├── InvoiceProcessingBot/
│   ├── Main.xaml
│   ├── project.json
│   └── project.uiproj
│
├── Invoices.xlsx
├── InvoiceProcessingSystem.sln
└── .gitignore
```

## Features

- Reads invoice records from Excel using UiPath.
- Processes multiple invoices automatically.
- Sends invoice data to the backend through REST API requests.
- Stores
