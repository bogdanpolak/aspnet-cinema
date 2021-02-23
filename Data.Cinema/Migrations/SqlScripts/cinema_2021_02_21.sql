CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE movies (
    movieid integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    title text NOT NULL,
    CONSTRAINT "PK_movies" PRIMARY KEY (movieid)
);

CREATE TABLE rooms (
    roomid integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    name text NOT NULL,
    rows integer NOT NULL,
    columns integer NOT NULL,
    CONSTRAINT "PK_rooms" PRIMARY KEY (roomid)
);

CREATE TABLE shows (
    showid integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    movieid integer NOT NULL,
    roomid integer NOT NULL,
    start timestamp without time zone NOT NULL,
    CONSTRAINT shows_pkey PRIMARY KEY (showid),
    CONSTRAINT shows_movieid_fkey FOREIGN KEY (movieid) REFERENCES movies (movieid) ON DELETE RESTRICT,
    CONSTRAINT shows_roomid_fkey FOREIGN KEY (roomid) REFERENCES rooms (roomid) ON DELETE RESTRICT
);

CREATE TABLE tickets (
    ticketid integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    showid integer NOT NULL,
    rownum integer NOT NULL,
    seatnum integer NOT NULL,
    price numeric NOT NULL,
    CONSTRAINT "PK_tickets" PRIMARY KEY (ticketid),
    CONSTRAINT tickets_showid_fkey FOREIGN KEY (showid) REFERENCES shows (showid) ON DELETE RESTRICT
);

CREATE INDEX "IX_shows_movieid" ON shows (movieid);

CREATE INDEX "IX_shows_roomid" ON shows (roomid);

CREATE INDEX "IX_tickets_showid" ON tickets (showid);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210217195733_InitialCreate', '5.0.3');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210221193425_AddMovieRate', '5.0.3');

COMMIT;

START TRANSACTION;

ALTER TABLE movies ADD "LaunchDate" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210221223647_AddMovieLauchDate', '5.0.3');

COMMIT;
