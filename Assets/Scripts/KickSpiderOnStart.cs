using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KickSpiderOnStart : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float _force;

    private void Start()
    {
        var rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.AddForce(Vector3.right * _force, ForceMode2D.Impulse);
    }
}
