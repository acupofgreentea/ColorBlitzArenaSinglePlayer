using UnityEngine;

[CreateAssetMenu(menuName = "PunchStats/Opponent")]
public class OpponentPunchStatsSO : PunchStatsSO
{
    [field: SerializeField, Range(0, 100)] public float PunchChance {get; private set;} = 30f;
    [SerializeField, TextArea(2, 3)] public string punchChanceDescp;

    public bool IsPunchSuccess()
    {
        int randomChance = Random.Range(0, 100);
        return PunchChance > randomChance;
    } 
}
