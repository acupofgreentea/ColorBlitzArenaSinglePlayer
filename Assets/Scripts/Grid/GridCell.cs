using UnityEngine;
using DG.Tweening;

public class GridCell : MonoBehaviour
{
    public ColorType ColorType {get; set;}
    
    private Renderer _renderer;

    [SerializeField] private PlayerColorConfig colorConfig;

    private void Awake() 
    {
        _renderer = GetComponent<Renderer>();
        ColorType = colorConfig.ColorType;         
        _renderer.material.color = colorConfig.Color;
    }

    public void ChangeColor(PlayerColorConfig ColorConfig, Transform other)
    {
        if(colorConfig == ColorConfig)
            return;

        colorConfig = ColorConfig;

        ColorType = colorConfig.ColorType;

        var direction = transform.position - other.transform.position;
        direction.y = 0f;
        _renderer.material.DOColor(colorConfig.Color, 0.2f);
        transform.DOComplete();
        transform.DOShakePosition(0.2f, direction.normalized * 0.4f, 10);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!other.TryGetComponent<PlayerColor>(out var player))
            return;
            
        ChangeColor(player.ColorConfig, other.transform);
    }
}
