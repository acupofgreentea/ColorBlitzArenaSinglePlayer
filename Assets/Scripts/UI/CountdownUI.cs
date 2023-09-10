using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private Animator countdownTextAnim;
    private readonly int changeHashKey = Animator.StringToHash("Change");

    [SerializeField, Tooltip("Only session manager will subscribe to invoke sessionstart event")] 
    private UnityEvent OnCountdownComplete;

    private void CountdownTextSequence()
    {
        StartCoroutine(Sequence());

        IEnumerator Sequence()
        {
            for (int i = 3; i >= 1 ; i--)
            {
                countdownText.text = i.ToString();
                countdownTextAnim.SetTrigger(changeHashKey);
                yield return new WaitForSeconds(1.25f);
            }
            
            countdownText.gameObject.SetActive(false);
            OnCountdownComplete?.Invoke();
        }

    }

    private void Awake() 
    {
        SessionManager.OnCountdownStart += CountdownTextSequence;    
    }
}
