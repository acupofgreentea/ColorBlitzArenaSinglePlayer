using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Names")]
public class NamesSO : ScriptableObject
{
    [SerializeField] private List<string> Names = new();

    [ContextMenu("Create Names")]
    private void CreateNames()
    {
        Names.Clear();
        Names = NameGenerator.GetRandomNames(100).ToList();

        for (int i = 0; i < Names.Count; i++)
        {
            string name = Names[i];
            string newName = name.Replace(" ", "");
            Names[i] = newName;
        }
    }

    [SerializeField, Tooltip("These names will be given to characters in game")] private List<string> generatedRandomNames = new();

    public List<string> GeneratedRandomNames => generatedRandomNames;
    public List<string> GenerateRandomNames(int playerCount)
    {
        generatedRandomNames.Clear();
        while (generatedRandomNames.Count < playerCount)
        {
            string newName = Names[Random.Range(0, Names.Count)];

            if (!generatedRandomNames.Contains(newName))
            {
                generatedRandomNames.Add(newName);
            }
        }

        return generatedRandomNames;
    }
}
