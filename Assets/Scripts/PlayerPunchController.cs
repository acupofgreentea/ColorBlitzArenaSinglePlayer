using UnityEngine;

public class PlayerPunchController : MonoBehaviour
{
    [SerializeField] private float stunDuration = 1.25f;

    private Player player;

    public PlayerPunchController Init(Player player)
    {
        this.player = player;
        return this;
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

                if(punchable.IsStunned)
                    return;
                
                punchable.HandleGetPunch(stunDuration);
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position, 3f);    
    }
}

public interface IPunchable
{
    void HandleGetPunch(float stunDuration);

    bool IsStunned {get; }
}

public interface IPunchUser
{
    void Punch();
}
