# Scheduling System

A robust and intuitive scheduling system designed to optimize the management of appointments and schedules. Features include creating, editing, and deleting events, notifications, multi-user support, and integration with external calendars. Ideal for businesses and professionals.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)

## Features

- Create, edit, and delete events
- Notifications for upcoming events
- Multi-user support
- Integration with external calendars
- User authentication and authorization
- Responsive design for mobile and desktop

## Technologies Used

- .NET 8 Minimal API
- Entity Framework Core
- SQL Server
- Swagger
- JWT Authentication

## Architecture

The project follows a layered architecture with the following layers:

1. **Presentation Layer**: Contains API endpoints implemented using Minimal API.
2. **Application Layer**: Contains business logic and application services.
3. **Domain Layer**: Contains domain entities and business rules.
4. **Infrastructure Layer**: Contains data access implementations and external service integrations.

## Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/vitorparras/Scheduling-System.git
    ```

2. Navigate to the project directory:

    ```bash
    cd scheduling-system
    ```

3. Restore the dependencies:

    ```bash
    dotnet restore
    ```

4. Update the database:

    ```bash
    dotnet ef database update
    ```

5. Run the application:

    ```bash
    dotnet run
    ```

## Usage

- Access the API documentation at `http://localhost:5000/swagger` to explore the available endpoints.
- Use your preferred HTTP client (e.g., Postman) to interact with the API.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a pull request.
