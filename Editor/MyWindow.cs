using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MyWindow :EditorWindow
{

    private static Texture test;

    private static UnityEngine.Font m_font;

    private static GameObject m_go;

    [MenuItem("Window/MyWindow")]
    static void ShowWindow()
    {
        MyWindow win = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), false, "mywindow");
        win.autoRepaintOnSceneChange = true;
        win.Show();
    }

    Color curcolor = Color.red;
    GUILayoutOption[] size = {
            GUILayout.Width(200),
            GUILayout.Height(1000)
    };
    GUILayoutOption[] size1 = {
            GUILayout.Width(100),
            GUILayout.Height(100)
    };


    GameObject prefabe;
    private void OnGUI()
    {
        if (prefabe)
        {
            Debug.Log(prefabe.name);
            GameObject.Destroy(prefabe);
        
        }

        EditorGUILayout.BeginVertical(size);
        GUILayout.Label("ssssssssssssss");
        if (EditorWindow.mouseOverWindow)
        {
            EditorGUILayout.LabelField(EditorWindow.mouseOverWindow.ToString());
        }

        curcolor = EditorGUILayout.ColorField(curcolor, size1);

        GUILayout.Box("testtesttesttesttesttesttesttesttest");
        if (GUILayout.Button("print color"))
        {
            EditorUtility.DisplayProgressBar("color", "ahahah", 5);
            
        }
        if (GUILayout.Button("clear bar"))
        {
            EditorUtility.ClearProgressBar();
        }
        test = (Texture)EditorGUILayout.ObjectField(test, typeof(Texture));

        m_font = (UnityEngine.Font)EditorGUILayout.ObjectField(m_font, typeof(UnityEngine.Font));

        m_go = (GameObject)EditorGUILayout.ObjectField(m_go, typeof(UnityEngine.GameObject));

        if (GUILayout.Button("Create GO"))
        {
            if (prefabe)
            {

                prefabe = (GameObject)PrefabUtility.InstantiatePrefab(m_go);
                CheckLabel(prefabe.transform);
                EditorUtility.ClearProgressBar();
                GameObject.DestroyImmediate(prefabe);

            }
            GameObject go = GameObject.Find("Main Camera");

            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;


            //也可以选择Project下的Object
            //Selection.activeObject  = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Cube.prefab");

        }

        EditorGUILayout.EndVertical();


    }

    private static void CheckLabel(Transform childeGo)
    {
        Text label;
        Transform labelGo;
        for (int j = 0; j < childeGo.transform.childCount; j++)
        {
            labelGo = childeGo.transform.GetChild(j);
            label = labelGo.GetComponent<Text>();
            if (label)
            {
                EditorUtility.DisplayProgressBar("输出文字", $"text:{label.name}", 1);
                Debug.Log(label.text);
            }
            if (labelGo.transform.childCount > 0)
                CheckLabel(labelGo);
        }
    }
}
