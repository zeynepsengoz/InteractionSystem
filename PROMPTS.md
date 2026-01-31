# LLM Kullanım Dokümantasyonu

## Özet
- Toplam prompt sayısı: 1
- Kullanılan araçlar: ChatGPT
- En çok yardım alınan konular: Etkileşim sistemi, kod sadeleştirme

---

## Prompt 1: Key Interactable Oluşturma

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-31 18:30

**Prompt:**
Unity Developer Intern case'i için bir World Interaction System geliştiriyorum. Aşağıdaki standartlara birebir uymam gerekiyor: - C# Coding Conventions (m_, s_, k_ prefix, region sıralaması, XML documentation) - Naming Convention (P_, M_, SO_, UI prefab isimleri) - Prefab Asset kuralları (pivot, collider, hierarchy) Senden şunları istiyorum: - Interaction system için yüksek seviyeli mimari tasarım - Hangi class lar olmalı ve sorumlulukları ne olmalı - Dosya ve namespace yapısı - Trade-off’lar (raycast vs trigger, Update vs Coroutine) Kod yazma sadece mimari anlat.

**Alınan Cevap (Özet):**
>Hedef: World Interaction System (WIS) – Yüksek seviye mimari

Bu sistemi 3 katman olarak düşün:

Domain (Saf mantık)

“Şu obje etkileşebilir mi?”, “Hangi prompt gösterilmeli?”, “Etkileşim başladı mı/bitti mi?” gibi kurallar.

Unity bağımlılığı minimum.

Unity Adapter / Runtime (Algılama + bağlama)

Raycast/Trigger ile “hangi interactable yakın/önümde?” bilgisini üretir.

Domain’e “aday” gönderir ve sonucu uygular (highlight, UI prompt, invoke).

Presentation (UI + Audio/FX + Feedback)

UI prefabları (UI_*) ve prompt görselleştirme.

Audio/FX gibi geri bildirimler.

Bu ayrım, case standartlarına uygun “sorumlulukları net” class tasarlamayı kolaylaştırır.

Class’lar ve sorumlulukları

Aşağıdaki liste önerilen minimum set (case gereksinimlerine göre genişletilebilir). “Kod yok”, sadece rol dağılımı:

1) Player tarafı (Interactor)

PlayerInteractor

Oyuncunun “etkileşim isteğini” yönetir (input geldi → etkileşime çalış).

Seçili hedefi bilir (current candidate).

Domain’e “TryInteract” çağrısı yaptırır.

Sahne objesi: P_PlayerInteractor gibi (P_ prefix kuralına uygun).

InteractionInputRouter (opsiyonel ama temiz)

Yeni Input System/legacy farkını soyutlar: “InteractPressed”, “InteractReleased”.

PlayerInteractor’a event verir.

2) Algılama (Detection)

Bu katman “yakındaki/önündeki en iyi hedef”i üretir.

IInteractionDetector (interface)

“Bu frame’de hangi interactable hedef?” sorusunun cevabını üretir.

RaycastInteractionDetector

Kamera/aim yönüne ray atar, filtreler, en uygun hedefi döner.

LayerMask, distance, query trigger interaction gibi ayarlar burada.

TriggerInteractionDetector

Oyuncu çevresindeki trigger collider’lardan aday listesi tutar.

Önceliklendirme (en yakın, en önde, angle) yapar.

Case trade-off bölümünde raycast/trigger kıyasını ayrıca verdim.

InteractionTargetResolver

Detector’dan gelen “adaylar” arasından tek hedef seçer:

distance, angle, line-of-sight, priority value

Böylece UI ve etkileşim “zıplamaz”, stabil olur.

3) Interactable tarafı (Target)

IInteractable (interface)

“Etkileşebilir mi?” “Prompt text nedir?” “Execute interact” gibi contract.

Unity-specific değilse daha iyi (ama MonoBehaviour adaptörü olacak).

InteractableBehaviour (MonoBehaviour base)

Sahnedeki obje bununla “IInteractable” contract’ini sağlar.

Collider / pivot / highlight referanslarını tutar.

Prefab: P_Interactable_* veya domain’e göre P_Door_*, P_Item_*.

InteractionConditionSet

Kilitli mi? Envanter item gerekli mi? Cooldown var mı?

Condition’lar burada “kompozisyon” olur (tek dev if yerine).

InteractionActionSet

Etkileşince ne olacak? Kapı aç, item ver, UI aç, timeline tetikle…

Action’ları da kompozisyon olarak tut (tek sınıfa yığma).

InteractionPriorityProvider

Aynı anda birden fazla hedef varsa öncelik skoru sağlar (örn. “ana görev objesi daha öncelikli”).

4) Yönetici / Orkestrasyon (Coordinator)

InteractionController

“Detection → resolve → focus enter/exit → UI update → interact call” akışını yönetir.

PlayerInteractor’ın “merkezi beyni” gibi düşünebilirsin.

Input geldiğinde “current target” varsa etkileşimi başlatır.

Focus değişince “OnFocusEnter/Exit” (highlight vs) tetikler.

InteractionStateMachine (opsiyonel ama sağlam)

Idle / Focusing / Interacting / Blocked / Cooldown gibi state’ler.

UI, input, anim ile çakışmayı engeller.

5) UI / Feedback (Presentation)

InteractionPromptPresenter

Domain’den gelen “prompt model”i UI’ya çevirir.

UI prefab UI_InteractionPrompt gibi isimlendirilir.

InteractionHighlightPresenter

Outline/Material swap/Shader param gibi highlight işlerini yapar.

InteractableBehaviour ile konuşur (renderer refs vb).

InteractionFeedbackBus (opsiyonel)

Ses, haptic, VFX event’leri için tek merkez (istersen ScriptableObject event channel tarzı).

6) Data / Config

SO_InteractionSettings

Mesafe, layer mask, angle threshold, input hold time, vb.

SO_ prefix kuralına uygun.

M_InteractionPromptStyle (Material örneği)

M_ prefix kuralına uygun.

**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [x] Adapte ettim
- [] Reddettim

**Açıklama:** 


## Prompt 2: [Konu Başlığı]

**Araç:** ChatGPT
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
>Bu mimariyi 12 saatlik Unity Intern Case kapsamında MVP seviyesinde sadeleştirerek uygulayacağım. Öncelikle IInteractable sözleşmesini ve temel InteractableBase yapısını tanımlayıp, raycast tabanlı bir InteractionDetector ile devam etmeyi planlıyorum.

**Alınan Cevap (Özet):**
> MVP için planın doğru. 12 saatlik case’de “az sınıf + net sorumluluk + standartlara uyum” en yüksek puanı getirir. Aşağıdaki sade mimariyle ilerle:

MVP Mimari (raycast tabanlı)
Çekirdek akış

PlayerInteractor (Update)
→ RaycastInteractionDetector (Detect)
→ “currentTarget değişti mi?”
→ UI Prompt + Highlight güncelle
→ Input gelirse currentTarget.Interact()

Bu MVP’de “Resolver / StateMachine / ConditionSet” yok; hepsi opsiyonel.

MVP Class Listesi ve sorumluluklar
1) Sözleşme + Base

IInteractable

Player’ın ihtiyaç duyduğu minimum yüzey:

“Etkileşebilir mi?”

“Prompt ne?”

“Etkileşimi çalıştır”

“Focus enter/exit” (highlight/prompt tetiklemek için)

InteractableBase (MonoBehaviour)

IInteractable’ın varsayılan implementasyonu.

Unity bağımlı kısımlar burada:

Prompt anchor (transform)

Highlight hedefi (renderer / outline component ref)

Varsayılan “CanInteract” mantığı (mesafe/aktiflik)

İntern case için: Her interactable türü (Door, Pickup) InteractableBase’den türeyebilir.

2) Detection

RaycastInteractionDetector

Kamera forward yönüne raycast atar.

LayerMask + maxDistance ile filtreler.

Hit’ten IInteractable bulur (hit collider üzerinde veya parent’ta).

MVP’de tek raycast yeterli. Ekstra “line of sight” vs yok.

3) Orkestrasyon

InteractionController (veya PlayerInteractor)

Her frame detector’dan hedef alır.

Target değişince:

eski hedefe OnFocusExit

yeni hedefe OnFocusEnter

UI Prompt’u günceller.

Input geldiğinde Interact() çağırır (CanInteract true ise).

İstersen PlayerInteractor + InteractionController’ı tek sınıfta birleştir (MVP sade kalır).

4) Presentation (MVP)

InteractionPromptPresenter

UI_InteractionPrompt prefabını kontrol eder:

show/hide

text set

InteractionHighlighter (opsiyonel)

“Focus” stateinde highlight aç/kapat.

MVP’de InteractableBase içine de gömülebilir ama ayrı sınıf daha temiz.
Assets/_Project/WorldInteraction/Runtime/
  Core/
    IInteractable.cs
    InteractableBase.cs
  Detection/
    RaycastInteractionDetector.cs
  Player/
    PlayerInteractor.cs   (controller burada)
  UI/
    UI_InteractionPrompt.prefab
    InteractionPromptPresenter.cs
  Data/
    SO_InteractionSettings.asset
    SO_InteractionSettings.cs


**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**

## Prompt 3: [Konu Başlığı]

**Araç:** ChatGPT-4 
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
> şimdi Core’u kodlayalım.

Benim Unity proje yapım şu şekilde:
Assets/InteractionSystem/Scripts/Runtime/Core/
namespace: InteractionSystem.Scripts.Runtime.Core

Ludu Arts standartları:
- private field: m_
- private const: k_
- region sırası: Fields -> Properties -> Unity Methods -> Methods -> Interface Implementations
- public üyelerde kısa XML documentation
- silent bypass yok (null/disabled durumlarında en azından Debug.LogWarning/Error)

İstediğim çıktı:
1) IInteractable.cs (MVP contract)
- string Prompt { get; }
- Transform PromptAnchor { get; }
- bool CanInteract(GameObject interactor)
- void OnFocusEnter()
- void OnFocusExit()
- void Interact(GameObject interactor)

2) InteractableBase.cs (abstract MonoBehaviour, IInteractable implement)
Serialize alanlar:
- [SerializeField] string m_Prompt = "Interact";
- [SerializeField] Transform m_PromptAnchor;
- [SerializeField] float m_MaxInteractDistance (default 2f)
- [SerializeField] bool m_IsEnabled = true;
- [SerializeField] GameObject m_HighlightTarget (opsiyonel)
Kurallar:
- PromptAnchor null ise transform fallback
- CanInteract: enabled + interactor null check + distance check
- OnFocusEnter/Exit: highlight aç/kapat (highlight null ise sadece return)
- Interact abstract (türeyen class override edecek)

Bu iki dosyayı ayrı ayrı  direkt kopyalayıp Unity’ye yapıştırabileceğim şekilde ver. 

**Alınan Cevap (Özet):**
> // Assets/InteractionSystem/Scripts/Runtime/Core/IInteractable.cs
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Core
{
    /// <summary>
    /// Defines the minimum contract for an interactable world object.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Gets the prompt text to display to the player.
        /// </summary>
        string Prompt { get; }

        /// <summary>
        /// Gets the world-space anchor used to position the interaction prompt.
        /// </summary>
        Transform PromptAnchor { get; }

        /// <summary>
        /// Returns whether the interactor can interact with this target at this time.
        /// </summary>
        /// <param name="interactor">The GameObject attempting the interaction.</param>
        /// <returns>True if interaction is allowed; otherwise false.</returns>
        bool CanInteract(GameObject interactor);

        /// <summary>
        /// Called when this interactable becomes the current focused target.
        /// </summary>
        void OnFocusEnter();

        /// <summary>
        /// Called when this interactable is no longer the current focused target.
        /// </summary>
        void OnFocusExit();

        /// <summary>
        /// Executes the interaction logic.
        /// </summary>
        /// <param name="interactor">The GameObject performing the interaction.</param>
        void Interact(GameObject interactor);
    }
}
// Assets/InteractionSystem/Scripts/Runtime/Core/InteractableBase.cs
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Core
{
    /// <summary>
    /// Base class for interactables, providing common prompt, range, enable/disable,
    /// and focus highlight behavior.
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Fields

        [SerializeField] private string m_Prompt = "Interact";
        [SerializeField] private Transform m_PromptAnchor;
        [SerializeField] private float m_MaxInteractDistance = 2f;
        [SerializeField] private bool m_IsEnabled = true;
        [SerializeField] private GameObject m_HighlightTarget;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the prompt text to display to the player.
        /// </summary>
        public virtual string Prompt
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_Prompt))
                {
                    Debug.LogWarning($"{nameof(InteractableBase)}: Prompt is empty on '{name}'. Using fallback.", this);
                    return "Interact";
                }

                return m_Prompt;
            }
        }

        /// <summary>
        /// Gets the world-space anchor used to position the interaction prompt.
        /// Falls back to this transform if no anchor is assigned.
        /// </summary>
        public virtual Transform PromptAnchor
        {
            get
            {
                if (m_PromptAnchor == null)
                {
                    Debug.LogWarning($"{nameof(InteractableBase)}: PromptAnchor is null on '{name}'. Falling back to transform.", this);
                    return transform;
                }

                return m_PromptAnchor;
            }
        }

        #endregion

        #region Unity Methods

        protected virtual void OnValidate()
        {
            if (m_MaxInteractDistance < 0f)
            {
                Debug.LogWarning($"{nameof(InteractableBase)}: MaxInteractDistance was negative on '{name}'. Clamping to 0.", this);
                m_MaxInteractDistance = 0f;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns whether the interactor can interact with this target at this time.
        /// </summary>
        /// <param name="interactor">The GameObject attempting the interaction.</param>
        /// <returns>True if interaction is allowed; otherwise false.</returns>
        public virtual bool CanInteract(GameObject interactor)
        {
            if (!enabled)
            {
                Debug.LogWarning($"{nameof(InteractableBase)}: Component is disabled on '{name}'.", this);
                return false;
            }

            if (!m_IsEnabled)
            {
                Debug.LogWarning($"{nameof(InteractableBase)}: Interactable is disabled (m_IsEnabled=false) on '{name}'.", this);
                return false;
            }

            if (interactor == null)
            {
                Debug.LogError($"{nameof(InteractableBase)}: Interactor is null while checking CanInteract on '{name}'.", this);
                return false;
            }

            float distance = Vector3.Distance(interactor.transform.position, transform.position);
            if (distance > m_MaxInteractDistance)
            {
                Debug.LogWarning(
                    $"{nameof(InteractableBase)}: Interactor '{interactor.name}' is out of range for '{name}'. " +
                    $"Distance={distance:0.00}, Max={m_MaxInteractDistance:0.00}.",
                    this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when this interactable becomes the current focused target.
        /// </summary>
        public virtual void OnFocusEnter()
        {
            if (m_HighlightTarget == null)
            {
                return;
            }

            m_HighlightTarget.SetActive(true);
        }

        /// <summary>
        /// Called when this interactable is no longer the current focused target.
        /// </summary>
        public virtual void OnFocusExit()
        {
            if (m_HighlightTarget == null)
            {
                return;
            }

            m_HighlightTarget.SetActive(false);
        }

        #endregion

        #region Interface Implementations

        /// <summary>
        /// Executes the interaction logic.
        /// </summary>
        /// <param name="interactor">The GameObject performing the interaction.</param>
        public abstract void Interact(GameObject interactor);

        #endregion
    }
}


**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [] Adapte ettim
- [x ] Reddettim

**Açıklama:**
> Karmaşık geldi.
> 
## Prompt 4: 

**Araç:** ChatGPT-4 
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
> Kod çalışıyor  bana biraz karmaşık geldi. 

Core kısmını daha basit ve okunur yapmak istiyorum.

O yüzden çok fazla Debug.Log olmasın , kod mümkün olduğunca kısa ve net olsun ve MVP için şart olmayan kontroller çıkarılabilir ama şunlar mutlaka kalsın: 
- m_ prefix ve genel isimlendirme
- XML yorumlar
- PromptAnchor null ise transform kullanılması
- enabled + mesafe kontrolü
 amacım ilk bakışta anlaşılan sade core kodu buna göre sadeleştirir misin



**Alınan Cevap (Özet):**
>// Assets/InteractionSystem/Scripts/Runtime/Core/InteractableBase.cs
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Core
{
    /// <summary>
    /// Simple MVP base class for interactables: prompt, anchor fallback, range check, and optional highlight.
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Fields

        [SerializeField] private string m_Prompt = "Interact";
        [SerializeField] private Transform m_PromptAnchor;
        [SerializeField] private float m_MaxInteractDistance = 2f;
        [SerializeField] private GameObject m_HighlightTarget;

        #endregion

        #region Properties

        /// <summary>
        /// Prompt text shown to the player.
        /// </summary>
        public virtual string Prompt => m_Prompt;

        /// <summary>
        /// Anchor for prompt positioning. Falls back to this transform if not assigned.
        /// </summary>
        public virtual Transform PromptAnchor => m_PromptAnchor != null ? m_PromptAnchor : transform;

        #endregion

        #region Unity Methods
        #endregion

        #region Methods

        /// <summary>
        /// Checks if interaction is allowed (enabled + distance check).
        /// </summary>
        /// <param name="interactor">The GameObject attempting the interaction.</param>
        public virtual bool CanInteract(GameObject interactor)
        {
            if (!enabled || interactor == null)
            {
                return false;
            }

            float distance = Vector3.Distance(interactor.transform.position, transform.position);
            return distance <= m_MaxInteractDistance;
        }

        /// <summary>
        /// Called when this interactable becomes the focused target.
        /// </summary>
        public virtual void OnFocusEnter()
        {
            if (m_HighlightTarget != null)
            {
                m_HighlightTarget.SetActive(true);
            }
        }

        /// <summary>
        /// Called when this interactable is no longer the focused target.
        /// </summary>
        public virtual void OnFocusExit()
        {
            if (m_HighlightTarget != null)
            {
                m_HighlightTarget.SetActive(false);
            }
        }

        #endregion

        #region Interface Implementations

        /// <summary>
        /// Executes the interaction logic.
        /// </summary>
        /// <param name="interactor">The GameObject performing the interaction.</param>
        public abstract void Interact(GameObject interactor);

        #endregion
    }
}


**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [] Adapte ettim
- [x ] Reddettim

**Açıklama:**
> Daha basit, anlaşılır kod istediğim için.
## Prompt 5: 

**Araç:** ChatGPT-4 
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
> 

Raycast ile oyuncunun baktığı yönde interactable var mı diye kontrol eden
çok sade bir script istiyorum.

Şunlara dikkat et:
- Kod kısa ve okunur olsun
- Öğrenci seviyesinde yazılsın
- Sadece raycast atsın ve IInteractable döndürsün
- MonoBehaviour olsun
- Namespace kullansın (InteractionSystem.Scripts.Runtime.Detection)
- Kamera, mesafe ve layer SerializeField olsun
- Extra sistem, state, resolver vs. OLMASIN



RaycastInteractionDetector.cs dosyasını yaz.


**Alınan Cevap (Özet):**
>// Assets/InteractionSystem/Scripts/Runtime/Detection/RaycastInteractionDetector.cs
using UnityEngine;
using InteractionSystem.Scripts.Runtime.Core;

namespace InteractionSystem.Scripts.Runtime.Detection
{
    /// <summary>
    /// Very simple raycast detector that checks the camera forward direction
    /// and returns an IInteractable if found.
    /// </summary>
    public class RaycastInteractionDetector : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera m_Camera;
        [SerializeField] private float m_MaxDistance = 3f;
        [SerializeField] private LayerMask m_LayerMask = ~0;

        #endregion

        #region Methods

        /// <summary>
        /// Casts a ray from the camera forward and returns the hit interactable (if any).
        /// </summary>
        public IInteractable Detect()
        {
            if (m_Camera == null)
            {
                Debug.LogWarning($"{nameof(RaycastInteractionDetector)}: Camera is not assigned.", this);
                return null;
            }

            Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, m_MaxDistance, m_LayerMask))
            {
                return hit.collider.GetComponentInParent<IInteractable>();
            }

            return null;
        }

        #endregion
    }
}

**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [] Adapte ettim
- [ x] Reddettim

**Açıklama:**
> Ayrıntılı olduğu için.
> 
## Prompt 6: 

**Araç:** ChatGPT-4 
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
> Detaylı geldi o yüzden Unity’de öğrenci seviyesinde çok sade bir RaycastInteractionDetector yazmak istiyorum. Şartlar: - Debug.Log, ekstra kontrol, karmaşık yapı OLMASIN - Kod kısa ve ilk bakışta anlaşılır olsun Sadece RaycastInteractionDetector.cs dosyasını yaz.

**Alınan Cevap (Özet):**
> // Assets/InteractionSystem/Scripts/Runtime/Detection/RaycastInteractionDetector.cs
using UnityEngine;
using InteractionSystem.Scripts.Runtime.Core;

namespace InteractionSystem.Scripts.Runtime.Detection
{
    /// <summary>
    /// Simple raycast-based interaction detector.
    /// </summary>
    public class RaycastInteractionDetector : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private float m_MaxDistance = 3f;
        [SerializeField] private LayerMask m_LayerMask;

        /// <summary>
        /// Returns the interactable the camera is looking at, or null.
        /// </summary>
        public IInteractable Detect()
        {
            Ray ray = new Ray(
                m_Camera.transform.position,
                m_Camera.transform.forward
            );

            if (Physics.Raycast(ray, out RaycastHit hit, m_MaxDistance, m_LayerMask))
            {
                return hit.collider.GetComponentInParent<IInteractable>();
            }

            return null;
        }
    }
}


**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
>
> 
## Prompt 7: 

**Araç:** ChatGPT-4 
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**
> Chat  başta etkileşim sistemini raycast tabanlı olarak kurmuştum. 
Oyuncunun kamera baktığı yönde interactable objeyi tespit eden bir yapı vardı.

Geliştirme sürecinde bu yapının case kapsamı için gereğinden fazla karmaşık
olduğunu ve yakın mesafede stabil çalışmadığını fark ettim.

Bu yüzden etkileşim sistemini refactor ederek raycast yerine
yakınlık (proximity) tabanlı bir yaklaşıma geçmeye karar verdim. Sana şimdi düzelttiğim kodları göndereceğim 



**Alınan Cevap (Özet):**
> Kodları aldım — proximity yaklaşımı MVP için doğru yön, ama şu anki hali case standartlarına ve stabil çalışmaya göre birkaç kritik sorun içeriyor. Aşağıda hem sorunları net söylüyorum hem de aynı sistemi “sade + stabil + standart uyumlu” hale getiren örnek MVP kodlarını veriyorum (direkt kopyala-yapıştır).

Şu anki kodda kritik problemler

Physics.OverlapSphere(...)[0] patlar
Hiç collider yoksa array boş gelir → IndexOutOfRangeException.

Hedef seçimi stabil değil
[0] rastgele/physics sırasına göre gelebilir. Yakında “stabil değil” hissetmen normal.

Standartlara uymuyor

m_ prefix yok

region sırası yok

XML doc yok

namespace yok (PlayerInteractor, DoorInteractable, SimplePlayerMove)

DoorInteractable mantığı hatalı
isOpen = true ile başlıyor, if (isOpen) return; → kapı zaten açık olduğundan hiç açılmıyor/toggle yok.
// Assets/InteractionSystem/Scripts/Runtime/Core/IInteractable.cs
namespace InteractionSystem.Scripts.Runtime.Core
{
    /// <summary>
    /// Minimal proximity-based interactable contract.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Executes the interaction logic.
        /// </summary>
        void InteractLogic();
    }
}


**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Bana anlaşılır geldi ve adapte ettim.
>
> ## Prompt 8: 

**Araç:** ChatGPT-4 
**Tarih/Saat:** YYYY-MM-DD HH:MM

**Prompt:**Unity'de 3D Objelerin rengini nasıl değiştirebilirim
> 

**Alınan Cevap (Özet):**
> using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
}


**Nasıl Kullandım:**
- [ x] Direkt kullandım
- [] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> 








---
