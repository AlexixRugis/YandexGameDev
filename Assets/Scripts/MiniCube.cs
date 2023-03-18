using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class MiniCube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private int _coinsAmount;
    private Resources _resources;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _resources.CollectCoins(_coinsAmount, transform.position);
        Destroy(gameObject);
    }

    public void Init(Vector3 force, int coinsAmount, Resources resources)
    {
        _coinsAmount = coinsAmount;
        _resources = resources;

        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

}
