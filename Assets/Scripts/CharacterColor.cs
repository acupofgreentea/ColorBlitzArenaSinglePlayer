using UnityEngine;

public class CharacterColor : MonoBehaviour, IHaveColor
{
    public ColorData ColorData {get; private set;}

    [SerializeField] public Renderer _renderer;

    public void SetColorData(ColorData colorData) 
    {
        ColorData = new ColorData(colorData.Color, colorData.ColorType);
    }

    private void Start() 
    {
        this.DelayOneFrame(SetColor);    
    }

    public void SetColor()
    {
        _renderer.material.color = ColorData.Color;
    }
}
