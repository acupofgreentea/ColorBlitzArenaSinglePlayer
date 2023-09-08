using BayatGames.SaveGameFree;
using UnityEngine;

public class PlayerDataHelper : MonoBehaviour
{
    private readonly string Identifier = "PlayerData";

    public static PlayerDataHelper Instance;
    
    public PlayerData playerData = new();
    
    private void Awake() 
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        SaveGame.Save<PlayerData>(Identifier, playerData, SaveGamePath.DataPath);
    }

    public PlayerData Load()
    {
        playerData = SaveGame.Load<PlayerData>(Identifier, new PlayerData(), SaveGamePath.DataPath);

        return playerData;
    }
}

[System.Serializable]
public class PlayerData 
{
    public string PlayerName {get; set;}
    public int GridPaintedCount {get; set;}
}