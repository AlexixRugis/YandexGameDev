using System;
using UnityEngine;

namespace Asteroids.Model
{
    public class Nlo : Enemy
    {
        private readonly float _speed;
        private readonly ModelGroup _targets;

        private Transformable _target;

        public Nlo(ModelGroup targets, Vector2 position, float speed) : base(position, 0)
        {
            _targets = targets;
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            if (_target == null || _target.Destroyed)
            {
                _target = _targets.GetNearestTo(this);
            }
            else
            {
                Debug.Log(_target.Destroyed);
                Vector2 nextPosition = Vector2.MoveTowards(Position, _target.Position, _speed * deltaTime);
                MoveTo(nextPosition);
                LookAt(_target.Position);
            }
        }

        private void LookAt(Vector2 point)
        {
            Rotate(Vector2.SignedAngle(Quaternion.Euler(0, 0, Rotation) * Vector3.up, (Position - point)));
        }
    }
}
