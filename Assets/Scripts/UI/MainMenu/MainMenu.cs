using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_InputField playerNameField;

    public static event UnityAction OnPlayButtonPressed;

    private void Start() 
    {
        playButton.onClick.AddListener(PlayButtonPressed);    
        exitButton.onClick.AddListener(ExitButtonPressed); 
        playerNameField.onEndEdit.AddListener(SetPlayerName);   

        SetNameInputFieldText();
    }
    
    private void SetNameInputFieldText()
    {
        var data = PlayerDataHelper.Instance.Load();
        playerNameField.placeholder.GetComponent<TMP_Text>().text = String.IsNullOrEmpty(data.PlayerName) ? "Enter name..." : data.PlayerName;
    }

    private void PlayButtonPressed()
    {
        OnPlayButtonPressed?.Invoke();
        SceneManagement.Instance.LoadSceneAsync(1);//gamescene
    }

    private void ExitButtonPressed()
    {
        Application.Quit();
    }
    
    public void SetPlayerName(string name)
    {
        PlayerDataHelper.Instance.playerData.PlayerName = name;
        PlayerDataHelper.Instance.Save();
        playerNameField.placeholder.GetComponent<TMP_Text>().text = PlayerDataHelper.Instance.playerData.PlayerName;
    }
}
