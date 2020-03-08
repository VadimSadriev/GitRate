#!/bin/sh

# database connection settings
username="postgres"
password="postgres"
host="localhost"
port="5432"
default_database="postgres"
user_database="user_store"
main_database="git_rate"
default_connection_string=postgresql://$username:$password@$host:$port/$default_database
main_database_connection_string=postgresql://$username:$password@$host:$port/$main_database
user_database_connection_string=postgresql://$username:$password@$host:$port/$user_database

# sql files
main_db_sql_files="migrations/main/*"
user_db_sql_files="migrations/user/*"
shared_sql_files="migrations/shared/*"

# db create
psql -c "CREATE DATABASE $main_database" $default_connection_string
psql -c "CREATE DATABASE $user_database" $default_connection_string

# apply shared migrations
for file in $shared_sql_files
do
    psql -f $file $user_database_connection_string
    psql -f $file $main_database_connection_string
done

# apply migrations to main db
for file in $main_db_sql_files
do 
 psql -f $file $main_database_connection_string
done

# apply migrations to user db
for file in $user_db_sql_files
do 
 psql -f $file $user_database_connection_string
done

$SHELL