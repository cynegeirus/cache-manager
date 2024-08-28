# Generic Cache Manager Application

Generic Cache Manager Application is a simple ASP.NET Core Web API project that uses Redis for caching. This project demonstrates how to use Redis as a caching layer in a web application, providing basic CRUD operations on the cache.

## Features

- Get a cached item by key
- Get all cached items
- Add a new item to the cache
- Delete an item from the cache by key
- Delete all items from the cache

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop) (for running Redis)
- [Redis](https://redis.io/download)

## Getting Started

### Setting up Redis

To run Redis locally using Docker, execute the following command:

```sh
docker run --name redis -d -p 6379:6379 redis
```

### Configuration

Ensure your `appsettings.json` is configured to connect to your Redis instance. The connection string should look something like this:

```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  }
}
```

### Installing Dependencies

Navigate to the project directory and restore the dependencies:

```sh
dotnet restore
```

### Running the Application

To run the application, use the following command:

```sh
dotnet run
```

The API will be available at `https://localhost:5001`.

## API Endpoints

### Get Item by Key

- **URL:** `GET /api/Redis/Get`
- **Parameters:** `key` (string) - The key of the cached item
- **Response:** A JSON object containing the cached item

```sh
curl -X GET "https://localhost:5001/api/Redis/Get?key=YOUR_CACHE_KEY"
```

### Get All Items

- **URL:** `GET /api/Redis/GetAll`
- **Response:** A JSON array containing all cached items

```sh
curl -X GET "https://localhost:5001/api/Redis/GetAll"
```

### Add Item

- **URL:** `POST /api/Redis/Add`
- **Body:**

```json
{
  "key": "yourKey",
  "value": "yourValue"
}
```

- **Response:** A JSON object indicating success or failure

```sh
curl -X POST "https://localhost:5001/api/Redis/Add" -H "Content-Type: application/json" -d '{"key":"YOUR_CACHE_KEY","value":"YOUR_CACHE_VALUE}'
```

### Delete Item by Key

- **URL:** `POST /api/Redis/Delete`
- **Parameters:** `key` (string) - The key of the cached item to delete
- **Response:** A JSON object indicating success or failure

```sh
curl -X POST "https://localhost:5001/api/Redis/Delete?key=YOUR_CACHE_KEY"
```

### Delete All Items

- **URL:** `POST /api/Redis/DeleteAll`
- **Response:** A JSON object indicating success or failure

```sh
curl -X POST "https://localhost:5001/api/Redis/DeleteAll"
```

## Project Structure

- **Controllers**
  - `RedisController.cs` - Handles API requests for cache operations
- **Models**
  - `CacheItemModel.cs` - Represents a cache item
  - `CacheResponseModel.cs` - Represents the response model for cache operations
- **Services**
  - **Abstract**
    - `IGlobalCacheService.cs` - Interface for cache service
  - **Concrete**
    - `RedisCacheService.cs` - Implementation of the cache service using Redis

## Constants

- **Messages.cs** - Contains constant messages used in responses

## Example Response Models

### CacheResponseModel

Represents a general response model for cache operations.

```csharp
public class CacheResponseModel
{
    public bool Success { get; set; }
    public string Message { get; set; }
}

public class CacheResponseModel<T> : CacheResponseModel
{
    public string Machine { get; set; }
    public List<CacheItemModel<T>> Items { get; set; } = new List<CacheItemModel<T>>();
}
```

### CacheItemModel

Represents a cache item.

```csharp
public class CacheItemModel<T>
{
    public string Key { get; set; }
    public T Value { get; set; }
    public DateTimeOffset Expiration { get; set; }
}
```

## License

This project is licensed under the [MIT License](LICENSE). See the license file for details.

## Issues, Feature Requests or Support

Please use the [New Issue](https://github.com/akinbicer/dotnet-cachemanager/issues/new) button to submit issues, feature requests or support issues directly to me. You can also send an e-mail to akin.bicer@outlook.com.tr.
