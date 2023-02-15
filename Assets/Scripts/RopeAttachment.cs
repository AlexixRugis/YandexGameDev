using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class RopeAttachment : MonoBehaviour
{
    public Rope Rope { get; private set; }

    private Rope _lastRope = null;

    private HingeJoint2D _joint;

    private void Awake()
    {
        _joint = GetComponent<HingeJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Rope != null) return;
        
        if (collider.transform.TryGetComponent(out Rope newRope))
        {
            if (newRope != _lastRope)
            {
                Rope = newRope;
                _joint.connectedBody = newRope.Rigidbody;
                _joint.enabled = true;
            }
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
