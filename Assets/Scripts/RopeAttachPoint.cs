using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RopeAttachPoint : MonoBehaviour
{
    public Rigidbody2D Rigidbody => GetComponent<Rigidbody2D>();
}
