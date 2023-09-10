using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Lumin;

public class GridCell : MonoBehaviour, IHaveColor
{
    public ColorData ColorData {get; private set;}

    private Renderer _renderer;

    public ColorType ColorType => ColorData.ColorType;

    public ColorBombGridCell ColorBombGridCell {get; private set;}

    public event UnityAction<CharacterBase> OnTriggered;
    private readonly int colorID = Shader.PropertyToID("_Color");

    private void Awake() 
    {
        ColorBombGridCell = GetComponent<ColorBombGridCell>();    
    }

    private void Start() 
    {
        _renderer = GetComponentInChildren<Renderer>();
        this.DelayOneFrame(()=> {
            MaterialPropertyBlock.SetColor(colorID, ColorData.Color);
            _renderer.SetPropertyBlock(MaterialPropertyBlock);
    }
    );
    }
    
    public void SetColorData(ColorData colorData)
    {
        ColorData = new(colorData.Color, colorData.ColorType);
    }

    public void ChangeColor(ColorData colorData, Transform other)
    {
        if(this.ColorData == colorData)
            return;

        SetColorData(colorData);

        var direction = transform.position - other.transform.position;
        direction.y = 0f;

        StartCoroutine(ColorSequence());
        IEnumerator ColorSequence()
        {
            float elapsedTime = 0f;
            float changeTimer = 0.2f;
            Color startColor = MaterialPropertyBlock.GetColor(colorID);
            Color targetColor = ColorData.Color;
            
            while (elapsedTime < changeTimer)
            {
                elapsedTime += Time.deltaTime;
                Color colorValue = Color.Lerp(startColor, targetColor, elapsedTime / changeTimer);
                MaterialPropertyBlock.SetColor(colorID, colorValue);
                _renderer.SetPropertyBlock(MaterialPropertyBlock);
                yield return null;
            }
        }

        //_renderer.material.DOColor(ColorData.Color, 0.2f);
        //transform.DOComplete();

        transform.DOShakePosition(0.2f, direction.normalized * 0.4f, 10);
    }

    private MaterialPropertyBlock materialPropertyBlock;

    public MaterialPropertyBlock MaterialPropertyBlock
    {
        get
        {
            if (materialPropertyBlock == null)
                materialPropertyBlock = new MaterialPropertyBlock();

            return materialPropertyBlock;
        }
    }


    private void OnTriggerEnter(Collider other) 
    {
        transform.DOComplete();
        if(!other.TryGetComponent<CharacterBase>(out var player))
            return;

        if(player == Player.Instance)
            GameManager.PlayerPaintCount++;
        
        OnTriggered?.Invoke(player);
        
        ChangeColor(player.CharacterColor.ColorData, other.transform);
    }
}
