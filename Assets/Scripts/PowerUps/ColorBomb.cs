using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBomb : MonoBehaviour, IPowerUp
{
    [SerializeField] private float radius;

    private List<GridCell> gridsInRange;

    private void Awake()
    {
        startPosY = transform.position.y;
    }

    private IEnumerator Start() 
    {
        yield return null;  
        SetGridsInRange();  
    }

    private float startPosY;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startPosY+ Mathf.Sin(Time.time), transform.position.z);
    }

    private void SetGridsInRange()
    {
        gridsInRange = new();
        gridsInRange = Managers.Instance.GridManager.GetCellsInRange(transform, radius);
    }

    public void Activate()
    {
        StartCoroutine(Sequence());
        IEnumerator Sequence()
        {
            foreach (var item in gridsInRange)
            {
                item.ChangeColor(interactedPlayerColorData, transform);
                yield return new WaitForSeconds(0.01f);
            }
            
            Destroy(gameObject);
        }
    }

    private ColorData interactedPlayerColorData;
    private void OnTriggerEnter(Collider other) 
    {
        if(!other.TryGetComponent(out CharacterColor player))
            return;

        interactedPlayerColorData = player.ColorData;
        Activate();
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position, radius);    
    }
}
