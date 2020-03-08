
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM migrations where id = 'add_user_table') THEN
        CREATE TABLE users(
            id uuid NOT NULL DEFAULT uuid_generate_v1(),
            user_name varchar(256) NOT NULL,
            normalized_user_name varchar(256) NOT NULL,
            email varchar(256) NULL,
            normalized_email varchar(256) NULL,
            password_hash varchar(512) NULL,
            create_date timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
            modified_date timestamp with time zone NULL,
            CONSTRAINT "PK_users" PRIMARY KEY (id)
        );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM migrations where id = 'add_user_table') THEN
        PERFORM add_migration('add_user_table');
    END IF;
END $$;