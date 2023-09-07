using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<CharacterBase> players;

    [SerializeField] private List<ColorConfig> colorsConfig;

    [SerializeField] private float gameTime = 60f;

    public ColorConfig GetColor => colorsConfig[Count - 1];

    private void Start() 
    {
        for (int i = 0; i < players.Count; i++)
        {
            CharacterBase character = players[i];
            ColorConfig colorConfig = colorsConfig[i];

            character.CharacterColor.SetColorData(colorConfig.GetColorData);
        }   
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

    public int Count => players.Count;
    public void AddPlayer(CharacterBase player)
    {
        if(players.Contains(player))
        {
            Debug.LogError("Player is already added");
            return;
        }

        players.Add(player);
    }

    public void RemovePlayer(CharacterBase player)
    {
        if(!players.Contains(player))
        {
            Debug.LogError("The player is not exist in the list.");
            return;
        }

        players.Remove(player);
    }

   
}
