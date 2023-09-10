using UnityEngine;

public class PlayerPunchController : CharacterPunchControllerBase
{
    //animation event trigger
    public override void HitPunch()
    {
        var colliders = Physics.OverlapSphere(punchPivot.position, PunchStatsSO.PunchRange);
        
        foreach (var col in colliders)
        {
            if(col.TryGetComponent<IPunchable>(out var punchable))
            {
                if(col.gameObject.GetInstanceID() == this.gameObject.GetInstanceID())
                    continue;

                if(punchable.IsStunned)
                    return;
                
                punchable.HandleGetPunch(PunchStatsSO.StunDuration);
                break;
            }
        }
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
