using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battlesys : UIbase
{
    private Button m_gatherBtn;
    private Slider m_gatherSlider;
    private int m_gatherInsid;

    public override void DoCreate(string path)
    {
        base.DoCreate(path);

        MsgCenter.Instance.AddListener("ClientMsg", RefreshBtn);

        MsgCenter.Instance.AddListener("ServerMsg", ServerNotify);
    }

    private void ServerNotify(Notification obj)
    {
        if (obj.msg.Equals("gather_callback"))
        {
            m_gatherSlider.gameObject.SetActive(true);
        }
    }

    public override void DoShow(bool active)
    {
        base.DoShow(active);
        m_gatherBtn = m_go.transform.Find("gather_btn").GetComponent<Button>();
        m_gatherBtn.onClick.AddListener(() => {
            //交互服务器
            Notification notify = new Notification();
            notify.Refresh("gather", 1);
            MsgCenter.Instance.SendMsg("ServerMsg", notify);
        });
        m_gatherSlider = m_go.transform.Find("gather_slider").GetComponent<Slider>();
        m_gatherBtn.gameObject.SetActive(false);
        m_gatherSlider.gameObject.SetActive(false);
    }

    public override void Destory()
    {
        base.Destory();
        MsgCenter.Instance.RemoveListener("ClientMsg", RefreshBtn);
    }


    private void RefreshBtn(Notification notiy)
    {
        if (notiy.msg.Equals("gather_trigger"))
        {
            m_gatherInsid = (int)notiy.data[0];
            m_gatherBtn.gameObject.SetActive(true);
        }
    }
}
