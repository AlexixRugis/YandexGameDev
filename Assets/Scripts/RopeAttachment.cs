using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class RopeAttachment : MonoBehaviour
{
    public Rope Rope { get; private set; }

    private HingeJoint2D _joint;
    private Rope _lastRope = null;

    private void Awake()
    {
        _joint = GetComponent<HingeJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Rope != null) return;
        
        if (collider.transform.TryGetComponent(out Rope newRope))
        {
            AttachTo(newRope);
        }
    }

    public void AttachTo(Rope newRope)
    {
        if (Rope == null && newRope != _lastRope)
        {
            Rope = newRope;
            _joint.connectedBody = newRope.Rigidbody;
            _joint.enabled = true;
        }
    }

    public void Detach()
    {
        if (Rope == null) return;

        _joint.enabled = false;
        _lastRope = Rope;
        Rope = null;
    }
}
