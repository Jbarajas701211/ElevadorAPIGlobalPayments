# My API in .NET Core

This is an API developed with ASP.NET Core in conjunction with a Web application
for developing elevator controls.


## How to use

- Clone the repository
- You must run the Script.sql file located in the Solution Items folder. The query creates the database and tables for the login
- Restore the NuGet packages
- Run the application and you can see in the top a menu, select the option Elevator

## Main Endpoints

### For User

To register a user, you need to make the request to the following endpoint, including the input JSON:
- GET /api/User/registration
The input JSON is as follows:
{
"name": "string",
"email": "string",
"password": "string"
}

The response you will receive once the user is created is the following JSON,
where the token will be sent for use on the endpoints where it is required, along with the expiration time:
{
"success": true,
"data": {
"token": "string",
"expiration": "2025-07-29T20:34:12.695Z"
},
"errors": [
"string"
]
}

To log in, you must make a request to the following endpoint:

- POST /api/User/login

The input JSON you must send in the request is the following:

{
"email": "string",
"password": "string"
}

Once the user is created, you will receive the following JSON response,
where the token will be sent for use on the endpoints where it is required, along with the expiration time:

{
"success": true,
"data": {
"token": "string",
"expiration": "2025-07-29T20:37:44.484Z"
},
"errors": [
"string"
]
}

# ElevatorController API Documentation

This controller exposes endpoints for operating an elevator, allowing you to raise and lower the elevator, and call the elevator to a specific floor. All endpoints require JWT authentication.

## Endpoints

### 1. Raise the elevator

**POST** `/api/elevator/upload`

- **Description:** Moves the elevator up to the requested floor.
- **Input JSON:
{
"currentFloor": 0,
"doors": 0,
"movementState": 0,
"currentAddress": 0,
"requestedFloor": 0,
"requestedAddress": 0
}

_ **Output JSON:
{
"success": true,
"data": {
"currentFloor": 0,
"doors": 0,
"status": 0,
"currentAddress": 0
},
"errors": [
"string"
]
}

### 2. Lower the elevator

**POST** `/api/elevator/down`

- **Description:** Moves the elevator down to the requested floor.
- **Input JSON:
{
"currentFloor": 0,
"doors": 0,
"movementState": 0,
"currentAddress": 0,
"requestedFloor": 0,
"requestedAddress": 0
}

_ **Output JSON:
{
"success": true,
"data": {
"currentFloor": 0,
"doors": 0,
"status": 0,
"currentAddress": 0
},
"errors": [
"string"
]
}
## Models

### ElevatorDTORequest (input)
- `currentFloor`: int — Floor where the elevator is currently located.
- `doors`: string — Door status (`Open` or `Closed`).
- `movementState`: string — Movement status (`Stopped` or `Moving`).
- `currentDirection`: string — Current direction (`Up`, `Down`, `None`).
- `requestedFloor`: int — Floor to which the elevator is being requested to move.
- `requestedDirection`: string — Requested direction (`Up`, `Down`, `None`).

### ApiResponse<ElevatorDTOStatus> (output)
- `success`: bool — Indicates whether the operation was successful.
- `data`: ElevatorDTOStatus — Updated elevator status.
- `errors`: string[] — List of errors if the operation fails.

---

## Notes

- All endpoints require JWT authentication.
- Enum values must be sent as text (`Up`, `Down`, `None`, `Open`, `Closed`, `Stopped`, `Moving`).
- In case of an error, the `errors` field will contain a description of the problem.