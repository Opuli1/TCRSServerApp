# TCRS Web Portal

## Overview

The TCRS Web Portal is a comprehensive web portal designed as the primary information hub for current, potential, and future retirees. It provides a wealth of resources including FAQs about retirement, contact details for the retirement department, and convenient links to existing websites. Additionally, this portal acts as a training platform for internal users, facilitating efficient knowledge distribution.


## Features

* **Resource Hub:** Central access to important retirement information and FAQs.
* **Contact Information:** Easy Access to contact details for the retirement department.
* **Training Platform:** Provides training resources for internal users to ehance their understanding and skills.


## Installation

### Prerequisites

* **Visual Studio 2022**
* **SQL Server 2019** (SQL Server Management Studio is optional but recommended)

### Setup Instructions

1. **Configure the Database:**
    * Ensure SQL Server 2019 is installed and running.
    * Open the '**appsettings.json**' file in your project directory.
    * Modify the '**ConnectionStrings**' line to point to your existing or new database.
2. **Database Migration:**
    * Open Visual Studio 2022.
    * Navigate to the **Package Manager Console**.
    * Run the command '**update-database**' to apply the latest migrations to your database.


## Usage

Once installed, the TCRS Web Portal can be accessed through a local development server or deployed to a live web server. Users can borrow the various sections for retirement information, contact details, and training resources.


## Contributors

* **Adrian West**
* **Amanda Nix**
* **Daquan Buchanan**
* **Harry Starkweather**
* **Zachary Barber**


## Licensing
This project was created for educational purposes and is part of a coursework requirement at Austin Peay State University. This project is open for complete implementation by the Tennessee Department of Treasury if they so choose. For any other entity, it is not intended for commercial use or deployment in a production environment. Any redistribution or reproduction of part or all of the contents in any form is prohibited other than the following:

- You may print or download extracts for your personal and non-commercial use only.
- You may share the material with your classmates, instructors, or colleagues for educational purposes, acknowledging this project as the source of the material.

This project is provided "as is" without any representations or warranties, express or implied. The creators make no warranties regarding the accuracy, reliability, or completeness of this project. Neither the creators nor the educational institution will be liable for any damages arising in connection with the use of this project.
