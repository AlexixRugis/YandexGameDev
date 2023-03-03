using Asteroids.Model;
using UnityEngine;

public class EnemyPresenter : Presenter
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{name} {collision.transform.name}");
        if (collision.gameObject.CompareTag(Config.BulletsTag)
            || collision.gameObject.CompareTag(Config.EnemyTag))
        {
            DestroyCompose();
        }
    }
}
