# Scripts de Base de Datos

Este directorio contiene los scripts necesarios para crear la estructura de base de datos requerida por la aplicación.

## Prerequisitos

- PostgreSQL 12 o superior
- Un usuario con permisos para crear schemas y tablas en la base de datos

## Pasos para la Instalación

1. Asegúrese de que PostgreSQL esté instalado y ejecutándose
2. Cree una base de datos si aún no existe:
```sql
CREATE DATABASE dbfenix;
```

3. Ejecute el script usando psql:
```bash
psql -h localhost -U usr_owner_fnx -d dbfenix -f create_database.sql
```

O desde pgAdmin4, puede:
1. Conectarse a la base de datos dbfenix
2. Abrir la herramienta Query Tool
3. Cargar y ejecutar el archivo create_database.sql

## Estructura Creada

El script creará:

1. Un schema 'sales'
2. Las siguientes tablas:
   - identity_document_types: Tipos de documento de identidad
   - products: Productos
   - people: Personas
   - sold_products: Productos vendidos

3. Datos iniciales:
   - Tipos básicos de documentos de identidad

## Notas

- El script usa 'IF NOT EXISTS' para que sea seguro ejecutarlo múltiples veces
- Se crean índices para optimizar las consultas más comunes
- Las eliminaciones están restringidas por las claves foráneas para mantener la integridad referencial