using System.Collections;
using TMPro;
using UnityEngine;
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
        SceneManagement.OnLoadSceneAsync += HandleLoadSceneAsync;
    }

    private void HandleLoadSceneAsync(AsyncOperation op)
    {
        StartCoroutine(Sequence(op)); 

        LoadingTextSequence();
    }

    private void OnDestroy() 
    {
        SceneManagement.OnLoadSceneAsync -= HandleLoadSceneAsync;
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

    private IEnumerator Sequence(AsyncOperation op)
    {
        panel.SetActive(true);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);

            fillImage.fillAmount = progress;

            yield return null;
        }
        panel.SetActive(false);
    }
}
