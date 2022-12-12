using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class monstervalue
{
    public bool isselect = true;
    public MonsterType type = MonsterType.Normal;
}

public class MonsterEditor:EditorWindow
{
    static MonsterEditor win;

    static List<GameObject> monsters = new List<GameObject>();
    static List<monstervalue> monster_value = new List<monstervalue>();

    static JsonData _json = new JsonData();

    static string path ;
    static GameObject root;

    [MenuItem("Window/EditorWindow")]
    static void ShowMonsterWindow()
    {
        path = Application.streamingAssetsPath + @"/monster.json";
        root = GameObject.Find("NPC_Root");
        
        monsters.Clear();
        monster_value.Clear();
        _json.datas.Clear();

        win = EditorWindow.GetWindow<MonsterEditor>(typeof(MonsterEditor));
        win.Show();
    }
    MonsterData data;



    GUILayoutOption[] size1 = {
            GUILayout.Width(200),
            GUILayout.Height(50)
    };
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        if (monsters.Count > 0)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].activeSelf)
                {
                    EditorGUILayout.BeginHorizontal();
                    monster_value[i].isselect = EditorGUILayout.Toggle(monsters[i].name, monster_value[i].isselect);
                    monster_value[i].type = (MonsterType)EditorGUILayout.EnumPopup("type:", monster_value[i].type);
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    monster_value[i].isselect = false;
                }
            }

            if (GUILayout.Button("save"))
            {
                SaveData();
            } 
        }
        EditorGUILayout.EndVertical();
    }

    private void SaveData()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        for (int i = 0; i < monster_value.Count; i++)
        {
            if (monster_value[i].isselect)
            {
                Debug.Log(string.Format("统计数据 name:{0} pos:{1},{2},{3}", monsters[i].name, monsters[i].transform.position.x
                    , monsters[i].transform.position.y, monsters[i].transform.position.z));


                data = new MonsterData(monsters[i].name, monsters[i].transform.position.x
                    , monsters[i].transform.position.y, monsters[i].transform.position.z, monster_value[i].type);
                _json.datas.Add(data);
            }
        }
        string json = JsonUtility.ToJson(_json);
        Debug.Log(json);
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
        }
        File.WriteAllText(path, json);
        Debug.Log("生成json成功");
    }

    int cnt;
    monstervalue value;
    private void Update()
    {
        if (root)
        {
            cnt = root.transform.childCount;
            if (cnt > 0 && cnt != monsters.Count)
            {
                monsters.Clear();
                monster_value.Clear();
                for (int i = 0; i < cnt; i++)
                {
                    monsters.Add(root.transform.GetChild(i).gameObject);
                    value = new monstervalue();
                    value.isselect = true;
                    monster_value.Add(value);
                }
            }
        }
    }
}



