using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public float xMap, yMap;
    public float xoffset, yoffset;

    private Transform player;
    Dictionary<MonsterType, Transform> monsterdic = new Dictionary<MonsterType, Transform>();

    List<ObjectBase> otherGoPos = new List<ObjectBase>();
    Vector3 playerpos = new Vector3(0,0,0);
    List<Vector3> otherpos = new List<Vector3>();

    private void Awake()
    {
        xMap = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        yMap = this.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        xoffset = xMap / World.Instance.xlength;
        yoffset = xMap / World.Instance.ylength;

        player = transform.Find("player");
        monsterdic.Add(MonsterType.Gather, transform.Find("gather"));
        monsterdic.Add(MonsterType.Normal, transform.Find("monster"));
        monsterdic.Add(MonsterType.NPC, transform.Find("npc"));

        //InvokeRepeating("UpdateMap", 0, 0.3f);
    }
    
    void Update()
    {
        if (World.Instance.m_insDic.Count != otherGoPos.Count)
        {
            otherGoPos.Clear();
            otherpos.Clear();
            foreach (var item in World.Instance.m_insDic)
            {
                otherGoPos.Add( item.Value);
                otherpos.Add(new Vector3(0,0,0));
            }
        }
        if (player && World.Instance.m_plyer.m_go)
        {
            playerpos.Set(World.Instance.m_plyer.m_go.transform.position.x * xoffset, World.Instance.m_plyer.m_go.transform.position.z * yoffset, 0);
            //Debug.Log($"最终位置  x:{World.Instance.m_plyer.m_go.transform.position.x * xoffset}  y:{World.Instance.m_plyer.m_go.transform.position.z * yoffset}");
            player.localPosition = playerpos;
        }
        if (otherGoPos != null && otherGoPos.Count > 0)
        {
            for (int i = 0; i < otherGoPos.Count; i++)
            {
                otherpos[i] = new Vector3(otherGoPos[i].m_go.transform.position.x * xoffset, otherGoPos[i].m_go.transform.position.z * yoffset, 0);
                monsterdic[otherGoPos[i].m_type].transform.localPosition = otherpos[i];
            }
        }
        
    }

    private void OnDestroy()
    {

        CancelInvoke("UpdateMap");
    }

    //~MapControl()
    //{
    //    CancelInvoke("UpdateMap");
    //}
}
