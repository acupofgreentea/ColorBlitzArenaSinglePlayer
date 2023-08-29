using UnityEngine;

[CreateAssetMenu(fileName = "PlayersColorConfig", menuName = "PlayersColorConfig", order = 0)]
public class PlayerColorConfig : ScriptableObject 
{
    [field: SerializeField] public Color Color {get; private set;}
    [field: SerializeField] public ColorType ColorType {get; private set;}
}

public enum ColorType
{
    Blue,
    Red,
    Green,
    Yellow,
    Default
}