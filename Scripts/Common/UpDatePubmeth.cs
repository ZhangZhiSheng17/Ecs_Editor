using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDatePubmeth
{
    static private Dictionary<int, GameObject> m_allupdate = new Dictionary<int, GameObject>();
    static public int StartUpdate(Action _call)
    {
        int insid = -1;
        insid = m_allupdate.Count;
        GameObject tmp = new GameObject();
        UpDateUtils tmp_ud = tmp.AddComponent<UpDateUtils>();
        tmp_ud.m_call = _call;
        m_allupdate[insid] = tmp;

        return insid;
    }

    static public void KillUpdate(int id)
    {
        using (var tmp = m_allupdate.GetEnumerator())
        {
            while (tmp.MoveNext())
            {
                if (id == tmp.Current.Key)
                {
                    GameObject.Destroy(tmp.Current.Value);
                    m_allupdate.Remove(id);
                    break;
                }
            }
        }
    }
}
