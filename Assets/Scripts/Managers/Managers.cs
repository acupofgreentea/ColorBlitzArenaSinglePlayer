using UnityEngine;

public class Managers : MonoBehaviour
{
    public GameManager GameManager {get; private set;}
    public GridManager GridManager {get; private set;}

    public static Managers Instance {get; private set;}
    private void Awake() 
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        CacheManagers();
    }

    private void CacheManagers()
    {
        GameManager = GetComponentInChildren<GameManager>();
        GridManager = GetComponentInChildren<GridManager>();
    }
}
