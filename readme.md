# Exoplanet Finder Backend

## Description

**Exoplanet Finder Backend** is a server-side application designed to support functionalities related to identifying and analyzing exoplanets. This backend provides APIs and services that interact with external data sources, perform calculations, and manage data associated with discovered or observable exoplanets.

---

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
3. [Features](#features)
4. [Technologies Used](#technologies-used)
5. [Project Structure](#project-structure)

---

## Installation

Follow these steps to set up the project locally:

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
2. Navigate to the root directory:
   ```bash
   cd ExoplanetFinderBackend
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Configure environment settings:
    - Check `appsettings.json` and `appsettings.Development.json` for necessary configuration (e.g., database connection strings, API keys).
    - Modify as needed to suit your local environment.

5. Run the application:
   ```bash
   dotnet run
   ```

---

## Usage

Once the backend application is running, it will expose various endpoints. Below are examples:

1. **Base URL**:
   ```
   https://localhost:<port>/
   ```

2. **Test the APIs** with the HTTP client:  
   A `.http` file, `ExoplanetFinderBackend.http`, is provided for quick testing during development.

3. Deployment steps:
   To publish the application, use:
   ```bash
   dotnet publish -c Release -o ./publish
   ```

---

## Features

- API endpoints for managing and retrieving exoplanet-related data.
- Integration with a **PostgreSQL** database for experiment and observation storage.
- Configurable environment setups (via `appsettings.json` and `appsettings.Development.json`).
- Development-ready logging and debugging support for local environments.
- Modular project structure following clean architecture principles.
- RESTful API design with clearly defined endpoints for assumptions and experiments.

---

### API Endpoints

#### Assumptions
- **GET** `http://localhost:5291/assumptions`  
  Retrieves all scientific or operational assumptions used for exoplanet data processing.


#### Experiments
- **GET** `http://localhost:5291/experiments`  
  Returns a list of all experiments currently stored in the PostgreSQL database.

- **DELETE** `http://localhost:5291/experiments`  
  Deletes all existing experiments.

- **POST** `http://localhost:5291/experiments/custom-wave`  
  Creates a new experiment using a **custom light curve** (flux-time dataset) for exoplanet detection.

- **POST** `http://localhost:5291/experiments/known-star`  
  Initiates an experiment using a **known star’s observation data** (retrieved from archives or datasets).

- **GET** `http://localhost:5291/experiments/{id}`  
  Retrieves details of a specific experiment, including related assumptions and observations.

## Technologies Used

This backend is built with the following technologies:

- **.NET Core**: Framework for building the backend structure.
- **C#**: Programming language.
- **JSON-based Configuration**: Settings managed via `appsettings.json` and `appsettings.Development.json`.

---

## Project Structure

```
ExoplanetFinderBackend/
├── bin/
├── obj/
├── Controllers/
├── Domain/
├── Infrastructure/
├── Properties/
├── appsettings.json
├── appsettings.Development.json
├── ExoplanetFinderBackend.csproj
├── ExoplanetFinderBackend.http
├── Program.cs
└── ExoplanetFinderBackend.sln
```