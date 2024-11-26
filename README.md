# Stockify

Stockify is a modular monolith application designed for stock analysis using .NET. This project aims to provide a comprehensive platform for analyzing stock market data, facilitating both retrieval and processing of stock information.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Running the Application](#running-the-application)
- [Running Tests](#running-tests)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Features

- **Stock Data Retrieval:** Fetch real-time stock data from various APIs.
- **Data Processing:** Analyze and process stock data to generate insights.
- **Modular Monolith Architecture:** Organized structure that allows easy scaling and maintenance.
- **Docker Support:** Easy deployment using Docker containers.

## Technologies Used

- **C#**
- **.NET**
- **Testing:** xUnit testing framework for unit tests.
- **Docker Compose:** Configuration for running the application using Docker.
- **Database:** PostgreSQL for data storage.
- **ORM:** Entity Framework Core.
- **Messaging:** RabbitMQ for messaging between modules.
- **Logging:** Serilog + Seq.
- **Caching:** Redis.
- **Monitoring:** Jaeger for distributed tracing.
- **CI/CD:** GitHub Actions for continuous integration and deployment.
- **Security:** KeyCloak for authentication and authorization.

## Architecture

Stockify follows a modular monolith architecture, which includes:

- **API Layer:** Exposes endpoints for communication with the application.
- **Modules:** Contains the business logic, application, infrastructure, presentation, and integration events for each module.
- **Tests:** Includes test projects for the application.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/a-kostyuchenko/stockify.git
   cd stockify
   ```

2. Build and run the application using Docker:
   ```sh
   docker-compose up --build
   ```

## Running the Application

To run the application locally without Docker, use the following commands:

1. Navigate to the project directory:
   ```sh
   cd src/Stockify.Api
   ```

2. Run the application:
   ```sh
   dotnet run
   ```

## Running Tests

To run the tests, use the following command:
```sh
dotnet test Stockify.sln
```

## Project Structure

- `src/`
  - `API/Stockify.Api/` - API layer of the application
  - `Modules/`
    - `{ModuleName}/Stockify.Modules.{ModuleName}.Domain/` - Domain layer for business logic.
    - `{ModuleName}/Stockify.Modules.{ModuleName}.Application/` - Application layer for use cases.
    - `{ModuleName}/Stockify.Modules.{ModuleName}.Infrastructure/` - Infrastructure layer for data access, outbox and inbox messages, external services, etc.
    - `{ModuleName}/Stockify.Modules.{ModuleName}.Presenation/` - Presentation layer for endpoints, controllers, etc.
    - `{ModuleName}/Stockify.Modules.{ModuleName}.IntegrationEvents/` - Integration events for communication between modules.
- `tests/` - Contains test projects for the application
- `docker-compose.yml` - Docker Compose configuration

## Contributing

Contributions are welcome! Please fork the repository and create a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For any inquiries, please contact [kosttchka@gmail.com](mailto:kosttchka@gmail.com).
Feel free to adjust the content to fit your specific project details and any additional information you would like to include.

## Credits

This project was inspired by [@Milan Jovanović](https://github.com/m-jovanovic) [Modular Monolith Architecture Course](https://www.milanjovanovic.tech/modular-monolith-architecture)
