using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip popSound;
    [SerializeField] Transform popSound3DTr;

    public void PopSound(float soundValue)
    {
        audioSource.PlayOneShot(popSound, soundValue);
    }

    public void PopSound3D(Vector3 _pos)
    {
        Transform _eff = EZ_PoolManager.Spawn(popSound3DTr, _pos, Quaternion.identity);

        StartCoroutine(DespawnEffect(_eff, 0.5f));
    }

    private IEnumerator DespawnEffect(Transform _eff, float _despawnTime)
    {
        yield return new WaitForSeconds(_despawnTime);

        EZ_PoolManager.Despawn(_eff);
    }
}