using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDateUtils : MonoBehaviour
{
    public Action m_call;
    
    void Update()
    {
        m_call?.Invoke();
    }

    ~UpDateUtils()
    {
        m_call = null;
    }
}
