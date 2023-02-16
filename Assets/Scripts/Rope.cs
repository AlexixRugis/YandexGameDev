using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rope : MonoBehaviour
{
    [SerializeField] private Transform _attachmentPoint;
    [SerializeField] private float _mass;

    public Vector3 AttachmentPoint => _attachmentPoint.transform.position;
    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.mass = _mass;
    }
}
