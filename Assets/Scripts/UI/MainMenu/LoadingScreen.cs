using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image fillImage;

    [SerializeField] private TMP_Text loadingText;

    private void Awake()
    {
        panel.SetActive(false);
        fillImage.fillAmount = 0f;
    }

    
    private void Start() 
    {
        MainMenu.OnPlayButtonPressed += LoadScene;    
    }

    private void OnDestroy() 
    {
        MainMenu.OnPlayButtonPressed -= LoadScene;    
    }

    public void LoadScene()
    {
        StartCoroutine(Sequence(1)); //can set it by parameter

        LoadingTextSequence();
    }
    private void LoadingTextSequence()
    {
        string[] texts = new string[3] {"Loading.", "Loading..", "Loading..."};
        StartCoroutine(Sequence());

        IEnumerator Sequence()
        {
            int i = 0;
            while(true)
            {
                loadingText.text = texts[i];
                yield return new WaitForSeconds(0.25f);

                _ = i == 2 ? i = 0 : i++;
            } 
        }
    }

    private IEnumerator Sequence(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        panel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            fillImage.fillAmount = progress;

            yield return null;
        }
    }
}
