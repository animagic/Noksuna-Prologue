using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SpellDatabase : ScriptableObject
{
    [SerializeField]
    List<BaseSpell> database = new List<BaseSpell>();

    public void AddToDatabase(BaseSpell _spell)
    {
        database.Add(_spell);
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public void InsertIntoDatabase(int index, BaseSpell _spell)
    {
        database.Insert(index, _spell);
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public void RemoveFromDatabase(BaseSpell _spell)
    {
        database.Remove(_spell);
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public void RemoveFromDatabase(int index)
    {
        database.RemoveAt(index);
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public void ReplaceSpell(int index, BaseSpell _spell)
    {
        database[index] = _spell;
        SortDatabase();
        EditorUtility.SetDirty(this);
    }

    public int Count
    {
        get { return database.Count; }
    }

    public BaseSpell GetSingle(int index)
    {
        return database.ElementAt(index);
    }

    public void SortDatabase()
    {
        //List<BaseSpell> sortedDatabase = database.OrderBy(x => x.SpellDamageType).ToList();
        //database = sortedDatabase;
        //EditorUtility.SetDirty(this);
    }

    public List<BaseSpell> GetAll()
    {
        return database;
    }
}
