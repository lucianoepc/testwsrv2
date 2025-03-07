# Ejemplos de uso del API usando cURL

## Gestión de Personas

### Obtener todas las personas
```bash
curl -X GET http://localhost:5174/api/person \
     -H "Accept: application/json"
```

### Obtener una persona por ID
```bash
curl -X GET http://localhost:5174/api/person/1 \
     -H "Accept: application/json"
```

### Crear una nueva persona
```bash
curl -X POST http://localhost:5174/api/person \
     -H "Content-Type: application/json" \
     -d '{
         "code": "P00001",
         "firstName": "Juan",
         "lastName": "Pérez",
         "dateOfBirth": "1990-05-15",
         "placeOfBirth": "Lima",
         "identitydocumenttypeId": 1
     }'
```

### Actualizar una persona existente
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

### Eliminar una persona
```bash
curl -X DELETE http://localhost:5174/api/person/1
```

## Gestión de Tipos de Documento de Identidad

### Obtener todos los tipos de documento
```bash
curl -X GET http://localhost:5174/api/identitydocumenttype \
     -H "Accept: application/json"
```

### Obtener un tipo de documento por ID
```bash
curl -X GET http://localhost:5174/api/identitydocumenttype/1 \
     -H "Accept: application/json"
```

### Crear un nuevo tipo de documento
```bash
curl -X POST http://localhost:5174/api/identitydocumenttype \
     -H "Content-Type: application/json" \
     -d '{
         "code": "DNI",
         "description": "Documento Nacional de Identidad"
     }'
```

### Actualizar un tipo de documento existente
```bash
curl -X PUT http://localhost:5174/api/identitydocumenttype/1 \
     -H "Content-Type: application/json" \
     -d '{
         "id": 1,
         "code": "DNI",
         "description": "DNI - Documento Nacional de Identidad (Actualizado)"
     }'
```

### Eliminar un tipo de documento
```bash
curl -X DELETE http://localhost:5174/api/identitydocumenttype/1
```

## Gestión de Productos

### Obtener todos los productos
```bash
curl -X GET http://localhost:5174/api/product \
     -H "Accept: application/json"
```

### Obtener un producto por ID
```bash
curl -X GET http://localhost:5174/api/product/1 \
     -H "Accept: application/json"
```

### Crear un nuevo producto
```bash
curl -X POST http://localhost:5174/api/product \
     -H "Content-Type: application/json" \
     -d '{
         "code": "PROD001",
         "name": "Producto 1",
         "description": "Descripción del producto 1",
         "baseCost": 100.50
     }'
```

### Actualizar un producto existente
```bash
curl -X PUT http://localhost:5174/api/product/1 \
     -H "Content-Type: application/json" \
     -d '{
         "id": 1,
         "code": "PROD001",
         "name": "Producto 1 - Actualizado",
         "description": "Nueva descripción del producto 1",
         "baseCost": 120.75
     }'
```

### Eliminar un producto
```bash
curl -X DELETE http://localhost:5174/api/product/1
```

## Gestión de Productos Vendidos

### Obtener todos los productos vendidos
```bash
curl -X GET http://localhost:5174/api/soldproducts \
     -H "Accept: application/json"
```

### Obtener productos vendidos de una persona
```bash
curl -X GET http://localhost:5174/api/soldproducts/person/1 \
     -H "Accept: application/json"
```

### Registrar una venta de producto
```bash
curl -X POST http://localhost:5174/api/soldproducts \
     -H "Content-Type: application/json" \
     -d '{
         "personId": 1,
         "productId": 1,
         "quantity": 2,
         "saleDate": "2024-01-20T10:30:00Z"
     }'
```

# Respuestas esperadas

Los servicios responderán con los siguientes códigos HTTP:

- 200 OK: Para operaciones exitosas de GET y PUT
- 201 Created: Para operaciones exitosas de POST
- 204 No Content: Para operaciones exitosas de DELETE
- 400 Bad Request: Cuando los datos proporcionados no son válidos
- 404 Not Found: Cuando se intenta acceder a un recurso que no existe

Las respuestas incluirán el objeto en formato JSON con la estructura correspondiente a cada entidad.
