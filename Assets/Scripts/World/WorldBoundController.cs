using Infrastructure;
using JetBrains.Annotations;
using UnityEngine;

namespace World
{
    [UsedImplicitly]
    public class WorldBoundController
    {
        private readonly IArrowFactory _factory;
        private readonly WorldBound _view;

        public WorldBoundController(IArrowFactory factory, WorldBound view)
        {
            _factory = factory;
            _view = view;
            _view.SetController(this);
        }

        public void ProcessOutOfBound(Rigidbody2D player)
        {
            Vector3 position = player.transform.position + Cast(player.velocity).normalized * _view.SpawnArrowDistance;
            IHoming homing = _factory.Create(position, Quaternion.identity);
            homing.SetAim(player.transform);
        }

        private Vector3 Cast(Vector2 obj)
        {
            return new Vector3(obj.x, obj.y, 0);
        }
    }
}