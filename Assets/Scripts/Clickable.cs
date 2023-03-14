using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Clickable : MonoBehaviour
{

    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private float _scaleTime = 0.25f;
    [SerializeField] private HitEffect _hitEffectPrefab;
    [SerializeField] private Resources _resources;
    [SerializeField] private MiniCube _miniCubePrefab;
    [SerializeField] private float _minicubeLaunchForce;

    private int _coinsPerClick = 1;

    // Метод вызывается из Interaction при клике на объект
    public void Hit()
    {
        HitEffect hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
        hitEffect.Init(_coinsPerClick);
        MiniCube miniCube = Instantiate(_miniCubePrefab, transform.position, Quaternion.identity);
        float alpha = Random.Range(135f, 315f) * Mathf.Deg2Rad;
        Vector3 forceDirection = new Vector3(Mathf.Cos(alpha), 0.5f, Mathf.Sin(alpha));
        miniCube.Init(forceDirection * _minicubeLaunchForce, _coinsPerClick, _resources);

        StartCoroutine(HitAnimation());
    }

    // Анимация колебания куба
    private IEnumerator HitAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / _scaleTime)
        {
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    // Этот метод увеличивает количество монет, получаемой при клике
    public void AddCoinsPerClick(int value)
    {
        _coinsPerClick += value;
    }

}
