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

    [SerializeField] private TMP_Text gridPaintedCountText; 
    [SerializeField] private TMP_Text bluePaintedCountText; 
    [SerializeField] private TMP_Text yellowPaintedCountText; 
    [SerializeField] private TMP_Text redPaintedCountText; 
    [SerializeField] private TMP_Text greenPaintedCountText; 

    public static event UnityAction OnPlayButtonPressed;

    private void Start() 
    {
        playButton.onClick.AddListener(PlayButtonPressed);    
        exitButton.onClick.AddListener(ExitButtonPressed); 
        playerNameField.onEndEdit.AddListener(SetPlayerName);   

        SetTexts();
    }

    private void SetTexts()
    {
        var data = PlayerDataHelper.Instance.Load();
        gridPaintedCountText.text = data.GridPaintedCount.ToString();
        playerNameField.placeholder.GetComponent<TMP_Text>().text = String.IsNullOrEmpty(data.PlayerName) ? "Enter name..." : data.PlayerName;

        bluePaintedCountText.text =   data.BluePaintCount.ToString();
        yellowPaintedCountText.text = data.YellowPaintCount.ToString();
        redPaintedCountText.text =    data.RedPaintCount.ToString();
        greenPaintedCountText.text =  data.GreenPaintCount.ToString();
    }

    private void PlayButtonPressed()
    {
        OnPlayButtonPressed?.Invoke();
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
