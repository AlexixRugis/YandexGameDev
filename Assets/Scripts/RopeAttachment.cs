using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class RopeAttachment : MonoBehaviour
{
    /// <summary>
    /// The rope to which the object is attached, null if the object is not attached to the rope
    /// </summary>
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

    /// <summary>
    /// Attaches an object to a rope
    /// 
    /// Can only be used if the object is not attached to the rope
    /// </summary>
    /// <param name="newRope">The rope to attach the object to</param>
    public void AttachTo(Rope newRope)
    {
        if (Rope == null && newRope != _lastRope)
        {
            Rope = newRope;
            _joint.connectedBody = newRope.Rigidbody;
            _joint.enabled = true;
        }
    }


    /// <summary>
    /// Detaches an object from the rope
    /// 
    /// Can be used when the object is attached to the rope
    /// </summary>
    public void Detach()
    {
        if (Rope == null) return;

        _joint.enabled = false;
        _lastRope = Rope;
        Rope = null;
    }
}
