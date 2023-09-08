using System;
using System.Collections.Generic;
using UnityEngine;

public static class NameGenerator
{
    [Serializable]
    private class NamesList
    {
        public List<string> names;
    }
    static NamesList namesList;
    static NamesList CurrentNamesList
    {
        get
        {
            if (namesList == null)
            {
                TextAsset textAsset = Resources.Load("Texts/NamesList") as TextAsset;
                namesList = JsonUtility.FromJson<NamesList>(textAsset.text);
            }
            return namesList;
        }
    }
    public static string GetRandomName()
    {
        return CurrentNamesList.names[UnityEngine.Random.Range(0, CurrentNamesList.names.Count)];
    }
    public static string[] GetRandomNames(int nbNames)
    {
        if (nbNames > CurrentNamesList.names.Count)
            throw new Exception("Asking for more random names than there actually are!");
        
        NamesList copy = new NamesList();
        copy.names = new List<string>(CurrentNamesList.names);
        string[] result = new string[nbNames];
        for (int i = 0; i < nbNames; i++)
        {
            int rnd = UnityEngine.Random.Range(0, copy.names.Count);
            result[i] = copy.names[rnd];
            copy.names.RemoveAt(rnd);
        }
        return result;
    }
}
