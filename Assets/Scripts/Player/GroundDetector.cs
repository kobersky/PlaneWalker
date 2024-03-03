using UnityEngine;

/* Detects if touching a layer called "Ground" */
public class GroundDetector : MonoBehaviour
{
    public bool IsGrounded;

    private const float SPHERE_RADIUS = 0.2f;
    private void FixedUpdate()
    {
        var layer = 1 << LayerMask.NameToLayer(LayerTypes.GROUND);
        IsGrounded =  Physics.OverlapSphere(transform.position, SPHERE_RADIUS, layer).Length > 0;
    }
}
