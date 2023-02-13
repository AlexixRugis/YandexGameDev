using UnityEngine;

public class AttachRope : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<HingeJoint2D>()) return;

        var rope = collision.transform.GetComponent<RopeAttachPoint>();
        
        if (rope)
        {
            var joint = gameObject.AddComponent<HingeJoint2D>();
            joint.connectedBody = rope.Rigidbody;
        }
    }
}
