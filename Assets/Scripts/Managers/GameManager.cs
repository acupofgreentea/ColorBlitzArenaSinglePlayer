using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CharacterBase> players;

    [SerializeField] private List<ColorConfig> colorsConfig;

    [SerializeField] private NamesSO playerNames;

    public int Count => players.Count;

    public event UnityAction<string[]> OnNamesSet;

    public static int PlayerPaintCount = 0;

    private void Start() 
    {
        List<string> randomNames = playerNames.GenerateRandomNames(players.Count);
        string playerDataName = PlayerDataHelper.Instance.Load().PlayerName;

        string[] names = new string[players.Count];

        for (int i = 0; i < players.Count; i++)
        {
            CharacterBase character = players[i];
            ColorConfig colorConfig = colorsConfig[i];

            character.CharacterColor.SetColorData(colorConfig.GetColorData);
            string chName = i == 0 ? (string.IsNullOrEmpty(playerDataName) ? randomNames[i] : playerDataName) : randomNames[i]; //first index of players is actual player
            character.CharacterName.SetCharacterName(chName);
            names[i] = chName;
        } 

        OnNamesSet?.Invoke(names);
    }

    public CharacterBase GetPlayAtIndex(int i) => players[i];

    private void OnApplicationQuit()
    {
        var data = PlayerDataHelper.Instance.Load();
        data.GridPaintedCount += PlayerPaintCount;
        data.SetCount(colorsConfig[0].ColorType, PlayerPaintCount);
        PlayerDataHelper.Instance.Save();
    }
   
}
