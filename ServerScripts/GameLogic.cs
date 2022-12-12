using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerActionBase
{
    public SysType type;
    public ServerPlayer m_player;

    public ServerActionBase(ServerPlayer m_player)
    {
        this.m_player = m_player;
    }

   
}

public class ServerBattleAction : ServerActionBase
{
    public ServerPlayer m_target;

    public ServerBattleAction(ServerPlayer player) 
        :base(player)
    {
    }

    public void PlaySkill(int skillid)
    {
        ServerSkillInfo info = m_player.m_info.m_skills[skillid];
        if (Vector3.Distance(m_player.m_info.m_pos, m_target.m_info.m_pos)> info.range)
        {
            //判断target掉血了
            m_target.RefreshBaseData(1, info.atk_value * -1);
            //if ()  如果没有僵直，没有出发霸体，广播受击成功
            {
                m_target.Strike();
            }
        }
    }
    
}

public class ServerTaskAction : ServerActionBase
{
    public ServerTaskAction(ServerPlayer player)
        : base(player)
    {
    }
}

public enum SysType
{
    battle,
    task,

}

public class ServerSkillInfo
{
    public float range;
    public float mp;
    public float check_atk;
    public float buff_group;
    public float lock_target;
    public float skill_cd;
    public float atk_value;
}

public class ServerTaskInfo
{
    //
    public int id;
    public string name;
    public int limitLev;
}

public class ServerPlayerInfo
{
    public Vector3 m_pos;
    public float m_mp;
    public float m_hp;
    public Dictionary<int, ServerSkillInfo> m_skills;
    public List<ServerTaskInfo> m_tasks;
}

//代表服务器这边的一个角色所有的东西
public class ServerPlayer
{
    public int m_insid;
    public ServerPlayerInfo m_info;
    public Dictionary<SysType, ServerActionBase> m_allAction = new Dictionary<SysType, ServerActionBase>();
    Notification notify = new Notification();

    public ServerPlayer()
    {
        IntiPlayerInfo();
    }

    public void IntiPlayerInfo()
    {
        m_info = new ServerPlayerInfo();
        //读表
    }
    public void RefreshPos(Vector3 pos)
    {
        m_info.m_pos = pos;
    }
    /// <summary>
    /// key = 1:hp  2;mp  3:金币  4:钻石 5:点券  .........
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void RefreshBaseData(int key, float value)
    {
        switch (key)
        {
            case 1:
                m_info.m_hp += value;
                break;
            case 2:
                m_info.m_mp += value;
                break;
            default:
                break;
        }

        SendMsg2Client("TOCLIENT", "RefreshBaseInfo", m_insid, m_info);
    }

    public void PlaySkill(int skillid, ServerPlayer target)
    {
        ServerActionBase _act;
        if (!m_allAction.TryGetValue(SysType.battle, out _act))
        {
            _act = new ServerBattleAction(this);
            m_allAction[SysType.battle] = _act;
        }
        ServerBattleAction act = _act as ServerBattleAction;
        act.m_target = target;
    }


    public void Strike()
    {
        SendMsg2Client("TOCLIENT", "Strike", m_insid);
    }

    public void SendMsg2Client(string typecode, string msgcode, params object[] para)
    {
        notify.Refresh(msgcode, para);
        MsgCenter.Instance.SendMsg(typecode, notify);
        notify.Clear();
    }
}


//游戏服务器
/*
 1、初始化世界
 2、缓存所有实例
 2、各种实例间的交互决策逻辑
     */
public class GameLogic : MonoBehaviour
{
    public Dictionary<int, ServerPlayer> m_allplayer = new Dictionary<int, ServerPlayer>();

    public GameLogic()
    {
        MsgCenter.Instance.AddListener("TOSERVER", ToServer);

        IntiWorld();
    }

    private void ToServer(Notification notify)
    {
        switch (notify.msg)
        {
            case "PlayerMove":
                int insid = (int)notify.data[0];
                Vector3 pos = (Vector3)notify.data[1];
                m_allplayer[insid].RefreshPos(pos);
                break;
            case "BattleSkill":
                break;
            default:
                break;
        }
    }

    private void IntiWorld()
    {
        ServerPlayer player;
        for (int i = 0; i < 10; i++)
        {
            player = new ServerPlayer();
            player.m_insid = i + 1;
            player.RefreshPos(Vector3.zero);
            player.SendMsg2Client("TOCLIENT", "RefreshPlayer", player.m_info);
        }
    }


}
