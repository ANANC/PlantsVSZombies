using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIObject
{
    private string uiname;
    public string UIName { get { return uiname; } }

    private string resourceName;
    public string ResourceName { get { return resourceName; } }

    private GameObject gameObject;
    public GameObject GameObject { get { return gameObject; } }

    private Transform transform;
    public Transform Transform { get { return transform; } }

    private List<string> m_ChildUIName = null;

    public void SetUIName(string uiName, string resourceName)
    {
        this.uiname = uiName;
        this.resourceName = resourceName;
    }

    public void SetRoot(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    public void AddSubUI(string uiName)
    {
        if (m_ChildUIName == null)
        {
            m_ChildUIName = new List<string>(1);
        }
        m_ChildUIName.Add(uiName);
    }

    public List<string> GetSubUI()
    {
        return m_ChildUIName;
    }

    public virtual void Init()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void Close()
    {
    }

    public virtual void Destroy()
    {
    }

    public bool IsOpen()
    {
        return GameObject.activeSelf;
    }

    public Transform FindTransform(string path)
    {
        Transform findTransform = transform.Find(path);
        if (findTransform == null)
        {
            Debug.LogError($"UI寻找路径失败。UIName:{uiname} Path:{path}");
            return null;
        }
        return findTransform;
    }

    public GameObject FindGameObject(string path)
    {
        Transform findTransform = FindTransform(path);
        if(findTransform == null)
        {
            return null;
        }
        return findTransform.gameObject;
    }

    public T FindComponent<T>(string path)where T:Component
    {
        Transform findTransform = FindTransform(path);
        if (findTransform == null)
        {
            return null;
        }
        return findTransform.GetComponent<T>();
    }
}
