CREATE TABLE odunc_kitaplar (
    id SERIAL PRIMARY KEY,
    uye_adi VARCHAR(100),
    uye_soyadi VARCHAR(100),
    kitap_adi VARCHAR(100),
    verilis_tarihi DATE,
    teslim_tarihi DATE
);
INSERT INTO odunc_kitaplar (uye_adi, uye_soyadi, kitap_adi, verilis_tarihi, teslim_tarihi) VALUES
('Ali', 'Yılmaz', 'Hayvan Çiftliği', '2024-04-01', '2024-04-10'),
('Ayşe', 'Demir', 'Tutunamayanlar', '2024-04-15', '2024-04-30'), -- Gecikmiş!
('Mehmet', 'Kaya', 'Dune', CURRENT_DATE, CURRENT_DATE + INTERVAL '7 days');

SELECT * FROM odunc_kitaplar ORDER BY id;