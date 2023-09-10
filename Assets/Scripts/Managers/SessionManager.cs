using UnityEngine;
using UnityEngine.Events;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 60f;
    private bool isSessionStarted = false;
    private bool isSessionFinished = false;
    public static UnityAction OnSessionStart;
    public static UnityAction OnSessionFinish;
    public static event UnityAction OnCountdownStart;

    public void StartSession()
    {
        OnSessionStart?.Invoke();
        isSessionStarted = true;
    }

    private void Start() 
    {
        OnCountdownStart?.Invoke();
    }

    private void Update() 
    {
        if(!isSessionStarted || isSessionFinished)
            return;
            
        if(gameTime <= 0f)
        {
            OnSessionFinish?.Invoke();
            isSessionFinished = true;
        }
        
        gameTime-= Time.deltaTime;    
    }

    private void OnGUI() 
    {
        float remaningTime = gameTime;
        remaningTime = Mathf.RoundToInt(remaningTime);
        GUI.Label(new Rect(150, 0, 20, 20), remaningTime.ToString());
    }
}
