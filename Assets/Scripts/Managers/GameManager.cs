using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CharacterBase> players;

    [SerializeField] private List<ColorConfig> colorsConfig;

    [SerializeField] private NamesSO playerNames;

    [SerializeField] private float gameTime = 60f;

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
    
    private void Update() 
    {
        gameTime-= Time.deltaTime;    
    }
    private void OnGUI() 
    {
        float remaningTime = gameTime;
        remaningTime = Mathf.RoundToInt(remaningTime);
        GUI.Label(new Rect(150, 0, 20, 20), remaningTime.ToString());
    }

    public CharacterBase GetPlayAtIndex(int i) => players[i];

    private void OnApplicationQuit()
    {
        var data = PlayerDataHelper.Instance.Load();
        data.GridPaintedCount += PlayerPaintCount;
        PlayerDataHelper.Instance.Save();
    }
   
}
