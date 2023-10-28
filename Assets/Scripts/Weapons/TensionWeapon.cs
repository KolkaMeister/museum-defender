using UnityEngine;

public class TensionWeapon : RangeWeapon
{
    [SerializeField] protected Transform _arrowHoldPoint;
    [SerializeField] protected Projectile _strungArrow;

    private void Start()
    {
        if (_currentAmmo > 0)
            SetArrow();
    }
    
    public override void Attack()
    {
        //Debug.Log("Attack");
        if (!_strungArrow) return;

        _strungArrow.Shot(_arrowHoldPoint.position - _holdPoint.position, _force, _attackLayer);
        _strungArrow = null;
        _currentAmmo--;
    }
    
    public override void Reload(int count)
    {
        //Debug.Log("Reload");
        base.Reload(count);
        SetArrow();
    }

    public override void ChangeSpriteOrder(int order)
    {
        base.ChangeSpriteOrder(order);
        if(_strungArrow) _strungArrow.Renderer.sortingOrder = order;
    }

    private void SetArrow()
    {
        _strungArrow = Instantiate(_proj, transform);
        _strungArrow.transform.localPosition = _arrowHoldPoint.localPosition;
        _strungArrow.Collider.enabled = false;
        _strungArrow.Renderer.sortingOrder = SpriteRenderer.sortingOrder;
    }
}
