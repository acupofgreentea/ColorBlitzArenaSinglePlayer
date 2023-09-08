using System.Collections;
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

    }

    public void HandleGetPunch()
    {
        OnGetPunched?.Invoke(stunDuration);
    }

    //animation event trigger
    public void HitPunch()
    {
        currentPunchable.HandleGetPunch();
    }

    public void Punch()
    {
        OnPunchUse?.Invoke();
    }

    private IPunchable currentPunchable;

    private void OnTriggerEnter(Collider other) 
    {
        if(!other.TryGetComponent(out IPunchable punchable) || other == this)
            return;

        currentPunchable = punchable;
    }
}

public interface IPunchable
{
    void HandleGetPunch();
}

public interface IPunchUser
{
    void Punch();
}
