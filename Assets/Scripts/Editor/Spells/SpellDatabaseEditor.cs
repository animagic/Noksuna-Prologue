using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpellDatabaseEditor : EditorWindow
{
    SpellDatabase SpellDatabase;
    BaseSpell SelectedSpell;
    Vector2 _scrollPos;

    int fieldHeight = 15;
    int textFieldWidth = 300;

    const string DATABASE_PATH = @"Assets/Resources/Databases/SpellDatabase.asset";

    [MenuItem("Noksuna Tools/Databases/Spell Database Editor")]
    public static void Init()
    {
        SpellDatabaseEditor window = EditorWindow.GetWindow<SpellDatabaseEditor>();
        window.minSize = new Vector2(700, 300);
        window.titleContent = new GUIContent("Spell Database Editor");
        window.Show();
    }

    private void OnEnable()
    {

        SpellDatabase = AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(SpellDatabase)) as SpellDatabase;
        if (SpellDatabase == null)
        {
            SpellDatabase = ScriptableObject.CreateInstance<SpellDatabase>();
            AssetDatabase.CreateAsset(SpellDatabase, DATABASE_PATH);
            EditorUtility.SetDirty(SpellDatabase);
            AssetDatabase.SaveAssets();
        }

        SelectedSpell = new BaseSpell();
    }

    private void OnGUI()
    {

        DisplayDatabaseItems();

    }

    private void OnInspectorUpdate()
    {
        Repaint();

    }

    void AddSpellToDatabaseGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        //SelectedSpell.SpellName = EditorGUILayout.TextField("Spell Name: ", SelectedSpell.SpellName, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        //SelectedSpell.SpellDamageType = (BaseSpell.SpellDamageTypeEnum)EditorGUILayout.EnumPopup("Spell Damage Type: ", SelectedSpell.SpellDamageType, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        //SelectedSpell.SpellAction = (BaseSpell.SpellActionType)EditorGUILayout.EnumPopup("Spell Damage Type: ", SelectedSpell.SpellAction, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        //SelectedSpell.SpellPower = EditorGUILayout.IntField("Spell Power: ", SelectedSpell.SpellPower, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        //if (GUILayout.Button("Add New Spell", GUILayout.Width(100), GUILayout.Height(60)))
        //{
        //    if (String.IsNullOrEmpty(SelectedSpell.SpellName))
        //    {
        //        Debug.Log("The Spell must have a name!");
        //        return;
        //    }

        //    SpellDatabase.AddToDatabase(SelectedSpell);

        //    SelectedSpell = new BaseSpell();
        //}
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    void DisplayDatabaseItems()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandHeight(true));
        ListDatabaseItems();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        AddSpellToDatabaseGUI();
    }

    void ListDatabaseItems()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        for (int cnt = 0; cnt < SpellDatabase.Count; cnt++)
        {
            EditorGUILayout.BeginHorizontal();
            // Data Vertical
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            BaseSpell _spell = SpellDatabase.GetSingle(cnt);
            //_spell.SpellName = EditorGUILayout.TextField("Spell Name: ", _spell.SpellName, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            //_spell.SpellDamageType = (BaseSpell.SpellDamageTypeEnum)EditorGUILayout.EnumPopup("Spell Damage Type: ", _spell.SpellDamageType, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //_spell.SpellPower = EditorGUILayout.IntField("Spell Power: ", _spell.SpellPower, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            //EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            //_spell.SpellAction = (BaseSpell.SpellActionType)EditorGUILayout.EnumPopup("Spell Action Type: ", _spell.SpellAction, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            //EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            // Action button Vertical
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save", GUILayout.Width(50), GUILayout.Height(fieldHeight * 2)))
            {
                //SpellDatabase.GetSingle(cnt).SpellName = _spell.SpellName;
                //SpellDatabase.GetSingle(cnt).SpellDamageType = _spell.SpellDamageType;
                //SpellDatabase.GetSingle(cnt).SpellPower = _spell.SpellPower;
                //SpellDatabase.GetSingle(cnt).SpellAction = _spell.SpellAction;
                SpellDatabase.SortDatabase();
                EditorUtility.SetDirty(SpellDatabase);
            }

            //if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(fieldHeight * 2)))
            //{
            //    if (EditorUtility.DisplayDialog(
            //        "Delete Spell",
            //        "Delete the spell: " + /*_spell.SpellName */+" from the database?",
            //        "Okay", "Cancel"))
            //    {
            //        SpellDatabase.RemoveFromDatabase(cnt);
            //    }
            //}
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();


        }
        EditorGUILayout.EndVertical();
    }
}
