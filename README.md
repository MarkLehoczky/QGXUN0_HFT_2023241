# QGXUN0_HFT_2023241

## Project Overview
This project was initially developed during the **Advanced Development Techniques** course, focusing on backend development and database integration. Later, it was extended in the **Software Technology and Graphical User Interface Design** course to include graphical user interfaces and additional features.

It is a database-driven application with a C# backend, designed using a layered architecture and a four-table SQL database. The project demonstrates the integration of multiple technologies to create a robust, full-stack application.

## Features

### Backend
- **Framework**: Built using *ASP.NET Core*, the backend provides a REST API for handling API calls.
- **Architecture**: Implements a *layered architecture* with the following layers:
  - **Models**: Defines the database schema and relationships using Entity Framework Core.
  - **Repository**: Handles data access and CRUD operations.
  - **Logic**: Contains business logic and validation.
  - **Endpoint**: Exposes API endpoints for external communication.
- **Database**: Utilizes a *four-table SQL database* with relationships managed via foreign keys.
- **Error Handling**: Implements robust error handling and validation mechanisms.

### Frontend
1. **WPF Graphical User Interface**:
   - A desktop application built with *Windows Presentation Foundation (WPF)*.
   - Provides an intuitive and user-friendly interface for interacting with the application.
   - Supports API calls to the backend for data retrieval and manipulation.

2. **Web Page**:
   - Developed using *HTML*, *CSS*, and *JavaScript*.
   - Offers a browser-based interface for users who prefer web access.
   - Communicates with the backend API to perform CRUD operations.

### Testing
- **Frameworks**: Includes *NUnit* and *Moq* for unit testing.
- **Coverage**: Tests cover both CRUD and non-CRUD operations, ensuring the reliability of backend logic.
- **Mocking**: Uses *Moq* to simulate database interactions for isolated testing.
- **Test Data**: Provides predefined test cases for models like `Book`, `Author`, and `Publisher`.

### Console Interface
- A command-line interface for interacting with the application.
- Useful for debugging and performing quick operations without a graphical interface.

## Development History
- **Advanced Development Techniques**: The initial phase of the project focused on backend development, database design, and implementing the REST API.
- **Software Technology and Graphical User Interface Design**: In this phase, graphical user interfaces were added, including the WPF application and the web page.

## Installation and Setup

### Prerequisites
- **Visual Studio** (recommended version: 2022 or later) with the following workloads installed:
  - **ASP.NET and web development**
  - **.NET desktop development**
- **SQL Server** (or a compatible database engine) for hosting the database.

### Steps
1. Clone the repository:
  ```sh
  git clone https://github.com/marklehoczky/QGXUN0_HFT_2023241.git
  ```
2. Open the solution file (`.sln`) in Visual Studio.
3. Restore NuGet packages by building the solution or using the **Manage NuGet Packages** option.
4. Build the solution to ensure all dependencies are resolved.

## Launching the Application

### Step 1: Start the Backend
1. In Visual Studio, set the backend project (ASP.NET Core API) as the startup project.
2. Run the backend predefined profile.
3. Ensure the API endpoint is running and accessible.

### Step 2: Launch the User Interfaces
- **WPF Application**:
   1. Set the WPF project as the startup project in Visual Studio.
   2. Run the application. Ensure the backend is running, as the WPF app communicates with it via API calls.
- **Web Page**:
   1. Run the backend  using the *IIS Express* profile.
   2. The web page will use JavaScript to make API calls to the backend.

## Running Tests
1. Open the Test Explorer in Visual Studio (**Test > Test Explorer**).
2. Build the solution to discover all tests.
3. Run the tests using the **Run All** button in the Test Explorer.

## Screenshots
Here are some screenshots of the application:

|                                         | WPF Application                                                                                                                                                                                    | Console Interface                                                                                                                                                                                          | Web Page                                                                                                                                                                                           |
| :-------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------: | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------: |
| Main menu                               | ![wpf_main_menu](https://github.com/user-attachments/assets/1a4645cd-59ed-48dd-a624-e112af96915b)                                                                                                  | ![console_main_menu](https://github.com/user-attachments/assets/99554443-849d-4143-893f-9559a9ea4cb0)                                                                                                      | ![web_main_menu](https://github.com/user-attachments/assets/575779fa-df93-4d5c-8b3f-507352beb709)                                                                                                  |
| List of books                           | ![wpf_books1](https://github.com/user-attachments/assets/f64771ff-9fec-4ede-b5ee-1d83d8859b06) <br> ![wpf_books2](https://github.com/user-attachments/assets/8e524998-b5c4-4961-9681-fa89f29883b8) | ![console_books1](https://github.com/user-attachments/assets/04775707-95a3-4a09-9887-0f5794b94f70) <br> ![console_books2](https://github.com/user-attachments/assets/37c090c4-0adf-4ac1-bf58-1c121920117e) | ![web_books1](https://github.com/user-attachments/assets/5281f49d-2e9d-4dc1-96eb-878697124126) <br> ![web_books2](https://github.com/user-attachments/assets/b07f517b-c0b6-43e8-bbeb-ac7f7fb68c00) |
| List of books between 2010 and 2015     | ![wpf_books_filtered](https://github.com/user-attachments/assets/d4df3bae-09c3-4885-8f3a-0c94b0ce8da8)                                                                                             | ![console_books_filtered](https://github.com/user-attachments/assets/f5bcfc22-6c28-4d9b-ba91-35669742eec7)                                                                                                 | ![web_books_filtered](https://github.com/user-attachments/assets/ce1d263c-b364-4b58-9b11-d7361565d7ec)                                                                                             |
| Selected books for collection expansion | ![wpf_books_selected](https://github.com/user-attachments/assets/0eda4491-208f-4ca2-8c35-c7970412af66)                                                                                             | ![console_books_selected](https://github.com/user-attachments/assets/f6284b8a-82b2-4bb3-8b1f-b1b5c75b54f5)                                                                                                 | ![web_books_selected](https://github.com/user-attachments/assets/9035b9b1-c679-4fde-9315-438c1be3b681)                                                                                             |
