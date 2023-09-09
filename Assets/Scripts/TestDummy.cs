using UnityEngine;

public class TestDummy : MonoBehaviour, IPunchable
{
    public bool IsStunned => throw new System.NotImplementedException();

    public void HandleGetPunch(float stunDuration)
    {
        Debug.LogError("Damaged");
    }
}
