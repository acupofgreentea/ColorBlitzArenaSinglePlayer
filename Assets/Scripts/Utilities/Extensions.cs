using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class Extensions 
{
    public static Vector3 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, 0f, v.y);
    }

    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }

    public static void DelayOneFrame(this MonoBehaviour monoBehaviour, UnityAction action)
    {
        monoBehaviour.StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return null;
            
            action?.Invoke();
        }
    }
}
