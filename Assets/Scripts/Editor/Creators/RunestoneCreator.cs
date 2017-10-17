//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using UnityEditor;
//using UnityEngine;

//public class RunestoneCreator : EditorWindow
//{
//    Vector2 _scrollPos;
//    BaseRunestone Runestone;

//    string stoneName = "";
//    StoneType stoneType;
//    int fieldHeight = 15;
//    int textFieldWidth = 300;

//    const string ElysianPath = @"/Resources/Scriptable Objects/Runestones/ElysianStones/";
//    const string JuronianPath = @"/Resources/Scriptable Objects/Runestones/JuronianStones/";
//    const string ArcanePath = @"/Resources/Scriptable Objects/Runestones/ArcaneStones/";

//    private enum StoneType
//    {
//        ELYSIAN,
//        JURONIAN,
//        ARCANE
//    }

//    [MenuItem("Noksuna Tools/Creators/Runestones")]
//    public static void Init()
//    {
//        RunestoneCreator window = EditorWindow.GetWindow<RunestoneCreator>();
//        window.minSize = new Vector2(700, 300);
//        window.titleContent = new GUIContent("Runestone Creator", "Runestone Creator");
//        window.Show();
//    }

//    private void OnEnable()
//    {
//        Runestone = new BaseRunestone();
//        stoneName = "";
//    }

//    private void OnGUI()
//    {
//        DisplayEditor();
//    }

//    private void OnInspectorUpdate()
//    {
//        Repaint();
//    }

//    void AddSpellToDatabaseGUI()
//    {
//        EditorGUILayout.BeginHorizontal();
//        EditorGUILayout.BeginVertical();
//        stoneName = EditorGUILayout.TextField("Runestone Name: ", stoneName, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
//        stoneType = (StoneType)EditorGUILayout.EnumPopup("Stone Type: ", stoneType, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
//        EditorGUILayout.EndVertical();
//        EditorGUILayout.BeginVertical();
//        if (GUILayout.Button("Create Runestone", GUILayout.Width(200), GUILayout.Height(90)))
//        {
//            if (String.IsNullOrEmpty(stoneName))
//            {
//                Debug.Log("The Runestone needs a name!");
//                return;
//            }

//            List<string> allStones = new List<string>();
//            List<string> stoneNames = new List<string>();
//            switch (stoneType)
//            {
//                case StoneType.ELYSIAN:
//                    allStones = Directory.GetFiles(Application.dataPath + ElysianPath).ToList();
//                    break;
//                case StoneType.JURONIAN:
//                    allStones = Directory.GetFiles(Application.dataPath + JuronianPath).ToList();
//                    break;
//                case StoneType.ARCANE:
//                    allStones = Directory.GetFiles(Application.dataPath + ArcanePath).ToList();
//                    break;
//            }
//            foreach (string s in allStones)
//            {
//                string fileName = s.Remove(s.Length - 6);
//                stoneNames.Add(fileName);
//            }
//            bool exists = stoneNames.Any(x => x == stoneName);
//            if(exists)
//            {
//                Debug.Log("This name is already taken");
//                return;
//            }
//            else
//            {
//                CreateStone(stoneType, stoneName);
//            }
            

//            Runestone = new BaseRunestone();
//            stoneName = "";
//        }
//        EditorGUILayout.EndVertical();
//        EditorGUILayout.EndHorizontal();
//    }

//    void CreateStone(StoneType type, string name)
//    {
//        switch (type)
//        {
//            //case StoneType.ELYSIAN:
//            //    ElysianStone eStone = ScriptableObject.CreateInstance("ElysianStone") as ElysianStone;
//            //    AssetDatabase.CreateAsset(eStone, @"Assets" + ElysianPath + name + ".asset");
//            //    AssetDatabase.SaveAssets();
//            //    break;
//            //case StoneType.JURONIAN:
//            //    JuronianStone jStone = ScriptableObject.CreateInstance("JuronianStone") as JuronianStone;
//            //    AssetDatabase.CreateAsset(jStone, @"Assets" + JuronianPath + name + ".asset");
//            //    AssetDatabase.SaveAssets();
//            //    break;
//            //case StoneType.ARCANE:
//            //    ArcaneStone aStone = ScriptableObject.CreateInstance("ArcaneStone") as ArcaneStone;
//            //    AssetDatabase.CreateAsset(aStone, @"Assets" + ArcanePath + name + ".asset");
//            //    AssetDatabase.SaveAssets();
//            //    break;
//        }
//    }

//    void DisplayEditor()
//    {
//        AddSpellToDatabaseGUI();
//    }
//}
