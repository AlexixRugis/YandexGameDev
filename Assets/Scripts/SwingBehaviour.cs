using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SwingBehaviour : MonoBehaviour
{
    [SerializeField] private float _force;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(InputConstants.SwingButton))
        {
            if (GetComponent<HingeJoint2D>())
            {
                _rigidbody.AddForce(Vector2.right * _force, ForceMode2D.Impulse);
            }
        }
    }
}
