using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//账号数据类
//1、账号基础数据  2、角色数据  
public class AccountInfo:SingleTon<AccountInfo>
{
    public string m_head;
    public long create_time;
    public int lev;
    public int exp;
    public int gold;
    public int money;
    public int power;


    public player_info playerinfo;
}
