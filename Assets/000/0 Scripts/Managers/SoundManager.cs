using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;
using Unity.Mathematics;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource audioSource;
    
    [SerializeField] private Transform audioSourceStackedStandToArea;
    [SerializeField] private Transform audioSourceStackRunnerToIdle;
    [SerializeField] private Transform audioSourceStackRunner;
    [SerializeField] private Transform audioSourceStandUpgrade;
    [SerializeField] private Transform audioSourceMexicianWave;
    [SerializeField] private Transform audioSourceBoingStand;
    
    [SerializeField] AudioClip popSound;
    [SerializeField] Transform popSound3DTr;
    
    public void MexicianWave(Vector3 pos)
    {
        Transform sound = EZ_PoolManager.Spawn(audioSourceMexicianWave, pos, Quaternion.identity);

        StartCoroutine(DespawnEffect(sound, 1f));
    }
    public void BoingStandSound(Vector3 pos)
    {
        Transform sound = EZ_PoolManager.Spawn(audioSourceBoingStand, pos, Quaternion.identity);

        StartCoroutine(DespawnEffect(sound, 1f));
    }
    public void PopSound(float soundValue)
    {
        audioSource.PlayOneShot(popSound, soundValue);
    }

    public void PopSound3D(Vector3 _pos)
    {
        Transform _eff = EZ_PoolManager.Spawn(popSound3DTr, _pos, Quaternion.identity);

        StartCoroutine(DespawnEffect(_eff, 0.5f));
    }

    public void StackedStandToAreaSound(Vector3 pos)
    {
        Transform sound = EZ_PoolManager.Spawn(audioSourceStackedStandToArea, pos, Quaternion.identity);

        StartCoroutine(DespawnEffect(sound, .5f));
    }

    public void RunnerToIdleStackSound(Vector3 pos)
    {
        Transform sound = EZ_PoolManager.Spawn(audioSourceStackRunnerToIdle, pos, Quaternion.identity);
        
        StartCoroutine(DespawnEffect(sound, 2f));
    }

    public void RunnerStackSound(Vector3 pos)
    {
        Transform sound = EZ_PoolManager.Spawn(audioSourceStackRunner, pos, Quaternion.identity);

        StartCoroutine(DespawnEffect(sound, .5f));
    }

    public void StandUpgradeSound(Vector3 pos)
    {
        Transform sound = EZ_PoolManager.Spawn(audioSourceStandUpgrade, pos, Quaternion.identity);

        StartCoroutine(DespawnEffect(sound, 1f));
    }
    private IEnumerator DespawnEffect(Transform _eff, float _despawnTime)
    {
        yield return new WaitForSeconds(_despawnTime);

        EZ_PoolManager.Despawn(_eff);
    }
}