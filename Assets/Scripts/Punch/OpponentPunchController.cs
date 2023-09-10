using UnityEngine;

public class OpponentPunchController : CharacterPunchControllerBase
{
    public new OpponentPunchStatsSO PunchStatsSO => base.PunchStatsSO as OpponentPunchStatsSO;

    
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

    private void OnTriggerEnter(Collider other) 
    {
        if(CharacterBase.IsStunned)
            return;

        if(!other.TryGetComponent<IPunchable>(out var punchable))
            return;
        
        if(punchable.IsStunned)
            return;
        
        bool isPunchSuccess = PunchStatsSO.IsPunchSuccess();

        if(!isPunchSuccess)
            return;
        
        CharacterBase.Punch();
    }
}
