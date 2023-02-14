using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(RopeAttachment))]
public class SwingBehaviour : MonoBehaviour
{
    [SerializeField] private float _force;
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
        if (_attachment.IsAttached)
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
        Vector2 toAttachmentDirection = (_attachment.Rope.AttachmentPoint - transform.position).normalized;
        Vector3 perpendicularDirection = new Vector2(toAttachmentDirection.y, -toAttachmentDirection.x);

        float factor = 1f - Mathf.Clamp01(
            Mathf.Abs(transform.position.x - _attachment.Rope.AttachmentPoint.x) * 5f);

        if (_rigidbody.velocity.x >= 0)
        {
            return perpendicularDirection * _force * factor;
        }
        else
        {
            return -perpendicularDirection * _force * factor;
        }
    }
}
