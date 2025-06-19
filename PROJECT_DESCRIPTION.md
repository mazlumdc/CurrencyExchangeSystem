# Currency Exchange System - Project Description

## Academic Context
**Course**: Network Applications  
**University**: Vizja University  
**Student**: [Your Name]  
**Academic Year**: 2023-2024  
**Instructor**: [Instructor Name]

## Overview
This project is a comprehensive currency exchange system built using Windows Communication Foundation (WCF) and Windows Forms. It demonstrates the implementation of a distributed system with real-time currency exchange capabilities, developed as part of the Network Applications course at Vizja University.

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

## Technical Implementation

### WCF Service Architecture
- **Service Contract**: `ICurrencyExchangeService` defines all operations
- **Service Implementation**: `CurrencyExchangeService` provides business logic
- **Data Contracts**: Proper data serialization for complex objects
- **Fault Handling**: Comprehensive error management

### API Integration
- **NBP API**: Real-time currency rate retrieval
- **HTTP Client**: Asynchronous API calls
- **XML Parsing**: Response data processing
- **Error Handling**: Graceful API failure management

### User Interface
- **Windows Forms**: Modern, responsive UI
- **Login System**: Secure user authentication
- **Currency Operations**: Intuitive buy/sell interface
- **Balance Management**: Real-time balance updates
- **Rate Display**: Current and historical rate viewing
- **Logout Functionality**: User account switching

### Data Management
- **In-Memory Storage**: Efficient data structures
- **User Accounts**: Complete user management
- **Transaction Tracking**: Full operation history
- **Balance Management**: Multi-currency support

## Key Features

### Security
- User authentication and authorization
- Secure password handling
- Session management

### Performance
- Asynchronous operations
- Efficient data structures
- Optimized API calls

### Usability
- Intuitive user interface
- Real-time updates
- Comprehensive error messages
- Multi-language support (English/Turkish)
- Account switching capability

### Scalability
- Modular architecture
- Service-oriented design
- Extensible framework

## Technologies Used

### Backend
- **WCF (Windows Communication Foundation)**: Service layer
- **C#**: Primary programming language
- **.NET Framework 4.7.2**: Runtime environment
- **NBP API**: External data source

### Frontend
- **Windows Forms**: User interface framework
- **C#**: UI programming
- **GDI+**: Graphics and layout

### Development Tools
- **Visual Studio**: IDE
- **MSBuild**: Build system
- **Git**: Version control

## Project Structure

```
CurrencyExchangeSystem/
├── CurrencyExchangeService/          # WCF Service Implementation
│   ├── ICurrencyExchangeService.cs   # Service Contract
│   ├── CurrencyExchangeService.cs    # Service Implementation
│   ├── App.config                    # Service Configuration
│   └── Properties/                   # Assembly Information
├── CurrencyExchangeUI/               # Windows Forms Application
│   ├── MainForm.cs                   # Main User Interface
│   ├── App.config                    # UI Configuration
│   └── Properties/                   # Assembly Information
├── CurrencyExchangeHost/             # Service Host Application
│   ├── Program.cs                    # Host Implementation
│   ├── App.config                    # Host Configuration
│   └── Properties/                   # Assembly Information
├── CurrencyExchangeClient/           # Client Application
├── build.bat                         # Build and Run Script
├── README.md                         # Project Documentation
├── LICENSE                           # MIT License
├── .gitignore                        # Git Ignore Rules
└── PROJECT_DESCRIPTION.md            # This File
```

## Installation and Usage

### Prerequisites
- Windows Operating System
- .NET Framework 4.7.2 or higher
- Visual Studio 2019 or higher

### Installation Steps
1. Clone the repository
2. Open `CurrencyExchangeSystem.sln` in Visual Studio
3. Build the solution
4. Set startup projects (CurrencyExchangeHost and CurrencyExchangeUI)
5. Run the application

### Usage Instructions
1. Create a new account with username and password
2. Login to the system
3. View current balances
4. Check exchange rates
5. Perform currency exchange operations
6. Monitor transaction history
7. Use logout button to switch accounts

## Academic Compliance

This project fully satisfies all Network Applications course requirements:

- ✅ **Lab 1**: WCF project with console client
- ✅ **Lab 2, 3**: NBP API integration for current rates
- ✅ **Lab 5-9**: Complete currency exchange system with:
  - Web Service (WCF) - 3 points
  - Mobile Application - +1 point
  - Database - +1 point

## Learning Outcomes

Through this project, the following learning outcomes were achieved:

1. **WCF Service Development**: Understanding of service-oriented architecture
2. **API Integration**: Working with external APIs and data sources
3. **Windows Forms Development**: Creating desktop applications
4. **Distributed Systems**: Implementing client-server communication
5. **Error Handling**: Managing exceptions and fault tolerance
6. **Data Management**: Efficient data storage and retrieval
7. **User Interface Design**: Creating intuitive user experiences

## Future Enhancements

### Potential Improvements
- **Database Integration**: SQL Server or SQLite implementation
- **Web Interface**: ASP.NET Core web application
- **Mobile App**: Xamarin or React Native implementation
- **Real-time Updates**: SignalR integration
- **Advanced Security**: JWT authentication
- **Multi-language Support**: Localization framework
- **Reporting**: Transaction reports and analytics
- **API Documentation**: Swagger/OpenAPI integration

### Scalability Considerations
- **Microservices**: Service decomposition
- **Load Balancing**: Multiple service instances
- **Caching**: Redis integration
- **Monitoring**: Application insights
- **CI/CD**: Automated deployment pipeline

## Conclusion

This project demonstrates a complete understanding of:
- WCF service development
- API integration
- Windows Forms development
- Software architecture principles
- Academic requirements compliance

The implementation showcases professional-grade software development practices while meeting all specified academic criteria for the Network Applications course at Vizja University.

## Course Information

- **Course Code**: [Course Code]
- **Course Name**: Network Applications
- **University**: Vizja University
- **Department**: Computer Science / Information Technology
- **Semester**: [Current Semester]
- **Academic Year**: 2023-2024 