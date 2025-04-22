Expense Tracker CLI
A simple command-line tool to track, list, summarize, and delete expenses, built with C#.

# Features

Add new expenses with a description and amount

List all recorded expenses

Show a summary (total expenses)

Delete expenses by ID

Data saved and loaded automatically from expenses.json

# Requirements

.NET 6.0 or higher

C# compiler

# Usage

1. Add a New Expense
   bash
   Copy
   Edit
   expense-tracker add --description "Lunch" --amount 20
   Adds a new expense with description "Lunch" and amount $20.

2. List All Expenses
   bash
   Copy
   Edit
   expense-tracker list
   or simply run:

bash
Copy
Edit
expense-tracker
Displays a list of all expenses with ID, Date, Description, and Amount.

3. Show Summary
   bash
   Copy
   Edit
   expense-tracker summary
   Displays the total amount spent.

4. Delete an Expense
   bash
   Copy
   Edit
   expense-tracker delete --id 3
   Deletes the expense with ID = 3.

# How It Works

Expenses are saved in a local file called expenses.json.

When you start the app, it loads all previous expenses automatically.

New expenses, deletions, and updates are immediately saved back to the file.

# Running the Program

Build the app:

bash
Copy
Edit
dotnet build
Run the app:

bash
Copy
Edit
dotnet run -- add --description "Coffee" --amount 5
or

bash
Copy
Edit
dotnet run -- summary

# Commands Overview

# Command Description

add --description <desc> --amount <amt> Add a new expense
list List all expenses
summary Show total expenses
delete --id <id> Delete an expense by ID

# Data Storage

All expense data is stored in a simple expenses.json file in the application directory.

The file is created automatically if it doesn't exist.
Project URL: https://roadmap.sh/projects/expense-tracker
