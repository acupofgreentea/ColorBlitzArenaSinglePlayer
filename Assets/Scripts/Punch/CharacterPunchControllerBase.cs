using UnityEngine;

public abstract class CharacterPunchControllerBase : MonoBehaviour
{
    [field: SerializeField] public PunchStatsSO PunchStatsSO { get; private set;}

    [SerializeField] protected Transform punchPivot;

    public CharacterBase CharacterBase {get; private set;}

    public CharacterPunchControllerBase Init(CharacterBase characterBase)
    {
        this.CharacterBase = characterBase;
        return this;
    }

    public abstract void HitPunch();

    
#if UNITY_EDITOR
    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(punchPivot.position, PunchStatsSO.PunchRange);    
    }
#endif
}
