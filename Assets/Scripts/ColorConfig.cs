using UnityEngine;

[CreateAssetMenu(fileName = "ColorConfig", menuName = "ColorConfig", order = 0)]
public class ColorConfig : ScriptableObject 
{
    [field: SerializeField] public Color Color {get; private set;}
    [field: SerializeField] public ColorType ColorType {get; private set;}

    public ColorData GetColorData => new ColorData(Color, ColorType);
    
}

public enum ColorType
{
    Blue,
    Red,
    Green,
    Yellow,
    Uncolored
}

public struct ColorData
{
    public Color Color {get; private set;}
    public ColorType ColorType {get; private set;}

    public ColorData(Color color, ColorType colorType)
    {
        Color = color;
        ColorType = colorType;
    }

    public static bool operator ==(ColorData c1, ColorData c2)
    {
        return c1.ColorType == c2.ColorType;
    }

    public static bool operator !=(ColorData c1, ColorData c2)
    { 
        return c1.ColorType != c2.ColorType;
    }

    public override bool Equals(object obj)
    {
        ColorData otherData = (ColorData)obj;
        return this.ColorType == otherData.ColorType;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public interface IHaveColor
{
    ColorData ColorData {get;}
}