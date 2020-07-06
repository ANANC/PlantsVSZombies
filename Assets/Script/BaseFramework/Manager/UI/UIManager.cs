using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : IManager
{
    private string UIFolderPath = string.Empty;

    private GameObject m_Canvas;

    private Dictionary<string, BaseUIObject> m_UIObjectDict;
    private Dictionary<string, int> m_UINameDict;

    public void Init()
    {
        m_Canvas = GameObject.Instantiate(Resources.Load<GameObject>("UICanvas"));

        m_UIObjectDict = new Dictionary<string, BaseUIObject>();
        m_UINameDict = new Dictionary<string, int>();
    }
    public void UnInit()
    {
    }

    public void Start()
    {
    }

    public void Update()
    {
    }

    public void SetUIFolderPath(string path)
    {
        UIFolderPath = path;
    }

    private T _CreateUI<T>(string resourceName, string uiName,Transform parent) where T : BaseUIObject, new()
    {
        string path = string.Format("{0}/{1}", UIFolderPath, resourceName);

        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(path);
        if (gameObject == null)
        {
            return null;
        }

        Transform transform = gameObject.transform;
        transform.SetParent(parent);
        if (transform as RectTransform)
        {
            ((RectTransform)transform).sizeDelta = Vector3.zero;
        }
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        gameObject.name = resourceName;

        if (m_UINameDict.ContainsKey(resourceName))
        {
            m_UINameDict[resourceName] += 1;
            uiName += m_UINameDict[resourceName];
        }else
        {
            m_UINameDict.Add(resourceName, 0);
        }

        BaseUIObject ui = new T();
        ui.SetUIName(uiName, resourceName);
        ui.SetRoot(gameObject);

        m_UIObjectDict.Add(uiName, ui);

        return (T)ui;
    }

    public T OpenUI<T>(string uiName)where T: BaseUIObject, new()
    {
        return _OpenUI<T>(uiName, m_Canvas.transform);
    }

    private T _OpenUI<T>(string uiName, Transform parent) where T : BaseUIObject,new()
    {
        BaseUIObject ui;
        if (m_UIObjectDict.TryGetValue(uiName, out ui))
        {
            if (ui.IsOpen())
            {
                return (T)ui;
            }
        }
        else
        {
            ui = _CreateUI<T>(uiName, uiName,parent);
            if (ui != null)
            {
                ui.Init();
            }
        }

        ui.Start();
        ui.GameObject.SetActive(true);

        return (T)ui;
    }

    public T OpenSubUI<T>(string parentUIName, string uiName, Transform parent) where T : BaseUIObject, new()
    {
        BaseUIObject parentUI;
        if (!m_UIObjectDict.TryGetValue(parentUIName, out parentUI))
        {
            Debug.LogError(string.Format("【UI】打开子界面({0})失败，主界面({1})并未打开。", uiName, parentUIName));
            return null;
        }
        if (parent == null)
        {
            parent = parentUI.Transform;
        }

        T ui = _CreateUI<T>(uiName, "sub_" + uiName, parent);

        if (ui != null)
        {
            ui.Init();
            ui.Start();
            ui.GameObject.SetActive(true);
            ui.Transform.SetParent(parent);
            parentUI.AddSubUI(uiName);
        }

        return ui;
    }


    public void CloseUI(string uiName)
    {
        BaseUIObject ui;
        if (m_UIObjectDict.TryGetValue(uiName, out ui))
        {
            List<string> childs = ui.GetSubUI();
            if (childs != null)
            {
                for (int index = 0; index < childs.Count; index++)
                {
                    CloseUI(childs[index]);
                }
            }

            _CloseUI(ui);
        }
        else
        {
            return;
        }
    }

    private void _CloseUI(BaseUIObject ui)
    {
        if (!ui.IsOpen())
        {
            return;
        }

        ui.Close();
        ui.GameObject.SetActive(false);

    }

    public void DestroyUI(string uiName)
    {
        BaseUIObject ui;
        if (m_UIObjectDict.TryGetValue(uiName, out ui))
        {
            List<string> childs = ui.GetSubUI();
            if (childs != null)
            {
                for (int index = 0; index < childs.Count; index++)
                {
                    DestroyUI(childs[index]);
                }
            }
            _DestroyUI(ui, uiName);
            m_UINameDict.Remove(uiName);
        }
        else
        {
            return;
        }
    }

    private void _DestroyUI(BaseUIObject ui, string uiName)
    {
        if (ui.IsOpen())
        {
            ui.Close();
        }
        ui.Destroy();
        GameObject.Destroy(ui.GameObject);
        m_UIObjectDict.Remove(uiName);
    }

    public BaseUIObject GetUI(string uiName)
    {
        BaseUIObject ui;
        if (!m_UIObjectDict.TryGetValue(uiName, out ui))
        {
            Debug.LogError("【UI】获取UI（{0}）失败，当前并没有打开该界面");
        }
        return ui;
    }

 
}

