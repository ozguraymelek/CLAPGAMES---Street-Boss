using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] Transform popEffect, bloodDirectional, upgradeEffect, stackOnStandEffect;

    [SerializeField] List<Transform> foodEffects;

    public void PopEffect(Vector3 _pos, Quaternion _rot)
    {
        Transform _effTr = EZ_PoolManager.Spawn(popEffect, _pos, _rot);

        StartCoroutine(DespawnEffect(_effTr, 0.5f));
    }

    public void BloodDirectional(Vector3 _pos, Vector3 _euler)
    {
        Transform _effTr = EZ_PoolManager.Spawn(bloodDirectional, _pos, Quaternion.Euler(180, _euler.y, _euler.z));

        StartCoroutine(DespawnEffect(_effTr, 1.0f));
    }

    public void UpgradeEffect(Vector3 pos, Quaternion rot)
    {
        Transform _effTr = EZ_PoolManager.Spawn(upgradeEffect, pos, rot);

        StartCoroutine(DespawnEffect(_effTr, 1.0f));
    }

    public void StackOnDeskEffect(Vector3 pos, Quaternion rot, StackManager.FoodTypes foodTypes)
    {
        Transform _effTr = null;

        switch (foodTypes)
        {
            case StackManager.FoodTypes.Hamburger:
                _effTr = EZ_PoolManager.Spawn(foodEffects[0], pos, rot);
                break;
            case StackManager.FoodTypes.HotDog:
                _effTr = EZ_PoolManager.Spawn(foodEffects[1], pos, rot);
                break;
            case StackManager.FoodTypes.IceCream:
                _effTr = EZ_PoolManager.Spawn(foodEffects[2], pos, rot);
                break;
            case StackManager.FoodTypes.Donut:
                _effTr = EZ_PoolManager.Spawn(foodEffects[3], pos, rot);
                break;
            case StackManager.FoodTypes.Popcorn:
                _effTr = EZ_PoolManager.Spawn(foodEffects[4], pos, rot);
                break;
            case StackManager.FoodTypes.Chip:
                _effTr = EZ_PoolManager.Spawn(foodEffects[5], pos, rot);
                break;
        }
        StartCoroutine(DespawnEffect(_effTr, 1.0f));
    }

    public void StackOnStandEffect(Vector3 pos, Quaternion rot)
    {
        Transform _effTr = EZ_PoolManager.Spawn(stackOnStandEffect, pos, rot);

        StartCoroutine(DespawnEffect(_effTr, 1.0f));
    }
    private IEnumerator DespawnEffect(Transform _effTr, float _despawnTime)
    {
        yield return new WaitForSeconds(_despawnTime);

        EZ_PoolManager.Despawn(_effTr);
    }
}