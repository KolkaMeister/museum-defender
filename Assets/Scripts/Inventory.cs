using System.Collections;
using UnityEngine;
using static Utility.Idents;

public class Inventory : MonoBehaviour
{
    //***********************Weapons*******************************//
    private readonly WeaponsInventory _weaponInventory = new WeaponsInventory();
    [SerializeField] private Transform _weaponsHolder;
    [SerializeField] private AmmoInventoryData _ammoInventory = new AmmoInventoryData();
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _backHoldPoint;

    private bool _isReloading;
    private bool _isDead;

    public Weapon CurrentWeapon => _weaponInventory.CurrentWeapon;

    public void CalculateWeaponRotation(Vector2 view)
    {
        Vector2 direction = view - (Vector2)_weaponsHolder.position;
        float rad = Mathf.Atan(direction.y / direction.x);
        _weaponsHolder.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
    }

    //////////Weapons Methods//////////
    public void TakeWeapon(Weapon wep)
    {
        _weaponInventory.TakeWeapon(wep);
        wep.tag = "Untagged";
        wep.SetAttackLayer(gameObject.layer == Layers.Player ? Layers.Enemy : Layers.Player);
        wep.ChangeSpriteOrder(2);
    }

    public void SetCurrentWeaponIndex(int weaponIndex)
    {
        // Debug.Log(weaponIndex);
        _weaponInventory.ChangeIndex(weaponIndex);
    }

    public void Reload()
    {
        if (_isReloading) return;

        Weapon current = _weaponInventory.CurrentWeapon;
        if (!current || current.IsFull || current.ReloadTime == 0)
            return;
        if (_ammoInventory.GetAmmo(current.AmmoType) < 1)
            return;
        StartCoroutine(ReloadRoutine(current.ReloadTime));
    }

    public void DropAll()
    {
        _weaponInventory.DropWeapon(0);
        _weaponInventory.DropWeapon(1);
    }

    private void OnEnable()
    {
        _weaponInventory.OnListChanged += OnInventoryChanged;
        _weaponInventory.OnUseChanged += OnInventoryIndexChanged;
        _weaponInventory.OnUseChanged += StopReload;
    }

    private void OnDisable()
    {
        _weaponInventory.OnListChanged -= OnInventoryChanged;
        _weaponInventory.OnUseChanged -= OnInventoryIndexChanged;
        _weaponInventory.OnUseChanged -= StopReload;
    }

    private void OnInventoryChanged(Weapon oldValue, Weapon newValue)
    {
        if (oldValue) oldValue.Drop(transform.position);
        if (newValue) TakeUpWeapon(newValue);
    }

    private void OnInventoryIndexChanged(Weapon last, Weapon current)
    {
        if (last) last.HandOnBack(_backHoldPoint);
        if (current) TakeUpWeapon(current);
    }

    private void TakeUpWeapon(Weapon wep)
    {
        wep.TakeUp(_holdPoint.transform, new Vector3(1, transform.localScale.y, transform.localScale.z));
    }

    private void StopReload(Weapon last, Weapon current)
    {
        if (!_isReloading) return;
        StopAllCoroutines();
        _isReloading = false;
    }

    private IEnumerator ReloadRoutine(float time)
    {
        _isReloading = true;
        yield return new WaitForSeconds(time);

        Weapon current = _weaponInventory.CurrentWeapon;
        if (!current) yield break;

        int totalCount = _ammoInventory.GetAmmo(current.AmmoType);
        int relCount = Mathf.Min(totalCount, current.MaxAmmo);
        current.Reload(relCount);
        _ammoInventory.ReduceAmmo(current.AmmoType, relCount);
        _isReloading = false;
    }
}