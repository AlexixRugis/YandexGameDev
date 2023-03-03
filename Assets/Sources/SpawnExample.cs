using UnityEngine;
using Asteroids.Model;

public class SpawnExample : MonoBehaviour
{
    [SerializeField] private float _secondsPerIndex = 1f;
    [SerializeField] private PresentersFactory _factory;
    [SerializeField] private Root _init;

    private int _index;

    private void Update()
    {
        int newIndex = (int)(Time.time / _secondsPerIndex);

        if(newIndex > _index)
        {
            _index = newIndex;
            OnTick();
        }
    }

    private void OnTick()
    {
        float chance = Random.Range(0, 100);

        if (chance < 40)
        {
            Nlo model = new Nlo(_init.EnemyGroup2, GetRandomPositionLeft(), Config.NloSpeed);
            _init.EnemyGroup1.RegisterModel(model);
            _factory.CreateNlo1(model);
        }
        else if (chance < 80)
        {
            Nlo model = new Nlo(_init.EnemyGroup1, GetRandomPositionRight(), Config.NloSpeed);
            _init.EnemyGroup2.RegisterModel(model);
            _factory.CreateNlo2(model);
        }
        else
        {
            Vector2 position = GetRandomPositionOutsideScreen();
            Vector2 direction = GetDirectionThroughtScreen(position);

            _factory.CreateAsteroid(new Asteroid(position, direction, Config.AsteroidSpeed));
        }
    }

    private Vector2 GetRandomPositionRight()
    {
        return new Vector2(1f, Random.Range(0f, 1f));
    }

    private Vector2 GetRandomPositionLeft()
    {
        return new Vector2(0f, Random.Range(0f, 1f));
    }

    private Vector2 GetRandomPositionOutsideScreen()
    {
        return Random.insideUnitCircle.normalized + new Vector2(0.5F, 0.5F);
    }

    private static Vector2 GetDirectionThroughtScreen(Vector2 postion)
    {
        return (new Vector2(Random.value, Random.value) - postion).normalized;
    }
}
