# PhotoBoxTestApi

# Execution
This project was written using Visual Studio 2022 and with a SQL Server database (easiest access I had on hand). A SQL script is included in the source if you wish to recreate the database for testing

## Functionality
A .NET core WebAPI application to provide two entry points into the orders function of the PhotoBox system.

- The first allows you to create a new order with 1 or many order items in a single payload associated with the order id given. This will create an order and linked order item records into the datastore and if returning positively provides the minimum bin width required to fullfill the order.
- The second allows you to retrieve a full order with its order items and required minimum bin width with the order id given at point of request

## Architecture
The project is using a simple multi-tier architecture providing separation of concerns.
- The web API controller recieves the requests for adding or retrieving data, applying payload validation where it can
- Helpers provide discrete calculations to know inputs (minimum width calculations in this instance)
- Models provide containers for data. In this instance DTO models for transferring data in and out of the API and a supporting structure for defining a product type
- A datastore for adding and retrieving orders from the database (MSSQL in this instance)

## Testing
I decided that the most prudent testing would be to make sure the minimum width calculation was rock solid with multiple combinations of items within one order, paying close attention to the mug stacking calculation and to test the basic adding and retrieving of the orders with the controller

# Additional decisions and considerations for production implementations
I chose to define the product types in code for simplicity and to show diversity of my programming skills. If this were for production then I would store the product definitions within the datastore and keep a cached version on hand when performing the width calculations

Happy to discuss any and all of this project.
