using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _jumpSound;

    public event Action OnDied;

    private Vector3 _defailtPosition;
    private bool _onGround = true;
    private bool _isDied = false;
    private bool _paused = false;

    private void Start()
    {
        _defailtPosition = transform.position;
    }


    private void Update()
    {
        if (!_isDied && _onGround && Input.GetButton("Jump"))
        {
            StartCoroutine(JumpRoutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_isDied && collider.TryGetComponent(out Obstacle obstacle))
        {
            HandleObjstacle();
        }
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    }

    private void HandleObjstacle()
    {
        _animator.SetTrigger("Died");
        _isDied = true;
        OnDied?.Invoke();
    }

    private IEnumerator JumpRoutine()
    {
        float duration = _jumpCurve.keys[_jumpCurve.length - 1].time;

        _onGround = false;
        _audio.PlayOneShot(_jumpSound);
        _animator.SetTrigger("Jump");
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            if (_paused) yield return new WaitUntil(() => !_paused);

            transform.position = _defailtPosition + Vector3.up * _jumpCurve.Evaluate(time);
            yield return null;
        }

        _onGround = true;
    }
}
