using UnityEngine;
public class CharacterBase : MonoBehaviour
{
    public CharacterMovement CharacterMovement {get; private set;}
    public CharacterAnimationControllerBase CharacterAnimationControllerBase {get; private set;}
    public CharacterColor CharacterColor {get; private set;}

    protected virtual void Awake()
    {
        CharacterMovement = GetComponent<CharacterMovement>().Init(this);
        CharacterAnimationControllerBase = GetComponent<CharacterAnimationControllerBase>().Init(this);
        CharacterColor = GetComponent<CharacterColor>();
    }
}
