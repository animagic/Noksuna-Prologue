using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AbilitiesMenuItems : MonoBehaviour {

    static string AbilityScriptPath = "/Scripts/Gameplay/Characters/Abilities/Abilities";
    static string CreatedAbilitySOPath = "/Resources/Scriptable Objects/Abilities";

    static string SpellScriptPath = "/Scripts/Gameplay/Characters/Abilities/Spells";
    static string CreatedSpellSOPath = "/Resources/Scriptable Objects/Spells";

    static string EffectScriptPath = "/Scripts/Gameplay/Characters/Abilities/StatusEffects";
    static string CreatedEffectSOPath = "/Resources/Scriptable Objects/Effects";

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [MenuItem("Noksuna Tools/Wipe Tools/Regenerate All Ability Assets")]
    public static void RegenerateAbilityAssets()
    {
        if (EditorUtility.DisplayDialog(
                   "Regenerate All Ability Assets",
                   "This action will regenerate ALL of the Ability ScriptableObjects, erasing all Inspector attached objects.  Are you sure you want to do this?",
                   "Okay", "Cancel"))
        {
            string[] allSkills = Directory.GetFiles(Application.dataPath + AbilityScriptPath);

            int x = 0;
            foreach(string s in allSkills)
            {
                string fileName = s.Substring(s.LastIndexOf("\\") + 1);
                if(fileName.EndsWith(".cs"))
                {
                    string script = fileName.Remove(fileName.Length - 3);
                    var scriptType = script;
                    var ability = ScriptableObject.CreateInstance(scriptType);
                    AssetDatabase.CreateAsset(ability, @"Assets/Resources/Ability Scriptable Objects/" + scriptType + ".asset");
                    AssetDatabase.SaveAssets();

                    Debug.Log("Created Asset: " + ability.name);
                    x++;
                }
            }
        }
    }

    [MenuItem("Noksuna Tools/Abilities/Generate New Ability Assets")]
    public static void CreateAbilityAssets()
    {
        List<string> allAbilities = Directory.GetFiles(Application.dataPath + AbilityScriptPath).ToList();
        List<string> createdAbilities = Directory.GetFiles(Application.dataPath + CreatedAbilitySOPath).ToList();

        if (allAbilities.Count > 0)
            CheckAndCreateSOSet(allAbilities, createdAbilities, CreatedAbilitySOPath, "Abilities");
        else
            Debug.Log("There are no Ability scripts to create SOs from.");
   }

    //[MenuItem("Noksuna Tools/Abilities/Generate New Spell Assets")]
    //public static void CreateSpellAssets()
    //{
    //    List<string> allSpells = Directory.GetFiles(Application.dataPath + SpellScriptPath).ToList();
    //    List<string> createdSpells = Directory.GetFiles(Application.dataPath + CreatedSpellSOPath).ToList();

    //    if (allSpells.Count > 0)
    //        CheckAndCreateSOSet(allSpells, createdSpells, CreatedSpellSOPath, "Spells");
    //    else
    //        Debug.Log("There are no Spell scripts to create SOs from.");
    //}

    [MenuItem("Noksuna Tools/Abilities/Generate New Status Effect Assets")]
    public static void CreateEffectAssets()
    {
        List<string> allEffects = Directory.GetFiles(Application.dataPath + EffectScriptPath).ToList();
        List<string> createdEffects = Directory.GetFiles(Application.dataPath + CreatedEffectSOPath).ToList();
        if (allEffects.Count > 0)
            CheckAndCreateSOSet(allEffects, createdEffects, CreatedEffectSOPath, "StatusEffects");
        else
            Debug.Log("There are no Status Effect scripts to create SOs from.");
    }

    static void CheckAndCreateSOSet(List<string> allList, List<string> createdList, string pathToSO, string nameSpace)
    {
        List<string> emptyList = new List<string>();
        List<string> curList = new List<string>();

        foreach (string str in createdList)
        {
            string fileName = str.Substring(str.LastIndexOf("\\") + 1);
            if (fileName.EndsWith(".asset"))
            {
                string f = fileName.Remove(fileName.Length - 6);
                curList.Add(f);
            }
        }

        int x = 0;
        foreach (string s in allList)
        {
            string fileName = s.Substring(s.LastIndexOf("\\") + 1);
            if (fileName.EndsWith(".cs"))
                emptyList.Add(fileName);
        }
        foreach (string s in emptyList)
        {
            string fileName = s.Substring(s.LastIndexOf("\\") + 1);

            if (fileName.EndsWith(".cs"))
            {
                string script = fileName.Remove(fileName.Length - 3);
                var scriptType = script;
                bool exists = curList.Any(y => y == script);
                if (!exists)
                {
                    var ability = ScriptableObject.CreateInstance(nameSpace + "." + scriptType);
                    AssetDatabase.CreateAsset(ability, @"Assets" + pathToSO + "/" + scriptType + ".asset");
                    AssetDatabase.SaveAssets();
                    
                    Debug.Log("Created " + nameSpace + " Asset: " + ability.name);
                }
                else
                {
                    Debug.Log(fileName + " already exists as a ScriptableObject");
                }
                x++;
            }
        }
    }

   

}
