using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ColorBombGridCell : MonoBehaviour
{
    private GridCell gridCell;
    public bool IsActivated {get; private set;}

    [SerializeField] private Renderer glowEffectCube;

    private void Awake() 
    {
        gridCell = GetComponent<GridCell>();    
    }

    private void Start() 
    {
        gridCell.OnTriggered += HandleTriggered;
        glowEffectCube.gameObject.SetActive(false);
    }

    private void HandleTriggered(CharacterBase player)
    {
        Deactivate();
    }

    public void Activate()
    {        
        IsActivated = true;
        glowEffectCube.gameObject.SetActive(true);
        glowEffectCube.transform.DOScale(1.25f, 0.8f).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            float elapsedTime = 0f;
            float changeRate = 0.5f;
            Color stepColor = Color.red;
            Color lastColor = glowEffectCube.material.color;

            while(IsActivated)
            {   
                elapsedTime += Time.deltaTime;

                if(elapsedTime > changeRate)
                {
                    elapsedTime = 0f;
                    stepColor = new Color(Random.value, Random.value, Random.value);
                    lastColor = glowEffectCube.material.color;
                } 

                var colorValue = Color.Lerp(lastColor, stepColor, elapsedTime / changeRate);
                glowEffectCube.material.color = colorValue;

                yield return null;
            }
        }
    }

    private void OnDestroy() 
    {
        glowEffectCube.transform.DOKill();    
    }

    public void Deactivate()
    {
        if(!IsActivated)
            return;

        glowEffectCube.gameObject.SetActive(false);
        transform.DOKill();
        
        IsActivated = false;
    }
}
