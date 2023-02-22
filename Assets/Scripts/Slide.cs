using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Slide : MonoBehaviour
{
    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;

    private readonly RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];

    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigidbody2d;

    private bool _grounded;
    private Vector2 _velocity;
    private Vector2 _groundNormal;
    private ContactFilter2D _contactFilter;
    
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();

        SetupContactFilter();
    }

    private void FixedUpdate()
    {
        UpdateVelocity();

        Vector2 deltaPosition = _velocity * Time.fixedDeltaTime;
        ApplyMovement(deltaPosition);

        HandleJump();
    }

    private void SetupContactFilter()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void UpdateVelocity()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.fixedDeltaTime;
        _velocity.x = _groundNormal.x * _speed;
    }

    private void ApplyMovement(Vector2 deltaPosition)
    {
        _grounded = false;

        Vector2 alongGroundDirection = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 moveAlongGround = alongGroundDirection * deltaPosition.x;
        _rigidbody2d.position += CalculatePhysicalMovement(moveAlongGround);

        Vector2 moveVertical = Vector2.up * deltaPosition.y;
        _rigidbody2d.position += CalculatePhysicalMovement(moveVertical);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown(InputConstants.JumpButtonName) && _grounded)
        {
            _velocity.y += _jumpForce;
            _grounded = false;
        }
    }

    private Vector2 CalculatePhysicalMovement(Vector2 move)
    {
        float distance = move.magnitude;

        if (distance < MinMoveDistance) return Vector2.zero;
         
        int count = _rigidbody2d.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);
        for (int i = 0; i < count; i++)
        {
            Vector2 currentNormal = _hitBuffer[i].normal;
            if (currentNormal.y > _minGroundNormalY)
            {
                _grounded = true;
                _groundNormal = currentNormal;
            }

            ApplyProjectionToVelocity(currentNormal);

            float modifiedDistance = _hitBuffer[i].distance - ShellRadius;
            distance = modifiedDistance < distance ? modifiedDistance : distance;
        }

        return move.normalized * distance;
    }

    private void ApplyProjectionToVelocity(Vector2 currentNormal)
    {
        float projection = Vector2.Dot(_velocity, currentNormal);
        if (projection < 0)
        {
            _velocity = _velocity - projection * currentNormal;
        }
    }
}