CREATE TABLE kullanicilar (
    id SERIAL PRIMARY KEY,
    kullaniciadi VARCHAR(50) NOT NULL,
    sifre VARCHAR(50) NOT NULL
);

INSERT INTO kullanicilar (kullaniciadi, sifre) VALUES ('admin', '1234');
INSERT INTO kullanicilar (kullaniciadi, sifre) VALUES ('fey', '2323');
INSERT INTO kullanicilar (kullaniciadi, sifre) VALUES ('irem', '3434');
INSERT INTO kullanicilar (kullaniciadi, sifre) VALUES ('sd', '1212');