using UnityEngine;

public class RopeAttachment : MonoBehaviour
{
    public Rope Rope { get; private set; }
    public bool IsAttached => Rope;

    private Rope _lastRope = null;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsAttached) return;

        var rope = collider.transform.GetComponent<Rope>();
        
        if (rope && rope != _lastRope)
        {
            var joint = gameObject.AddComponent<HingeJoint2D>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedBody = rope.Rigidbody;
            joint.connectedAnchor = Vector2.zero;
            Rope = rope;
        }
    }

    public void Detach()
    {
        if (!IsAttached) return; 

        var joint = GetComponent<HingeJoint2D>();
        if (joint)
        {
            Destroy(joint);

            _lastRope = Rope;
            Rope = null;
        }
    }
}
