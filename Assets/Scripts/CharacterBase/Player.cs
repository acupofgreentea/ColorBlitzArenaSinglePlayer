public class Player : CharacterBase
{
    public new PlayerMovement CharacterMovement => base.CharacterMovement as PlayerMovement;
    public PlayerPunchController PlayerPunchController {get; private set;}
    public PlayerAnimationEventHandler PlayerAnimationEventHandler {get; private set;}
    public PlayerInputHandler PlayerInputHandler {get; private set;}

    public static Player Instance; 

    protected override void Awake() 
    {
        base.Awake();
        CacheComponents();

        if(Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void CacheComponents()
    {
        PlayerPunchController = GetComponent<PlayerPunchController>().Init(this);
        PlayerAnimationEventHandler = GetComponentInChildren<PlayerAnimationEventHandler>().Init(this);
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
    }
}
