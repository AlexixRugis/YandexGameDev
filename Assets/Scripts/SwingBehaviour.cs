using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(RopeAttachment))]
public class SwingBehaviour : MonoBehaviour
{
    [Tooltip("The force applied to the object for acceleration using Rigidbody")]
    [Min(0f)]
    [SerializeField] private float _force;
    [Tooltip("The maximum speed of the object at the lowest point of the trajectory")]
    [Min(0f)]
    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rigidbody;
    private RopeAttachment _attachment;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _attachment = GetComponent<RopeAttachment>();
    }

    private void FixedUpdate()
    {
        if (_attachment.Rope != null)
        {
            ApplySwing();
        }
    }

    private void ApplySwing()
    {
        if (_rigidbody.velocity.sqrMagnitude < _maxSpeed*_maxSpeed)
        {
            Vector2 force = CalculateSwingForce();

            _rigidbody.AddForce(force, ForceMode2D.Force);
        }
    }

    private Vector2 CalculateSwingForce()
    {
        float factor = 1f - Mathf.Clamp01(
            Mathf.Abs(transform.position.x - _attachment.Rope.AttachmentPoint.x) * 5f);

        Vector2 toAttachmentDirection = (_attachment.Rope.AttachmentPoint - transform.position).normalized;
        Vector3 forceDirection = new Vector2(toAttachmentDirection.y, -toAttachmentDirection.x);

        if (_rigidbody.velocity.x < 0)
        {
            forceDirection = -forceDirection;
        }

        return forceDirection * _force * factor;
    }
}
