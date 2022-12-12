using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_info//Data
{
    public int ID;        //1 or 2
    public string m_name; 
    public Vector3 m_pos;
    public string m_res;
    public MonsterType m_type;
}

public class player_info : object_info
{
    public int m_level;
    public float m_HP;
    public float m_hpMax;
    public float m_MP;
    public float m_mpMax;
    //技能列表
    public List<SkillXml> skillList;

}

public class npc_info : object_info
{
    public int m_plotId = 0; //0是不响应

    public npc_info(int plot,object_info info)
    {
        ID = info.ID;
        m_name = info.m_name;
        m_pos = info.m_pos;
        m_res = info.m_res;
        m_type = MonsterType.NPC;
    }
}

public class monster_info : object_info
{
    public monster_info(MonsterType type,object_info info)
    {
        ID = info.ID;
        m_name = info.m_name;
        m_pos = info.m_pos;
        m_res = info.m_res;
        m_type = type;
    }

}
