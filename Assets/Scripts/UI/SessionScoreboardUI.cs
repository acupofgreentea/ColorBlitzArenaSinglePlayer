using System;
using System.Collections.Generic;
using UnityEngine;

public class SessionScoreboardUI : MonoBehaviour
{
    [SerializeField] private List<ScoreboardElementUI> scoreboardElementUIs;
    
    public static event Func<List<(ColorType, int)>> GetScoreboardPercentages;
    public static event Func<ColorType, string> GetCharacterNames;
    public static event Func<ColorType, Color> GetCharacterColor;

    private void Start()
    {
        SessionManager.OnSessionFinish += HandleSessionFinish;
        GameEndUI.OnPlayAgainPressed += HandlePlayAgainPressed;
    }

    private void HandlePlayAgainPressed()
    {
        gameObject.SetActive(false);
    }

    private void HandleSessionFinish()
    {   
        var percentages = GetScoreboardPercentages?.Invoke();
        int count = Managers.Instance.GameManager.PlayerCount;

        for (int i = 0; i < count; i++)
        {
            ScoreboardElementUI scoreboardElementUI = scoreboardElementUIs[i];
            var percentage = percentages[i];

            string name = GetCharacterNames?.Invoke(percentage.Item1);
            Color color = (Color)GetCharacterColor?.Invoke(percentage.Item1);

            scoreboardElementUI.SetElementUI(percentage.Item2, name, color);
        }
    }

    private void OnDestroy()
    {
        SessionManager.OnSessionFinish -= HandleSessionFinish;
        GameEndUI.OnPlayAgainPressed -= HandlePlayAgainPressed;
    }
}
