using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    public static event UnityAction OnPlayButtonPressed;

    private void Start() 
    {
        playButton.onClick.AddListener(PlayButtonPressed);    
        exitButton.onClick.AddListener(ExitButtonPressed);    
    }

    private void PlayButtonPressed()
    {
        OnPlayButtonPressed?.Invoke();
    }

    private void ExitButtonPressed()
    {
        Application.Quit();
    }
}
