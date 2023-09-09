using UnityEngine.Events;

public class Player : CharacterBase, IPunchable, IPunchUser
{
    public new PlayerMovement CharacterMovement => base.CharacterMovement as PlayerMovement;
    public PlayerPunchController PlayerPunchController {get; private set;}
    public PlayerAnimationEventHandler PlayerAnimationEventHandler {get; private set;}
    public PlayerInputHandler PlayerInputHandler {get; private set;}

    public bool IsStunned {get; set;}

    public static Player Instance; 
    public event UnityAction OnPunchUse;
    public event UnityAction<float> OnGetPunched;
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
        PlayerPunchController = GetComponent<PlayerPunchController>().Init(this);
        PlayerAnimationEventHandler = GetComponentInChildren<PlayerAnimationEventHandler>().Init(this);
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
    }
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
