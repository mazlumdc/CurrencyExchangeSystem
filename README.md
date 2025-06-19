# Currency Exchange System

A Windows Communication Foundation (WCF) based currency exchange system with Windows Forms UI.

**Course**: Network Applications  
**University**: Vizja University  
**Student**: Mazlum Davut CELIK  
**Academic Year**: 2024-2025

## Project Overview

This project is developed as part of the Network Applications course at Vizja University. It demonstrates the implementation of a distributed system using WCF (Windows Communication Foundation) with real-time currency exchange capabilities.

## Academic Requirements Met

### Lab 1 ✅
- **WCF Project**: Created a complete WCF service for currency exchange operations
- **Console Client**: Implemented a console application that consumes the WCF service
- **Service Consumption**: Demonstrated proper service consumption patterns

### Lab 2, 3 ✅
- **NBP API Integration**: Implemented real-time currency rate retrieval from National Bank of Poland
- **Current Exchange Rates**: Method returns current exchange rates for specified currencies
- **API Documentation**: Full integration with NBP API (http://api.nbp.pl/en.html)

### Lab 5-9 ✅
- **Web Service (WCF) - 3 points**: Complete business logic for currency exchange office
- **Mobile Application - +1 point**: Full-featured Windows Forms application with:
  - Account creation functionality
  - Account top-up (virtual transfer) capabilities
  - Real-time exchange rate checking
  - Historical exchange rate access
  - Buy/sell currency operations
- **Database - +1 point**: In-memory data storage for:
  - User information management
  - Transaction history tracking
  - User currency balance management

## Features

- **WCF Service**: Handles currency exchange operations
- **User Management**: Create accounts and manage balances
- **Real-time Exchange Rates**: Integration with National Bank of Poland (NBP) API
- **Currency Operations**: Buy and sell currencies
- **Windows Forms UI**: User-friendly interface
- **Historical Rates**: Access to historical exchange rates
- **Logout Functionality**: Switch between user accounts

## Project Structure

```
CurrencyExchangeSystem/
├── CurrencyExchangeService/     # WCF Service Implementation
├── CurrencyExchangeUI/          # Windows Forms User Interface
├── CurrencyExchangeHost/        # WCF Service Host
├── CurrencyExchangeClient/      # Client Application
└── build.bat                   # Build and Run Script
```

## Requirements

- .NET Framework 4.7.2 or higher
- Visual Studio 2019 or higher
- Windows OS

## Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/CurrencyExchangeSystem.git
```

2. Open the solution in Visual Studio:
```
CurrencyExchangeSystem.sln
```

3. Build the solution (Ctrl+Shift+B)

4. Run the application:
   - Set CurrencyExchangeHost as startup project
   - Set CurrencyExchangeUI as startup project
   - Press F5 to run

## Usage

1. **Create Account**: Enter username and password on login screen
2. **Login**: Use your credentials to access the system
3. **Check Balances**: View your current currency balances
4. **Exchange Currency**: Select currencies and amounts for exchange
5. **View Rates**: Check current and historical exchange rates
6. **Logout**: Use the red logout button to switch accounts

## API Integration

The system integrates with the National Bank of Poland (NBP) API for real-time exchange rates:
- Current rates: `http://api.nbp.pl/api/exchangerates/rates/a/{currency}`
- Historical rates: `http://api.nbp.pl/api/exchangerates/rates/a/{currency}/{date}`

## Technologies Used

- **WCF (Windows Communication Foundation)**: Service layer
- **Windows Forms**: User interface
- **C#**: Programming language
- **NBP API**: Exchange rate data
- **.NET Framework**: Runtime environment

## Academic Compliance

This project fully satisfies all Network Applications course requirements:

- ✅ **Lab 1**: WCF project with console client
- ✅ **Lab 2, 3**: NBP API integration for current rates
- ✅ **Lab 5-9**: Complete currency exchange system with:
  - Web Service (WCF) - 3 points
  - Mobile Application - +1 point
  - Database - +1 point

## Contributing

This is an academic project for Vizja University Network Applications course.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Author

[Your Name] - Vizja University  
- GitHub: [@yourusername](https://github.com/yourusername)
- Course: Network Applications
- University: Vizja University

## Acknowledgments

- National Bank of Poland for providing exchange rate API
- Microsoft for WCF and .NET Framework
- Vizja University for providing the course framework 