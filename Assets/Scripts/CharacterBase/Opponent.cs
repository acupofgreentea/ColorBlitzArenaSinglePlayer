using UnityEngine.Events;

public class Opponent : CharacterBase, IPunchable, IPunchUser
{
    public new OpponentMovement CharacterMovement => base.CharacterMovement as OpponentMovement;
    public OpponentPunchController OpponentPunchController {get; private set;}
    public OpponentAnimationEventHandler OpponentAnimationEventHandler {get; private set;}

    public bool IsStunned {get; set;}
    protected override void Awake() 
    {
        base.Awake();
        OpponentPunchController = GetComponent<OpponentPunchController>();
        OpponentAnimationEventHandler = GetComponentInChildren<OpponentAnimationEventHandler>().Init(this);
    }

    public event UnityAction OnPunchUse;
    public event UnityAction<float> OnGetPunched;

    public void HandleGetPunch(float stunDuration)
    {
        OnGetPunched?.Invoke(stunDuration);
        IsStunned = true;
    }

    public void Punch()
    {
        OnPunchUse?.Invoke();
    }
}
