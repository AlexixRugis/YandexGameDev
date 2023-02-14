using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rope : MonoBehaviour
{
    [SerializeField] private Transform _attachmentPoint;

    public Vector3 AttachmentPoint => _attachmentPoint.transform.position;
    public Rigidbody2D Rigidbody => GetComponent<Rigidbody2D>();
}
