
# BankingAPI Project

## Overview

BankingAPI is a comprehensive solution for banking operations. This project offers a wide range of functionalities such as account management, transactions, loan services, and automated payments, making it suitable for financial institutions aiming to digitalize their services.

## Features

- **Account Management:** Create and manage bank accounts, including balance checks and updates.
- **Transaction Handling:** Support for deposits, withdrawals, and transfers.
- **Loan Services:** Apply for loans, check loan statuses, and make loan payments.
- **Payment Services:** Set up automatic payments and perform manual payments.
- **User Management:** User registration, authentication, and role updates.
- **Support Services:** Create and track support requests.

## Technical Stack

- **ASP.NET Core MVC:** For building the web API.
- **Entity Framework Core:** As an Object-Relational Mapping (ORM) framework.
- **Microsoft SQL Server:** For database management.
- **JWT Authentication:** For securing the API.

## Project Structure

```
BankingAPIProject/
│
├── src/
│   ├── BankingAPI/
│   │   ├── Controllers/ (API endpoints for different functionalities)
│   │   ├── Models/ (Data models representing database entities)
│   │   ├── Services/ (Business logic implementation)
│   │   ├── DTOs/ (Data Transfer Objects for transferring data)
│   │   ├── Data/ (Database context and repositories)
│   │   ├── Helpers/ (Utility classes like AuthHelper and PasswordHasher)
│   ├── README.md (Project documentation)
```

## Setup and Installation

1. **Prerequisites:** Ensure you have .NET Core SDK and Microsoft SQL Server installed.
2. **Database Setup:** Create a database in SQL Server and update the connection string in `appsettings.json`.
3. **Run Migrations:** Execute `Update-Database` in the Package Manager Console to create the database schema.
4. **Running the API:** Use the `dotnet run` command in the project directory to start the API.

## API Endpoints

- **AccountController:** `api/account/...` (Create account, Get balance, Update balance)
- **TransactionController:** `api/transaction/...` (Deposit, Withdraw, Transfer)
- **LoanController:** `api/loan/...` (Apply for loan, Get loan status, Make loan payment)
- **PaymentController:** `api/payment/...` (Setup auto payment, Make payment)
- **UserController:** `api/user/...` (User registration, Login, Update user)
- **SupportController:** `api/support/...` (Create support request, Get request status)

## Security

- API endpoints are secured with JWT authentication.
- Role-based authorization to restrict access to certain functionalities.
- Passwords are hashed using SHA256 for secure storage.

## Testing

- ...
