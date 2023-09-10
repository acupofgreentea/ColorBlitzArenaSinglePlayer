using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class CharacterBase : MonoBehaviour, IPunchable, IPunchUser
{
    public CharacterMovement CharacterMovement {get; private set;}
    public CharacterAnimationControllerBase CharacterAnimationControllerBase {get; private set;}
    public CharacterColor CharacterColor {get; private set;}
    public CharacterName CharacterName {get; private set;}
    public CharacterPunchControllerBase CharacterPunchController {get; private set;}

    protected virtual void Awake()
    {
        CharacterMovement = GetComponent<CharacterMovement>().Init(this);
        CharacterAnimationControllerBase = GetComponent<CharacterAnimationControllerBase>().Init(this);
        CharacterPunchController = GetComponent<CharacterPunchControllerBase>().Init(this);
        CharacterColor = GetComponent<CharacterColor>();
        CharacterName = GetComponent<CharacterName>();
    }

    public event UnityAction OnPunchUse;
    public event UnityAction<float> OnGetPunched;
    public event UnityAction OnStunFinished;
    public bool IsStunned {get; set;}

    public void HandleGetPunch(float stunDuration)
    {
        OnGetPunched?.Invoke(stunDuration);
        IsStunned = true;
        StartCoroutine(Sequence());

        IEnumerator Sequence()
        {
            yield return new WaitForSeconds(stunDuration);
            OnStunFinished?.Invoke();
        }
    }
    public void Punch()
    {
        OnPunchUse?.Invoke();
    }
}
