-- Create schema if not exists
CREATE SCHEMA IF NOT EXISTS sales;

-- Set the search path for the current session
SET search_path TO sales;

-- Create identity_document_types table
CREATE TABLE IF NOT EXISTS sales.identity_document_types (
    id SERIAL PRIMARY KEY,
    code VARCHAR(10) NOT NULL,
    description VARCHAR(100) NOT NULL,
    CONSTRAINT uk_identity_document_types_code UNIQUE (code)
);

-- Create products table
CREATE TABLE IF NOT EXISTS sales.products (
    id SERIAL PRIMARY KEY,
    code VARCHAR(20) NOT NULL,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    base_cost DECIMAL(10,2) NOT NULL,
    CONSTRAINT uk_products_code UNIQUE (code)
);

-- Create people table
CREATE TABLE IF NOT EXISTS sales.people (
    id SERIAL PRIMARY KEY,
    code VARCHAR(20) NOT NULL,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    date_of_birth DATE NOT NULL,
    place_of_birth VARCHAR(100) NOT NULL,
    identity_document_type_id INTEGER NOT NULL,
    CONSTRAINT uk_people_code UNIQUE (code),
    CONSTRAINT fk_people_identity_document_type FOREIGN KEY (identity_document_type_id)
        REFERENCES sales.identity_document_types (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);

-- Create sold_products table
CREATE TABLE IF NOT EXISTS sales.sold_products (
    id SERIAL PRIMARY KEY,
    person_id INTEGER NOT NULL,
    person_code VARCHAR(20) NOT NULL,
    product_id INTEGER NOT NULL,
    product_code VARCHAR(20) NOT NULL,
    sale_date TIMESTAMP NOT NULL,
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    CONSTRAINT fk_sold_products_person FOREIGN KEY (person_id)
        REFERENCES sales.people (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    CONSTRAINT fk_sold_products_product FOREIGN KEY (product_id)
        REFERENCES sales.products (id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);

-- Create indexes
CREATE INDEX IF NOT EXISTS idx_people_identity_document_type ON sales.people(identity_document_type_id);
CREATE INDEX IF NOT EXISTS idx_sold_products_person ON sales.sold_products(person_id);
CREATE INDEX IF NOT EXISTS idx_sold_products_product ON sales.sold_products(product_id);
CREATE INDEX IF NOT EXISTS idx_sold_products_sale_date ON sales.sold_products(sale_date);

-- Insert some basic identity document types
INSERT INTO sales.identity_document_types (code, description) 
VALUES 
    ('DNI', 'Documento Nacional de Identidad'),
    ('CE', 'Carnet de Extranjería'),
    ('PAS', 'Pasaporte')
ON CONFLICT (code) DO UPDATE 
    SET description = EXCLUDED.description;