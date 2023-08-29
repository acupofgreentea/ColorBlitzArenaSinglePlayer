public class Player : CharacterBase
{
    public new PlayerMovement CharacterMovement => base.CharacterMovement as PlayerMovement;
    public PlayerPunchController PlayerPunchController {get; private set;}
    public PlayerAnimationEventHandler PlayerAnimationEventHandler {get; private set;}

    protected override void Awake() 
    {
        base.Awake();
        CacheComponents();

    }

    private void Start() 
    {
        Managers.Instance.GameManager.AddPlayer(this);    
    }

    private void CacheComponents()
    {
        PlayerPunchController = GetComponent<PlayerPunchController>().Init(this);
        PlayerAnimationEventHandler = GetComponentInChildren<PlayerAnimationEventHandler>().Init(this);
    }
}
