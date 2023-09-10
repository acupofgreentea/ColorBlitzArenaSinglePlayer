public class Player : CharacterBase, IPunchable, IPunchUser
{
    public new PlayerMovement CharacterMovement => base.CharacterMovement as PlayerMovement;
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

    private void Start() 
    {
        PlayerInputHandler.OnPunch += Punch;    
    }

    private void CacheComponents()
    {
        PlayerAnimationEventHandler = GetComponentInChildren<PlayerAnimationEventHandler>().Init(this);
        PlayerInputHandler = GetComponent<PlayerInputHandler>().Init(this);
    }
}
