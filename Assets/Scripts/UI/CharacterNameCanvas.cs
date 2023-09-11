using TMPro;
using UnityEngine;

public class CharacterNameCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text[] texts;

    private Camera cam;

    private void Awake() 
    {
        cam = Camera.main;
    }

    private void Start() 
    {
        Managers.Instance.GameManager.OnNamesSet += HandleNamesSet;
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
}
