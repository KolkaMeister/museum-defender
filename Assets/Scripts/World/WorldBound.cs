using UnityEngine;

namespace World
{
    public class WorldBound : MonoBehaviour
    {
        public float SpawnArrowDistance;
        
        private WorldBoundController _controller;

        public void SetController(WorldBoundController controller)
        {
            _controller = controller;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerInputController player))
            {
                _controller.ProcessOutOfBound(other.attachedRigidbody); 
            }
        }
    }
}