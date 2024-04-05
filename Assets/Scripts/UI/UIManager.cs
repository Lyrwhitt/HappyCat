using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    [HideInInspector]
    public Canvas canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        if (canvas == null)
        {
            Canvas obj = Resources.Load(string.Concat("UI/", "Canvas"), typeof(Canvas)) as Canvas;
            if (obj == null)
            {
                return;
            }
            else
                canvas = Instantiate(obj, this.transform, false);
        }
    }

    private List<GameObject> popups = new List<GameObject>();

    public GameObject ShowUI(string popupname)
    {
        GameObject obj = Resources.Load("UI/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            return null;
        }
        return ShowUIWithPrefab(obj, popupname);
    }

    public GameObject ShowUIWithPrefab(GameObject prefab, string popupname)
    {
        GameObject obj = Instantiate(prefab);
        obj.name = popupname;

        return ShowUI(obj);
    }

    private GameObject ShowUI(GameObject popup)
    {
        popups.Add(popup);

        popup.SetActive(true);
        popup.transform.SetParent(canvas.transform, false);

        return popup;
    }

    public void ClosAllUI()
    {
        foreach (GameObject popup in popups)
        {
            Destroy(popup.gameObject);
        }

        popups.Clear();
    }

    public void CloseUI()
    {
        if(popups.Count > 0)
        {
            Destroy(popups[popups.Count - 1].gameObject);
            popups.RemoveAt(popups.Count - 1);
        }
    }

    public GameObject GetUI()
    {
        if (popups.Count == 0)
        {
            return null;
        }
        else
            return popups[popups.Count - 1];
    }

    public bool IsOpenedUI(string popupname)
    {
        if (popups.Find(x => x.gameObject.name == popupname) != null)
        {
            return true;
        }
        else
            return false;
    }

    public int GetActivePopupCount()
    {
        return popups.Count;
    }
}