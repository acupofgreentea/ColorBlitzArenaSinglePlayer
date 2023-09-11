using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CharacterBase> players;

    [SerializeField] private List<ColorConfig> colorsConfig;

    [SerializeField] private NamesSO playerNames;

    public int PlayerCount => players.Count;

    public event UnityAction<string[]> OnNamesSet;

    public static int PlayerPaintCount = 0;

    private void Start() 
    {
        SessionManager.OnSessionStart += HandleSessionStart;
        SessionManager.OnSessionFinish += HandleSessionFinish;
        SessionScoreboardUI.GetCharacterNames += GetCharacterNameByColorType;
        SessionScoreboardUI.GetCharacterColor += GetColor;

        SetCharacters();
    }

    private void SetCharacters()
    {
        List<string> randomNames = playerNames.GenerateRandomNames(players.Count);
        string playerDataName = PlayerDataHelper.Instance.Load().PlayerName;

        colorsConfig.Randomize();
        
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

    private string GetCharacterNameByColorType(ColorType type)
    {
        return players.Find(x => x.CharacterColor.ColorData.ColorType == type).CharacterName.Name;
    }
    private Color GetColor(ColorType colorType)
    {
        var color = colorsConfig.Find(x => x.ColorType == colorType);
        return color.Color;
    } 

    public CharacterBase GetPlayAtIndex(int i) => players[i];
    private void HandleSessionFinish()
    {
        SaveData();
    }

    private void HandleSessionStart()
    {
        PlayerPaintCount = 0;
    }

    private void SaveData()
    {
        var data = PlayerDataHelper.Instance.Load();
        data.GridPaintedCount += PlayerPaintCount;
        data.SetCount(colorsConfig[0].ColorType, PlayerPaintCount);
        PlayerDataHelper.Instance.Save();
    }

    private void OnDestroy() 
    {
        SessionManager.OnSessionStart -= HandleSessionStart;
        SessionManager.OnSessionFinish -= HandleSessionFinish;
        SessionScoreboardUI.GetCharacterNames -= GetCharacterNameByColorType;        
        SessionScoreboardUI.GetCharacterColor -= GetColor;
    }
}
