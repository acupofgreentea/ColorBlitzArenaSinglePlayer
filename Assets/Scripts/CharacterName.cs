using UnityEngine;

public class CharacterName : MonoBehaviour
{
    private string characterName;
    public string Name => characterName;

    public void SetCharacterName(string name)
    {
        characterName = name;
    }
}
