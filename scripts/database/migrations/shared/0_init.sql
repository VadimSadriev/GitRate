-- extensions 
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- database versions
CREATE TABLE IF NOT EXISTS "migrations"(
    id varchar(512) NOT NULL,
    create_date timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
    CONSTRAINT "PK_migrations" PRIMARY KEY (id)
);

-- functions
CREATE OR REPLACE FUNCTION add_migration(id text) RETURNS void AS $$ 
     INSERT INTO migrations(id) VALUES($1);
$$ LANGUAGE SQL;