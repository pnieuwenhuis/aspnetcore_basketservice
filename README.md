# Simple BasketService API on ASP.NET Core

A simple basket service API built on ASP.NET Core 1.0 with Akka.NET.

## Installation

```bash
dotnet restore
```

## Starting the API

The API can be started using:

```bash
dotnet run
```

## API Usage

Getting the product list: `GET - http://localhost:5000/api/products`.

Getting the contents of a basket: `GET - http://localhost:5000/api/basket/1`.

Add an item to the basket: `PUT - http://localhost:5000/api/basket/1/items` with JSON body:

```json
{
	"productId": 1000,
	"amount": 1
}
```

Remove an item from the basket: `DELETE - http://localhost:5000/api/basket/1/items/{basketItemGUID}`.



