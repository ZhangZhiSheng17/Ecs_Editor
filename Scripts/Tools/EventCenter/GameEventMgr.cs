using UnityEngine;
using System.Collections.Generic;
using System;


#region 基于委托的事件中心
public class MsgCenter : SingleTon<MsgCenter>
{
    //存储消息的集合
    Dictionary<string, Action<Notification>> m_MsgDicts = new Dictionary<string, Action<Notification>>();
    /// <summary>
    /// 注册监听的方法
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="action"></param>
    public void AddListener(string msg, Action<Notification> action)
    {
        if (!m_MsgDicts.ContainsKey(msg))
        {
            m_MsgDicts.Add(msg, null);
        }
        m_MsgDicts[msg] += action;
    }

    /// <summary>
    /// 解除注册的方法
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="action"></param>
    public void RemoveListener(string msg, Action<Notification> action)
    {
        if (m_MsgDicts.ContainsKey(msg))
        {
            m_MsgDicts[msg] -= action;
            if (m_MsgDicts[msg] == null)
            {
                m_MsgDicts.Remove(msg);
            }
        }
    }

    /// <summary>
    /// 广播的方法
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="notify"></param>
    public void SendMsg(string msg, Notification notify)
    {
        if (m_MsgDicts.ContainsKey(msg))
        {
            m_MsgDicts[msg].Invoke(notify);
        }
    }
}

/// <summary>
/// 消息类型枚举
/// </summary>
/// <summary>
/// 消息类
/// </summary>
public class Notification
{
    //消息类型
    public string msg;
    //存储消息的集合
    public object[] data;

    public void Refresh(string msg,params object[] data)
    {
        this.msg = msg;
        this.data = data;
    }
    public void Clear()
    {
        msg = string.Empty;
        data = null;
    }
}
#endregion
