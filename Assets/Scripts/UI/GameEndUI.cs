using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button backtoMenuButton;

    public static event UnityAction OnPlayAgainPressed;
    private void Start() 
    {
        SessionManager.OnSessionFinish += HandleSessionFinish;
        playAgainButton.onClick.AddListener(HandleRetryButton);
        backtoMenuButton.onClick.AddListener(HandleBackToMenuButton);
        gameEndPanel.SetActive(false);
    }

    private void HandleBackToMenuButton()
    {
        SceneManagement.Instance.LoadSceneAsync(0);
    }

    private void HandleRetryButton()
    {
        OnPlayAgainPressed?.Invoke();
        SceneManagement.Instance.LoadSceneAsync(1);
    }

    private void HandleSessionFinish()
    {
        gameEndPanel.transform.localScale = Vector3.zero;
        gameEndPanel.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InExpo);
        gameEndPanel.SetActive(true);
    }

    private void OnDestroy() 
    {
        SessionManager.OnSessionFinish -= HandleSessionFinish;    
    }
}
