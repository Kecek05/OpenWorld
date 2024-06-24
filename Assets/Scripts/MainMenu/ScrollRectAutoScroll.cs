using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScrollRectAutoScroll : MonoBehaviour
{
    public float scrollSpeed = 10f;
    private bool mouseOver = false;

    private List<Selectable> m_Selectables = new List<Selectable>();
    private ScrollRect m_ScrollRect;

    private Vector2 m_NextScrollPosition = Vector2.up;

    void OnEnable()
    {
        if (m_ScrollRect)
        {
            m_ScrollRect.content.GetComponentsInChildren(m_Selectables);
        }
    }
    void Awake()
    {
        m_ScrollRect = GetComponent<ScrollRect>();
        //remove this line if not using Rewired
    }
    void Start()
    {
        if (m_ScrollRect)
        {
            m_ScrollRect.content.GetComponentsInChildren(m_Selectables);
        }
        ScrollToSelected(true);
    }
    void Update()
    {
        if (Gamepad.current != null) // only work with gamepad on
        {
                // Scroll via input.
                InputScroll();
            if (!mouseOver)
            {
                // Lerp scrolling code.
                m_ScrollRect.normalizedPosition = Vector2.Lerp(m_ScrollRect.normalizedPosition, m_NextScrollPosition, scrollSpeed * Time.unscaledDeltaTime);
            }
            else
            {
                m_NextScrollPosition = m_ScrollRect.normalizedPosition;
            }

        }
    }
    void InputScroll()
    {
        if (m_Selectables.Count > 0)
        {
            //remove the rePlayer getaxis calls is you aren't using Rewired
            //if it still doesn't work, check your input manager settings's axes and make sure they are defined properly
            //if you're using the new input system, this is also probably where you should replace the calls to the old one
            if (
                 UnityEngine.Input.GetAxis("Vertical") != 0.0f || UnityEngine.Input.GetAxis("Horizontal") != 0.0f || UnityEngine.Input.GetButtonDown("Horizontal")
                || UnityEngine.Input.GetButtonDown("Vertical") || UnityEngine.Input.GetButton("Horizontal") || UnityEngine.Input.GetButton("Vertical"))
            {
                ScrollToSelected(false);
            }
        }
    }
    void ScrollToSelected(bool quickScroll)
    {


            int selectedIndex = -1;
            Selectable selectedElement = EventSystem.current.currentSelectedGameObject ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null;

            if (selectedElement)
            {
                selectedIndex = m_Selectables.IndexOf(selectedElement);
            }
            if (selectedIndex > -1)
            {
                if (quickScroll)
                {
                    m_ScrollRect.normalizedPosition = new Vector2(0, 1 - (selectedIndex / ((float)m_Selectables.Count - 1)));
                    m_NextScrollPosition = m_ScrollRect.normalizedPosition;
                }
                else
                {
                    m_NextScrollPosition = new Vector2(0, 1 - (selectedIndex / ((float)m_Selectables.Count - 1)));
                }
            }
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        ScrollToSelected(false);
    }
}
