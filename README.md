[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/b2uNUfo9)
### Sanal Evcil Hayvan Bakımı API'si

#### Proje Özeti
Bu proje, kullanıcıların sanal evcil hayvanlarını yönetebilmeleri için bir RESTful API geliştirmeyi hedefler. API, kullanıcıların sanal evcil hayvanlarını beslemelerine, oynatmalarına, eğitmelerine ve onların sağlık durumlarını izlemelerine olanak tanır. Proje, .NET 7 ve Entity Framework Core 7 kullanarak gerçekleştirilecektir.

#### Veritabanı Tasarımı
Veritabanında en az beş tablo bulunacak:
1. **Kullanıcılar**: Kullanıcı bilgilerini içerir.
2. **Evcil Hayvanlar**: Farklı türdeki sanal evcil hayvanların bilgilerini içerir.
3. **Sağlık Durumları**: Evcil hayvanların sağlık durumlarını kaydeder.
4. **Aktiviteler**: Evcil hayvanlarla yapılabilecek aktiviteleri içerir (örn. yürüyüş, oyun, eğitim).
5. **Besinler**: Evcil hayvanlara verilebilecek çeşitli besinlerin bilgilerini içerir.

#### API Fonksiyonları
1. **Kullanıcı İşlemleri**:
   - `POST /kullanicilar`: Yeni kullanıcı oluşturur.
   - `GET /kullanicilar/{kullaniciId}`: Belirli bir kullanıcının bilgilerini getirir.

2. **Evcil Hayvan İşlemleri**:
   - `POST /evcilHayvanlar`: Yeni evcil hayvan oluşturur.
   - `GET /evcilHayvanlar`: Tüm evcil hayvanları listeler.
   - `GET /evcilHayvanlar/{evcilHayvanId}`: Belirli bir evcil hayvanın bilgilerini getirir.
   - `PUT /evcilHayvanlar/{evcilHayvanId}`: Evcil hayvanın bilgilerini günceller.

3. **Sağlık Durumu İşlemleri**:
   - `GET /saglikDurumlari/{evcilHayvanId}`: Belirli bir evcil hayvanın sağlık durumunu getirir.
   - `PATCH /saglikDurumlari/{evcilHayvanId}`: Evcil hayvanın sağlık durumunu günceller.

4. **Aktivite İşlemleri**:
   - `POST /aktiviteler`: Evcil hayvan için yeni bir aktivite ekler.
   - `GET /aktiviteler/{evcilHayvanId}`: Evcil hayvanın yapabileceği aktiviteleri listeler.

5. **Besin İşlemleri**:
   - `GET /besinler`: Tüm besinleri listeler.
   - `POST /besinler/{evcilHayvanId}`: Evcil hayvana besin verir.
