using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rope : MonoBehaviour
{
    [Tooltip("The point of attachment of the rope to the base")]
    [SerializeField] private Transform _attachmentPoint;
    [Min(0.0001f)]
    [Tooltip("The overriden mass of Rigidbody2D component")]
    [SerializeField] private float _mass;

    public Vector3 AttachmentPoint => _attachmentPoint.transform.position;
    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.mass = _mass;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_attachmentPoint == null)
        {
            Debug.LogError($"Null referenced parameter {nameof(_attachmentPoint)} in {GetType().Name} of {gameObject.name}");
        }  
    }
#endif
}
