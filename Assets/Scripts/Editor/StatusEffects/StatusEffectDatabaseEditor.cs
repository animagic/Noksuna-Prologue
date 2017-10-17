using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatusEffectDatabaseEditor : EditorWindow
{
    StatusEffectDatabase StatusEffectDatabase;
    BaseStatusEffect SelectedStatusEffect;
    Vector2 _scrollPos;

    int fieldHeight = 15;
    int textFieldWidth = 300;

    const string DATABASE_PATH = @"Assets/Resources/Databases/StatusEffectDatabase.asset";

    //[MenuItem("Noksuna Tools/Databases/Status Effect Database Editor")]
    public static void Init()
    {
        StatusEffectDatabaseEditor window = EditorWindow.GetWindow<StatusEffectDatabaseEditor>();
        window.minSize = new Vector2(700, 300);
        window.titleContent = new GUIContent("Status Effect Database Editor");
        window.Show();
    }

    private void OnEnable()
    {
        StatusEffectDatabase = AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(StatusEffectDatabase)) as StatusEffectDatabase;
        if (StatusEffectDatabase == null)
        {
            StatusEffectDatabase = ScriptableObject.CreateInstance<StatusEffectDatabase>();
            AssetDatabase.CreateAsset(StatusEffectDatabase, DATABASE_PATH);
            EditorUtility.SetDirty(StatusEffectDatabase);
            AssetDatabase.SaveAssets();
        }
        SelectedStatusEffect = new BaseStatusEffect();
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
        SelectedStatusEffect.Name = EditorGUILayout.TextField("Status Effect Name: ", SelectedStatusEffect.Name, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        SelectedStatusEffect.MaxDuration = EditorGUILayout.FloatField("Max Duration: ", SelectedStatusEffect.MaxDuration, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        SelectedStatusEffect.InitialDuration = EditorGUILayout.FloatField("Initial Duration: ", SelectedStatusEffect.InitialDuration, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        SelectedStatusEffect.TimeIntervalToActivate = EditorGUILayout.FloatField("Activation Interval (sec): ", SelectedStatusEffect.TimeIntervalToActivate, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        SelectedStatusEffect.ChanceToActivate = EditorGUILayout.FloatField("Chance to Activate (0 - 1): ", SelectedStatusEffect.ChanceToActivate, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        //SelectedStatusEffect.Power = EditorGUILayout.FloatField("Status Effect Power: ", SelectedStatusEffect.Power, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Add New Status Effect", GUILayout.Width(200), GUILayout.Height(90)))
        {
            if (String.IsNullOrEmpty(SelectedStatusEffect.Name))
            {
                Debug.Log("The Status Effect must have a name!");
                return;
            }
            Debug.Log(SelectedStatusEffect.Name);
            StatusEffectDatabase.AddToDatabase(SelectedStatusEffect);

            SelectedStatusEffect = new BaseStatusEffect();
        }
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
        for (int cnt = 0; cnt < StatusEffectDatabase.Count; cnt++)
        {
            EditorGUILayout.BeginHorizontal();
            // Data Vertical
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            BaseStatusEffect _effect = StatusEffectDatabase.GetSingle(cnt);
            _effect.Name = EditorGUILayout.TextField("Status Effect Name: ", _effect.Name, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            //_effect.Power = EditorGUILayout.FloatField("Status Effect Power: ", _effect.Power, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            _effect.MaxDuration = EditorGUILayout.FloatField("Max Duration: ", _effect.MaxDuration, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            _effect.InitialDuration = EditorGUILayout.FloatField("Initial Duration: ", _effect.InitialDuration, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            _effect.TimeIntervalToActivate = EditorGUILayout.FloatField("Activation Interval (sec): ", _effect.TimeIntervalToActivate, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            _effect.ChanceToActivate = EditorGUILayout.FloatField("Chance to Activate: ", _effect.ChanceToActivate, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            // Action button Vertical
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save", GUILayout.Width(50), GUILayout.Height(fieldHeight * 2)))
            {
                StatusEffectDatabase.GetSingle(cnt).Name = _effect.Name;
                StatusEffectDatabase.GetSingle(cnt).MaxDuration = _effect.MaxDuration;
                StatusEffectDatabase.GetSingle(cnt).InitialDuration = _effect.InitialDuration;
                StatusEffectDatabase.GetSingle(cnt).TimeIntervalToActivate = _effect.TimeIntervalToActivate;
                StatusEffectDatabase.GetSingle(cnt).ChanceToActivate = _effect.ChanceToActivate;
                //StatusEffectDatabase.GetSingle(cnt).Power = _effect.Power;
                StatusEffectDatabase.SortDatabase();
                EditorUtility.SetDirty(StatusEffectDatabase);
            }

            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(fieldHeight * 2)))
            {
                if (EditorUtility.DisplayDialog(
                    "Delete Status Effect",
                    "Delete the Status Effect: " + _effect.Name + " from the database?",
                    "Okay", "Cancel"))
                {
                    StatusEffectDatabase.RemoveFromDatabase(cnt);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndVertical();
    }
}
