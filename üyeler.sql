CREATE TABLE uyeler (
    id SERIAL PRIMARY KEY,
    ad VARCHAR(50) NOT NULL,
    soyad VARCHAR(50) NOT NULL,
    dogum_tarihi DATE,
    cinsiyet VARCHAR(10),
    telefon VARCHAR(20),
    email VARCHAR(100),
    adres TEXT
);
INSERT INTO uyeler (ad, soyad, dogum_tarihi, cinsiyet, telefon, email, adres)
VALUES 
('Ahmet', 'Yılmaz', '1995-06-15', 'ERKEK', '05001234567', 'ahmet@example.com', 'İstanbul, Türkiye'),
('Ayşe', 'Demir', '1992-11-22', 'KADIN', '05509876543', 'ayse@example.com', 'Ankara, Türkiye'),
('Mehmet', 'Çelik', '1990-03-10', 'ERKEK', '05321234567', 'mehmet@example.com', 'İzmir, Türkiye'),
('Fatma', 'Öztürk', '1988-09-30', 'KADIN', '05421234567', 'fatma@example.com', 'Bursa, Türkiye');

SELECT * FROM  uyeler 