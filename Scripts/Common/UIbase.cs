using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIbase
{
    public GameObject m_go;

    public virtual void DoCreate(string path)
    {
        InsGo(path);
        DoShow(true);
    }

    private void InsGo(string path)
    {
        m_go = GameObject.Instantiate(Resources.Load<GameObject>(path));
        m_go.transform.SetParent(UIMgr.Instance.m_uiroot.transform, false);
        m_go.transform.localPosition = Vector3.zero;
        m_go.transform.localScale = Vector3.one;
    }

    public virtual void DoShow(bool active)
    {
        if (m_go)
        {
            m_go.SetActive(active);
        } 
    }

    public virtual void Destory()
    {
        GameObject.Destroy(m_go);
    }
    
}
