using UnityEngine;

namespace World
{
    public class HomingArrow : Arrow, IHoming
    {
        public float Speed;

        private Transform _aim;
        private bool _canFollow;

        public void SetAim(Transform aim)
        {
            _aim = aim;
            SetColliderActive(true);
            _canFollow = true;
        }

        private void SetColliderActive(bool value)
        {
            _collider.isTrigger = value;
            _collider.enabled = value;
        }

        private void Update()
        {
            if (_canFollow)
            {
                CheckState();
                
                if(_aim)
                    Follow();
            }
        }

        private void CheckState()
        {
            if (!_aim)
            {
                _canFollow = false;
                SetColliderActive(false);
            }
        }

        private void Follow()
        {
            Vector3 dir = (_aim.position - transform.position).normalized;
            Rb.velocity = dir * Speed;
            transform.rotation = Quaternion.Euler(0,0, CalculateAngle2D(dir));
        }

        private static float CalculateAngle2D(Vector3 dir) =>
            Mathf.Acos(dir.x) * Mathf.Sign(Mathf.Asin(dir.y)) * Mathf.Rad2Deg;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform == _aim && collision.TryGetComponent(out ITakeDamage damage))
            {
                damage.Push(transform.position);
                ModifyHealth(damage);
                _rb.velocity = Vector2.zero;
                transform.SetParent(collision.transform);
                SetColliderActive(false);
                _canFollow = false;
            }
        }
    }

    public interface IHoming
    {
        public void SetAim(Transform aim);
    }
}