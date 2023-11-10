drop schema if exists auth cascade;
create schema auth;
set search_path=auth;

-- this is a passwordless system, so there's no need to worry about hashes
-- instead, we'll use email to send out magic links
-- or use OAuth
create table users(
  id serial primary key,
  name text,
  email text not null unique,
  magic_link text, 
  magic_link_expires_at timestamptz,
  microsoft text,
  google text,
  github text, -- add whatever provider IDs you want here
  last_login timestamptz not null default now(),
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now() 
);
create type log_level as enum ('debug', 'info', 'warn', 'error');
create table logs(
  id bigserial primary key,
  event text not null,
  level log_level not null default 'info',
  message text,
  data jsonb,
  created_at timestamp with time zone NOT NULL DEFAULT now()
);