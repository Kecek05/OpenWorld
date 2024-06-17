using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsSoundsUI : MonoBehaviour
{
    public AudioClip hoverSound;
    public AudioClip pressSound;
    public AudioSource audioSource;

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AddEventTrigger(playButton.gameObject, EventTriggerType.PointerEnter, () => PlayTriggerSound(hoverSound));
        AddEventTrigger(optionsButton.gameObject, EventTriggerType.PointerEnter, () => PlayTriggerSound(hoverSound));
        AddEventTrigger(quitButton.gameObject, EventTriggerType.PointerEnter, () => PlayTriggerSound(hoverSound));

        AddEventTrigger(playButton.gameObject, EventTriggerType.PointerDown, () => PlaySelectedSound(pressSound));
        AddEventTrigger(optionsButton.gameObject, EventTriggerType.PointerDown, () => PlaySelectedSound(pressSound));
        AddEventTrigger(quitButton.gameObject, EventTriggerType.PointerDown, () => PlaySelectedSound(pressSound));
    }

    void PlayTriggerSound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }

    void PlaySelectedSound(AudioClip pressSound)
    {
        audioSource.clip = pressSound;
        audioSource.Play();
    }


    void AddEventTrigger(GameObject target, EventTriggerType eventType, System.Action action)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = target.AddComponent<EventTrigger>();
            audioSource.Stop();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }

}
