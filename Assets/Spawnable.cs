using UnityEngine;

public class Spawnable : MonoBehaviour
{
    [SerializeField] private float _width;

    public float Width => _width;
    public float RightBorder => transform.position.x + _width / 2f;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + Vector3.left * (_width / 2f), 
            transform.position + Vector3.right * (_width / 2f));
    }
#endif
}
