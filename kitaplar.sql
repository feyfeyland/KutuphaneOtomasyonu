CREATE TABLE kitaplar (
    id SERIAL PRIMARY KEY,
    kitap_adi VARCHAR(100) NOT NULL,
    yazar_adi VARCHAR(100) NOT NULL,
    tur VARCHAR(50),
    sayfa_sayisi INTEGER,
    basim_yili INTEGER,
    yayin_evi VARCHAR(100),
    adet INTEGER
);
INSERT INTO kitaplar (kitap_adi, yazar_adi, tur, sayfa_sayisi, basim_yili, yayin_evi, adet) VALUES
('Suç ve Ceza', 'Fyodor Dostoyevski', 'Roman', 687, 2020, 'İş Bankası Kültür Yayınları', 3),
('Kürk Mantolu Madonna', 'Sabahattin Ali', 'Roman', 160, 2018, 'Yapı Kredi Yayınları', 5),
('Dune', 'Frank Herbert', 'Bilim Kurgu', 688, 2021, 'İthaki Yayınları', 2),
('Hayvan Çiftliği', 'George Orwell', 'Alegori', 152, 2019, 'Can Yayınları', 4),
('Tutunamayanlar', 'Oğuz Atay', 'Roman', 724, 2017, 'İletişim Yayınları', 1);