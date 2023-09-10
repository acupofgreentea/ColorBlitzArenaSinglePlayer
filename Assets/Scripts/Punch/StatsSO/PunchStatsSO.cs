using UnityEngine;

public abstract class PunchStatsSO : ScriptableObject
{
    [field: SerializeField] public float StunDuration {get; private set;} = 1.75f;

    [field: SerializeField] public float PunchRange {get; private set;} = 3f;
}
