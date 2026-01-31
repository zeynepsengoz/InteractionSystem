# Interaction System (Unity)

Bu proje, Unity kullanılarak geliştirilmiş **temel bir etkileşim (interaction) sistemi** örneğidir.  
Oyuncunun sahnedeki farklı nesnelerle **tek bir sistem üzerinden** etkileşime girmesini sağlar.

Sistem; **basit, modüler ve genişletilebilir** olacak şekilde tasarlanmıştır.

---

## 🎮 Proje Özellikleri

- Oyuncu hareketi (WASD)
- `IInteractable` arayüzü (interface) ile etkileşim sistemi
- Yakınlık tabanlı etkileşim algılama
- Farklı etkileşilebilir nesne örnekleri:
  - Kapı (Door)
  - Sandık (Chest – anahtar & süre kontrolü)
  - Işık (Light – aç/kapat)
- Renk değişimi ile görsel geri bildirim
- Debug logları ile takip edilebilir etkileşimler

---

## 🧩 Etkileşim Mimarisi

Projede **interface tabanlı bir mimari** kullanılmıştır.

### `IInteractable`

Etkileşime girebilen tüm nesneler bu arayüzü uygular.

```csharp
public interface IInteractable
{
    void InteractLogic();
}
Bu yapı sayesinde oyuncu, nesnenin türünü bilmeden
(kapı mı, sandık mı, ışık mı) etkileşim kurabilir

🧍 Oyuncu Sistemi
PlayerInteractor.cs

Oyuncunun etrafındaki nesneleri Physics.OverlapSphere ile algılar

Etkileşim tuşuna (E) basıldığında çalışır

Bulunan nesnede InteractLogic() metodunu çağırır

Süre (timer) ve anahtar (keyValue) mantığı içerir

SimplePlayerMove.cs

Temel oyuncu hareketi sağlar

Kontroller:

W / A / S / D

Time.deltaTime kullanıldığı için frame bağımsızdır


🚪 Etkileşilebilir Nesneler
DoorInteractable

Kapıyı belirli bir açıyla döndürerek açar

Tek seferlik etkileşim

Konsola bilgi mesajı yazar

ChestInteractable

Sandığın açılabilmesi için:

Oyuncuda anahtar olmalı

Etkileşim tuşu 3 saniye basılı tutulmalı

Görsel durumlar:

Kırmızı → Kapalı

Yeşil → Açık

Şartlar sağlanmazsa süre sıfırlanır

LightInteractable

Açık / kapalı durumu arasında geçiş yapar

Renk değiştirir:

Siyah → Kapalı

Sarı → Açık

Mevcut durumu konsola yazar

🎯 Kontroller
İşlem	Tuş
Hareket	W A S D
Etkileşim	E

🛠️ Kullanılan Teknolojiler

Unity Engine

C#

MonoBehaviour

Interface (Arayüz) tabanlı programlama

Assets/
 └── InteractionSystem/
     └── Scripts/
         └── Runtime/
             ├── Core/
             │   └── IInteractable.cs
             ├── PlayerInteractor.cs
             ├── SimplePlayerMove.cs
             └── Interactables/
                 ├── DoorInteractable.cs
                 ├── ChestInteractable.cs
                 └── LightInteractable.cs
📌 Notlar

Bu proje bir case / öğrenme projesi olarak geliştirilmiştir

Etkileşim mantığını sade ve anlaşılır şekilde göstermeyi amaçlar
