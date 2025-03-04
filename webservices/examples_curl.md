# Ejemplos de uso del API de Personas usando cURL

## Obtener todas las personas
```bash
curl -X GET http://localhost:5174/api/person \
     -H "Accept: application/json"
```

## Obtener una persona por ID
```bash
curl -X GET http://localhost:5174/api/person/1 \
     -H "Accept: application/json"
```

## Crear una nueva persona
```bash
curl -X POST http://localhost:5174/api/person \
     -H "Content-Type: application/json" \
     -d '{
         "firstName": "Juan",
         "lastName": "Pérez",
         "dateOfBirth": "1990-05-15",
         "placeOfBirth": "Lima"
     }'
```

## Actualizar una persona existente
```bash
curl -X PUT http://localhost:5174/api/person/1 \
     -H "Content-Type: application/json" \
     -d '{
         "id": 1,
         "firstName": "Juan Carlos",
         "lastName": "Pérez",
         "dateOfBirth": "1990-05-15",
         "placeOfBirth": "Lima"
     }'
```

## Eliminar una persona
```bash
curl -X DELETE http://localhost:5174/api/person/1
```

# Respuestas esperadas

Los servicios responderán con los siguientes códigos HTTP:

- 200 OK: Para operaciones exitosas de GET y PUT
- 201 Created: Para operaciones exitosas de POST
- 204 No Content: Para operaciones exitosas de DELETE
- 400 Bad Request: Cuando los datos proporcionados no son válidos
- 404 Not Found: Cuando se intenta acceder a una persona que no existe

Las respuestas para GET y POST/PUT incluirán el objeto persona en formato JSON con esta estructura:

```json
{
    "id": 1,
    "firstName": "Juan",
    "lastName": "Pérez",
    "dateOfBirth": "1990-05-15T00:00:00",
    "placeOfBirth": "Lima",
    "age": 33
}
```

Nota: El campo "age" se calcula automáticamente basado en la fecha de nacimiento.
