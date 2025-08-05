# Mi API en .NET Core

Esta es una API desarrollada con ASP.NET Core en conjunto con una aplicaci�n Web
es para el desarrollo de control de un elevador.

## C�mo usar

- Clona el repositorio
- Deber�s ejecutar el archivo Script.sql que se encuentra en la carpeta Solution Items, el query crea la bd y tablas para el login
- Restaura los paquetes NuGet
- Ejecuta la aplicaci�n

## Endpoints principales

### Para Usuario

Para poder ejecutar el registro de un usuario requieres hacer la petici�n en el siguiente endpoint incluyendo el  json de entrada
- GET /api/Usuario/registro
  El json de entrada es el siguiente: 
  {
  "nombre": "string",
  "correo": "string",
  "clave": "string"
  }

  Lo que obtendr�s como respuesta una vez creado el usuario es el siguiente json, 
  donde se enviara el token para su uso en los endpoints donde sea requerido y el tiempo de expiraci�n:
  {
  "success": true,
  "data": {
    "token": "string",
    "expiracion": "2025-07-29T20:34:12.695Z"
  },
  "errors": [
    "string"
  ]
  }

Para realizar el login deberas hacer la petici�n al siguiente endpoint
- POST /api/Usuario/login
  
  El json de entrada que deber�s enviar en la petici�n es el siguiente:

  {
     "correo": "string",
     "clave": "string"
  }

  Lo que obtendr�s como respuesta una vez creado el usuario es el siguiente json, 
  donde se enviara el token para su uso en los endpoints donde sea requerido y el tiempo de expiraci�n:

  {
  "success": true,
  "data": {
    "token": "string",
    "expiracion": "2025-07-29T20:37:44.484Z"
  },
  "errors": [
    "string"
  ]
}

# ElevadorController API Documentation

Este controlador expone endpoints para operar un elevador, permitiendo subir, bajar y llamar el elevador a un piso espec�fico. Todos los endpoints requieren autenticaci�n JWT.

## Endpoints

### 1. Subir el elevador

**POST** `/api/elevador/subir`

- **Descripci�n:** Mueve el elevador hacia arriba al piso solicitado.
- **JSON de entrada:
  {
  "pisoActual": 0,
  "puertas": 0,
  "estadoMovimiento": 0,
  "direccionActual": 0,
  "pisoSolicitado": 0,
  "direccionSolicitada": 0
}

_ **JSON de salida:
    {
  "success": true,
  "data": {
    "pisoActual": 0,
    "puertas": 0,
    "estado": 0,
    "direccionActual": 0
  },
  "errors": [
    "string"
  ]
}


### 2. Bajar el elevador

**POST** `/api/elevador/bajar`

- **Descripci�n:** Mueve el elevador hacia abajo al piso solicitado.
- **JSON de entrada:
  {
  "pisoActual": 0,
  "puertas": 0,
  "estadoMovimiento": 0,
  "direccionActual": 0,
  "pisoSolicitado": 0,
  "direccionSolicitada": 0
}

_ **JSON de salida:
    {
  "success": true,
  "data": {
    "pisoActual": 0,
    "puertas": 0,
    "estado": 0,
    "direccionActual": 0
  },
  "errors": [
    "string"
  ]
}


## Modelos

### SolicitudElevadorDTO (entrada)
- `pisoActual`: int � Piso donde est� el elevador actualmente.
- `puertas`: string � Estado de las puertas (`Abierta` o `Cerrada`).
- `estadoMovimiento`: string � Estado del movimiento (`Parado` o `Moviendo`).
- `direccionActual`: string � Direcci�n actual (`Subir`, `Bajar`, `Ninguna`).
- `pisoSolicitado`: int � Piso al que se solicita mover el elevador.
- `direccionSolicitada`: string � Direcci�n solicitada (`Subir`, `Bajar`, `Ninguna`).

### ApiResponse<ElevadorEstadoDTO> (salida)
- `success`: bool � Indica si la operaci�n fue exitosa.
- `data`: ElevadorEstadoDTO � Estado actualizado del elevador.
- `errors`: string[] � Lista de errores si la operaci�n falla.

---

## Notas

- Todos los endpoints requieren autenticaci�n JWT.
- Los valores de los enums deben enviarse como texto (`"Subir"`, `"Bajar"`, `"Ninguna"`, `"Abierta"`, `"Cerrada"`, `"Parado"`, `"Moviendo"`).
- En caso de error, el campo `errors` contendr� la descripci�n del problema.
