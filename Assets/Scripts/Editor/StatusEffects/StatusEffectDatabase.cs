using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class StatusEffectDatabase : ScriptableObject
{
    [SerializeField]
    List<BaseStatusEffect> database = new List<BaseStatusEffect>();

    public void AddToDatabase(BaseStatusEffect _effect)
    {
        database.Add(_effect);
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public void InsertIntoDatabase(int index, BaseStatusEffect _effect)
    {
        database.Insert(index, _effect);
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public void RemoveFromDatabase(BaseStatusEffect _effect)
    {
        database.Remove(_effect);
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public void RemoveFromDatabase(int index)
    {
        database.RemoveAt(index);
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public void ReplaceSpell(int index, BaseStatusEffect _effect)
    {
        database[index] = _effect;
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public int Count
    {
        get { return database.Count; }
    }

    public BaseStatusEffect GetSingle(int index)
    {
        return database.ElementAt(index);
    }

    public void SortDatabase()
    {
        List<BaseStatusEffect> sortedDatabase = database.OrderBy(x => x.Name).ToList();
        database = sortedDatabase;
        EditorUtility.SetDirty(this);
    }

    public List<BaseStatusEffect> GetAll()
    {
        return database;
    }
}
