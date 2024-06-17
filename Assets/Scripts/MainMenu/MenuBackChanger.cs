using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuBackChanger : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite playBackground;
    public Sprite optionsBackground;
    public Sprite defaultBackground;

    public Button playButton;
    public Button optionsButton;

    void Start()
    {
        // Adicionar listeners aos eventos do Event Trigger dos botões
        AddEventTrigger(playButton.gameObject, EventTriggerType.PointerEnter, () => ChangeBackground(playBackground));
        AddEventTrigger(optionsButton.gameObject, EventTriggerType.PointerEnter, () => ChangeBackground(optionsBackground));
    }

    void ChangeBackground(Sprite newBackground)
    {
        backgroundImage.sprite = newBackground;
    }

    void AddEventTrigger(GameObject target, EventTriggerType eventType, System.Action action)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = target.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }
}
