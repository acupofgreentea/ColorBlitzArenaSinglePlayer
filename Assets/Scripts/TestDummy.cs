using UnityEngine;

public class TestDummy : MonoBehaviour, IPunchable
{
    public void HandleGetPunch()
    {
        Debug.LogError("Damaged");
    }
}
