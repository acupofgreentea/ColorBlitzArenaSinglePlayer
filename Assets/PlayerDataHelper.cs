using System.Collections.Generic;
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

    public int BluePaintCount;
    public int RedPaintCount;
    public int YellowPaintCount;
    public int GreenPaintCount;

    public void SetCount(ColorType colorType, int amount)
    {
        switch (colorType)
        {
            case ColorType.Blue:
            BluePaintCount += amount;
                break;
            case ColorType.Red:
                    RedPaintCount += amount;
                break;
            case ColorType.Yellow:
                    YellowPaintCount += amount;
                break;
            case ColorType.Green:
                    GreenPaintCount += amount;
                break;
        }
    }
}

public struct PaintCounter
{
    public ColorType colorType;
}