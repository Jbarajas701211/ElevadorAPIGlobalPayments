# Mi API en .NET Core

Esta es una API desarrollada con ASP.NET Core en conjunto con una aplicación Web
es para el desarrollo de control de un elevador.

## Cómo usar

- Clona el repositorio
- Deberás ejecutar el archivo Script.sql que se encuentra en la carpeta Solution Items, el query crea la bd y tablas para el login
- Restaura los paquetes NuGet
- Ejecuta la aplicación

## Endpoints principales

### Para Usuario

Para poder ejecutar el registro de un usuario requieres hacer la petición en el siguiente endpoint incluyendo el  json de entrada
- GET /api/Usuario/registro
  El json de entrada es el siguiente: 
  {
  "nombre": "string",
  "correo": "string",
  "clave": "string"
  }

  Lo que obtendrás como respuesta una vez creado el usuario es el siguiente json, 
  donde se enviara el token para su uso en los endpoints donde sea requerido y el tiempo de expiración:
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

Para realizar el login deberas hacer la petición al siguiente endpoint
- POST /api/Usuario/login
  
  El json de entrada que deberás enviar en la petición es el siguiente:

  {
     "correo": "string",
     "clave": "string"
  }

  Lo que obtendrás como respuesta una vez creado el usuario es el siguiente json, 
  donde se enviara el token para su uso en los endpoints donde sea requerido y el tiempo de expiración:

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

Este controlador expone endpoints para operar un elevador, permitiendo subir, bajar y llamar el elevador a un piso específico. Todos los endpoints requieren autenticación JWT.

## Endpoints

### 1. Subir el elevador

**POST** `/api/elevador/subir`

- **Descripción:** Mueve el elevador hacia arriba al piso solicitado.
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

- **Descripción:** Mueve el elevador hacia abajo al piso solicitado.
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
- `pisoActual`: int — Piso donde está el elevador actualmente.
- `puertas`: string — Estado de las puertas (`Abierta` o `Cerrada`).
- `estadoMovimiento`: string — Estado del movimiento (`Parado` o `Moviendo`).
- `direccionActual`: string — Dirección actual (`Subir`, `Bajar`, `Ninguna`).
- `pisoSolicitado`: int — Piso al que se solicita mover el elevador.
- `direccionSolicitada`: string — Dirección solicitada (`Subir`, `Bajar`, `Ninguna`).

### ApiResponse<ElevadorEstadoDTO> (salida)
- `success`: bool — Indica si la operación fue exitosa.
- `data`: ElevadorEstadoDTO — Estado actualizado del elevador.
- `errors`: string[] — Lista de errores si la operación falla.

---

## Notas

- Todos los endpoints requieren autenticación JWT.
- Los valores de los enums deben enviarse como texto (`"Subir"`, `"Bajar"`, `"Ninguna"`, `"Abierta"`, `"Cerrada"`, `"Parado"`, `"Moviendo"`).
- En caso de error, el campo `errors` contendrá la descripción del problema.
