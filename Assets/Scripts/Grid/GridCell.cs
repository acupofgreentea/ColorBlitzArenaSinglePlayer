using UnityEngine;
using DG.Tweening;

public class GridCell : MonoBehaviour, IHaveColor
{
    public ColorData ColorData {get; private set;}

    private Renderer _renderer;

    public ColorType ColorType => ColorData.ColorType;

    private void Start() 
    {
        _renderer = GetComponent<Renderer>();
        this.DelayOneFrame(()=> _renderer.material.color = ColorData.Color);
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
        _renderer.material.DOColor(ColorData.Color, 0.2f);
        transform.DOComplete();
        transform.DOShakePosition(0.2f, direction.normalized * 0.4f, 10);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!other.TryGetComponent<CharacterColor>(out var player))
            return;
            
        ChangeColor(player.ColorData, other.transform);
    }
}
