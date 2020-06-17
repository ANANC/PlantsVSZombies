using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject go, PointerEventData eventData);

    private VoidDelegate m_OnClick = null;
    public VoidDelegate onClick { set { m_OnClick = value; } }

    private VoidDelegate m_OnDown = null;
    public VoidDelegate onDown { set { m_OnDown = value; } }

    private VoidDelegate m_OnEnter = null;
    public VoidDelegate onEnter { set { m_OnEnter = value; } }

    private VoidDelegate m_OnExit = null;
    public VoidDelegate onExit { set { m_OnExit = value; } }

    private VoidDelegate m_OnUp = null;
    public VoidDelegate onUp { set { m_OnUp = value; } }

    private VoidDelegate m_OnSelect = null;
    public VoidDelegate onSelect { set { m_OnSelect = value; } }

    private VoidDelegate m_OnUpdateSelect = null;
    public VoidDelegate onUpdateSelect { set { m_OnUpdateSelect = value; } }

    private VoidDelegate m_OnBeginDrag = null;
    public VoidDelegate onBeginDrag { set { m_OnBeginDrag = value; } }

    private VoidDelegate m_OnDrag = null;
    public VoidDelegate onDrag { set { m_OnDrag = value; } }

    private VoidDelegate m_OnEndDrag = null;
    public VoidDelegate onEndDrag { set { m_OnEndDrag = value; } }

    private VoidDelegate m_OnDrop = null;
    public VoidDelegate onDrop { set { m_OnDrop = value; } }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (m_OnClick != null)
        {
            m_OnClick(gameObject, eventData);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (m_OnDown != null)
        {
            m_OnDown(gameObject, eventData);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (m_OnEnter != null)
        {
            m_OnEnter(gameObject, eventData);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (m_OnExit != null)
        {
            m_OnExit(gameObject, eventData);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (m_OnUp != null)
        {
            m_OnUp(gameObject, eventData);
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (m_OnBeginDrag != null)
        {
            m_OnBeginDrag(gameObject, eventData);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (m_OnDrag != null)
        {
            m_OnDrag(gameObject, eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (m_OnEndDrag != null)
        {
            m_OnEndDrag(gameObject, eventData);
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (m_OnDrop != null)
        {
            m_OnDrop(gameObject, eventData);
        }
    }
}
