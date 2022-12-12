using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
基础种类 
*/
public abstract class ObjectBase 
{
    public GameObject m_go;  // 存储当前物体
    public Vector3 m_local_pos;   //当前物体在本地的位置
    public Animator m_anim;
    public UIPate m_pate;
    public MonsterType m_type;

    public int m_insID;
    public string m_modelPath;  //模型路径
    
    public ObjectBase()
    {

    }
    // 创建物体的方法
    public virtual void CreateObj(MonsterType type)
    {
        m_type = type;
        if (!string.IsNullOrEmpty(m_modelPath) && m_insID >= 0)
        {
            m_go = (GameObject)GameObject.Instantiate(Resources.Load(m_modelPath));
            m_go.name = m_insID.ToString();
            m_go.transform.position = m_local_pos;
            if (m_go)
            {
                OnCreate();
            }
        }
    }
    //在创建的时候初始化的逻辑
    public virtual void OnCreate()
    {

    }
    //设置位置
    public virtual void SetPos(Vector3 pos)
    {
        m_local_pos = pos;
    }
    //移动
    public void MoveByTranslate( Vector3 look,Vector3 move)
    {
        //===TODO  使用Navmesh实现移动

        m_go.transform.LookAt(look);// 移动角色或物体的位置（按其所朝向的位置移动）
        m_go.transform.Translate(move);
    }
    //自定义懂
    public void AutoMove(Vector3 look, Vector3 move)
    {
        //TODO 显示路径点
        //TODO 通知小地图显示路径点

        MoveByTranslate(look,move);
    }
    // 销毁物体的方法
    public virtual void Destory()
    {
        if (m_pate)
        {
            GameObject.Destroy(m_pate);
        }
        GameObject.Destroy(m_go);
        m_local_pos = Vector3.zero;
        m_anim = null;
        m_insID = -1;
    }
}
