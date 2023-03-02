using Asteroids.Model;
using UnityEngine;

public class ShipPresenter : Presenter
{
    private Root _init;

    public void Init(Root init)
    {
        _init = init;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Config.EnemyTag))
        {
            (Model as Ship).TakeDamage();
        }
    }
}
