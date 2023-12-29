# **Gara** - Vehicle statistic tracking application

## About
Gara is a personal project that originally started under the Flutter framework with a Dart code behind. After wanting to focus more on .Net environments and C#, I decided to start rebuilding the application using .Net Maui. 

## Intended Functionality
While neither this app nor the Flutter version was fully completed, the idea of the Application is to fulfill a few use cases as a Minimum version:

 - Allow users to enter their vehicles and track statistics (miles, services, etc.)
 - Users can enter refueling statistics (price, gallons, etc.) to track averages, see spending, and receive an overhead view of their vehicle usage and costs

Other functionalities may come later.

## Design
**Database:**
![GaraVehicleData](https://github.com/DeFuanz/GaraMaui/assets/76855046/f5316976-2065-46a1-ab03-3ed95cdd3093)

The current design is minimal and missing some of the functionalities to be incorporateed later such as vehicle information.
Authentication and user management is handled hrough Auth0. DB users is not actually a live table, instead the userId will be captured on login and used where needed.

**System Architecture:**
![GaraSystemDesign](https://github.com/DeFuanz/GaraMaui/assets/76855046/f9c266fa-8d04-4acb-8cd1-fe67b68fa962)

Simple 3-tier sytle architecture with the app, custom Rest API, and Database
