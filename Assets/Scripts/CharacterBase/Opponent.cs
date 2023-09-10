public class Opponent : CharacterBase
{
    public new OpponentMovement CharacterMovement => base.CharacterMovement as OpponentMovement;
    public OpponentAnimationEventHandler OpponentAnimationEventHandler {get; private set;}
    
    protected override void Awake() 
    {
        base.Awake();
        OpponentAnimationEventHandler = GetComponentInChildren<OpponentAnimationEventHandler>().Init(this);
    }

    
}
