using Asteroids.Input;
using Asteroids.Model;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInputRouter
{
    private readonly ShipInput _input;
    private readonly bool _invertRotation;
    private readonly Ship _ship;

    private DefaultGun _firstGunSlot;
    private DefaultGun _secondGunSlot;

    public ShipInputRouter(Ship ship, bool invertRotation = false)
    {
        _input = new ShipInput();
        _invertRotation = invertRotation;
        _ship = ship;
    }

    public void OnEnable()
    {
        _input.Enable();
        _input.Ship.FirstSlotShoot.performed += OnFirstSlootShoot;
        _input.Ship.SecondSlotShoot.performed += OnSecondSlootShoot;
    }

    public void OnDisable()
    {
        _input.Disable();
        _input.Ship.FirstSlotShoot.performed -= OnFirstSlootShoot;
        _input.Ship.SecondSlotShoot.performed -= OnSecondSlootShoot;
    }

    public void Update()
    {
        if (MoveForwardPerformed())
            _ship.Accelerate(Time.deltaTime);
        else
            _ship.Slowdown(Time.deltaTime);

        TryRotate();
    }

    public ShipInputRouter BindGunToFirstSlot(DefaultGun gun)
    {
        _firstGunSlot = gun;
        return this;
    }

    public ShipInputRouter BindGunToSecondSlot(DefaultGun gun)
    {
        _secondGunSlot = gun;
        return this;
    }

    private bool MoveForwardPerformed()
    {
        return _input.Ship.MoveForward.phase == InputActionPhase.Performed;
    }

    private void OnFirstSlootShoot(InputAction.CallbackContext obj)
    {
        TryShoot(_firstGunSlot);
    }

    private void OnSecondSlootShoot(InputAction.CallbackContext obj)
    {
        TryShoot(_secondGunSlot);
    }

    private void TryShoot(DefaultGun gun)
    {
        if (gun.CanShoot())
            gun.Shoot();
    }

    private void TryRotate()
    {
        float direction = _input.Ship.Rotate.ReadValue<float>();

        if (_invertRotation)
            direction = -direction;

        if(direction != 0)
            _ship.Rotate(direction, Time.deltaTime);
    }
}
