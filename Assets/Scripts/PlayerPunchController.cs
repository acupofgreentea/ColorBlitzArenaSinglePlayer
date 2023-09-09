using UnityEngine;
using UnityEngine.Events;

public class PlayerPunchController : MonoBehaviour, IPunchable, IPunchUser
{
    [SerializeField] private float stunDuration = 1.25f;

    private Player player;

    public event UnityAction OnPunchUse;
    public event UnityAction<float> OnGetPunched;

    public PlayerPunchController Init(Player player)
    {
        this.player = player;
        return this;
    }

    private void Start() 
    {
        player.PlayerInputHandler.OnPunch += HandleOnPunch;
    }

    private void HandleOnPunch()
    {
        Punch();
    }

    public void HandleGetPunch()
    {
        OnGetPunched?.Invoke(stunDuration);
    }

    //animation event trigger
    public void HitPunch()
    {
        var colliders = Physics.OverlapSphere(transform.position, 3f);
        
        foreach (var col in colliders)
        {
            if(col.TryGetComponent<IPunchable>(out var punchable))
            {
                if(col.gameObject.GetInstanceID() == this.gameObject.GetInstanceID())
                    continue;
                
                punchable.HandleGetPunch();
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position, 3f);    
    }
    public void Punch()
    {
        OnPunchUse?.Invoke();
    }

    private IPunchable currentPunchable;
}

public interface IPunchable
{
    void HandleGetPunch();
}

public interface IPunchUser
{
    void Punch();
}
