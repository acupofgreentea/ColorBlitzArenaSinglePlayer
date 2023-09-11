using TMPro;
using UnityEngine;

public class CharacterNamesUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] texts;

    private Camera cam;

    private void Awake() 
    {
        cam = Camera.main;
    }

    private void Start() 
    {
        GameManager.OnNamesSet += HandleNamesSet;
        SessionManager.OnSessionFinish += HandleSessionFinish;
    }

    private void HandleSessionFinish()
    {
        gameObject.SetActive(false);
    }

    private void HandleNamesSet(string[] names)
    {
        for (int i = 0; i < names.Length; i++)
        {
            var text = texts[i];
            var name = names[i];

            text.text = name;
        }
    }

    private void Update() 
    {
        for (int i = 0; i < texts.Length; i++)
        {
            var text = texts[i];
            var player = Managers.Instance.GameManager.GetPlayAtIndex(i);
            Vector3 position = cam.WorldToScreenPoint(player.NameTextPosition.position);
            text.transform.position = position;
        }  
    }

    private void OnDestroy()
    {
        GameManager.OnNamesSet -= HandleNamesSet;        
        SessionManager.OnSessionFinish -= HandleSessionFinish;
    }
}
