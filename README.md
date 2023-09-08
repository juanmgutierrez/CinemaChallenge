# Cinema

DDD & Clean Architecture example with .NET Core 7

The solution provides with an API for creating showtimes, and reserving and buying seats.

---

## Instructions for running the solution

### Starting the API

You will need docker in order to use this API and then run the next command first:

```powershell
docker-compose up
```

### Correcting the Provided API database file

Inside the Provided API container, there is a file for the movies database that it has an incorrect name. So, you need to correct it before running the API.

To do that, run the next command inside the container:

```bash
cd /app
mv amovies-db.json movies-db.json
```

> **This is important, if not, it will return only a fake movie data**

### Playing with the API

Then run the solution, with the API as startup project

In [cUrls.txt](./cUrls.txt) you can find the curl commands for each of the commands and queries that are implemented

As well, [requests.http](./requests.http) is a good option to test the API (for instance, from Visual Studio)

### Stopping the API

When you end the test, run the next command:

```powershell
docker-compose down
```

---

## How it's designed

The solution is designed with **Clean Architecture & DDD**

The connection with the external MoviesAPI is made with **gRPC**. Channel is reused for better performance.

The commands and queries were implemented following the **CQRS pattern**, with **Mediatr** library.

Time tracking logging and model validations are implmented using Mediatr pipeline behaviors. And for model validations, FluentValidation is used.

Database is designed with code-first approach (using **FluentAPI**), with **Entity Framework Core**, and it's an in-memory **SQLite** database. It's seeded with some data for testing purposes.

> I changed from in-memory (from EF Core) to SQLite because with no extra effort it bring a better solution, more stable and with more capabilities.

**Cache** is implemented with **Redis**. It's configured to cache the response from the MoviesAPI.

> I changed a little the solution for caching the external MovieAPI response:
> 1. First, it looks in Redis, if it's there and it's not old data, it returns it
> 2. If it's not in Redis or the obtained value is old, it calls the MoviesAPI through the gRPC proxy, and if it's successful, it caches the response in Redis and returns it
> 3. Finally, if the MoviesAPI call fails, it returns the old cached value from Redis, or null if it's not in Redis database

The solution has Architecture Tests and Unit Tests. They are using **xUnit** and **FluentAssertions**, and the mocks and stubs are implmeented with **NSubstitute**.

The API is documented with **Swagger**.

I placed an .editorconfig file to keep the code style consistent in all projects.

Endpoints for the API are coded with MinimalAPI approach.

---

## Future improvements

1. Better code coverage with unit tests	
1. Finish the implementation of Error Handling (it's advanced but not finished)
1. Add integration tests
1. Use of Option Pattern for configuration
1. Implement Unit of Work pattern
1. Provide not only a REST API, but also a gRPC API (using gRPC JSON transcoding)
1. Add Rate Limit to the API
1. Add more logging and configure Serilog
1. Add Authentication and Authorization to the API
1. Design a solution that handles concurrent reservations

---

## Context

This is part of a challenge. The description of the problem is:

> We want a C# .Net Core Web Application API project that meets our requirements. We provide you with the solution skeleton and a few features implemented to save time.

> You will find the Data layer is implemented and is instructed to be an In-Memory Database. 

> The application represents a Cinema. We want to manage the showtimes of the cinema, getting some data from a provided API

> The test includes a docker-compose with Redis and the ProvidedApi, you will need docker to run them.

> We only want the following features:

> - Create showtimes
>   - Should create showtime and should grab the movie data from the ProvidedApi
> - Reserve seats
>   - Reserving the seat response will contain a GUID of the reservation, also the number of seats, the auditorium used and the movie that will be played
>   - It should not be possible to reserve the same seats two times in 10 minutes
>   - It shouldn't be possible to reserve an already sold seat
>   - All the seats, when doing a reservation, need to be contiguous
> - Buy seats
>   - We will need the GUID of the reservation, it is only possible to do it while the seats are reserved
>   - It is not possible to buy two times the same seat
>   - We are not going to use a Payment abstraction for this case, just have an Endpoint which I can Confirm a Reservation
