using System.Collections.Generic;

namespace Asteroids.Model
{
    public class ModelGroup
    {
        private readonly List<Transformable> _models = new List<Transformable>();

        public void RegisterModel(Transformable model)
        {
            if (_models.Contains(model)) return;

            _models.Add(model);
        }

        public void UnregisterModel(Transformable model)
        {
            _models.Remove(model);
        }

        public Transformable GetNearestTo(Transformable model)
        {
            float minSqrDistance = float.MaxValue;
            Transformable nearest = null;

            _models.RemoveAll(x => x.Destroyed);
            for (int i = 0; i < _models.Count; i++)
            {
                Transformable current = _models[i];
                if (current != model)
                {
                    float sqrDistance = (current.Position - model.Position).sqrMagnitude;
                    if (sqrDistance < minSqrDistance)
                    {
                        minSqrDistance = sqrDistance;
                        nearest = current;
                    }
                }
            }

            return nearest;
        }
    }
}
