using UnityEngine;

/* Represents objects which are snapped to round positions during edit mode */
[ExecuteInEditMode]
public class GridObject : MonoBehaviour
{
    void Update()    
    {
        SnapToGrid();
    }

    private void SnapToGrid()
    {
        var gridPosition = GetGridPos();
        transform.position = 
            new Vector3(gridPosition.x, gridPosition.y, gridPosition.z);
    }

    public Vector3Int GetGridPos()
    {
        return new Vector3Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z)
        );
    }
}
