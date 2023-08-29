using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    public ColorType ColorType {get; set;}

    [SerializeField] public Renderer _renderer;

    [SerializeField] public PlayerColorConfig ColorConfig;

    private void Awake() 
    {
        SetColor(ColorConfig);    
    }

    public void SetColor(PlayerColorConfig gamePlayersColorConfig)
    {
        ColorConfig = gamePlayersColorConfig;

        ColorType = ColorConfig.ColorType;
        _renderer.material.color = ColorConfig.Color;
    }
}
