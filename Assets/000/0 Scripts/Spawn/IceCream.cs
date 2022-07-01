using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class IceCream : MonoBehaviour
{
    [Header("Scriptable Object References")] [Space] 
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private IceCreamFoodArea foodAreaRef;
    
    [Header("Components")] [Space] 
    [SerializeField] private Food food;
    
    [Header("Settings")] [Space] 
    public float spawnTime = 0;
    public float spawnRate;
    public bool canProduce = true;
    public int countSpawnedIceCream = 0;
    
    [Header("Settings Shake")] [Space] 
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;
    [SerializeField] private float shakeDurationStA;
    [SerializeField] private float shakeStrengthStA;

    private void OnDisable()
    {
        canProduce = false;
    }

    private void OnApplicationQuit()
    {
        countSpawnedIceCream = 0;
    }

    private void Update()
    {
        if (canProduce)
        {
            if (countSpawnedIceCream < foodAreaRef.standPoints.Count)
            {
                spawnTime += Time.deltaTime;

                if (spawnTime >= spawnRate)
                {
                    IceCreamSpawn();
                    spawnTime = 0;
                }
            }else
                canProduce = false;
        }
    }

    private void IceCreamSpawn()
    {
        switch (gameSettings.iceCreambuildingIndex)
        {
            case 1:
                Level1(food);
                break;
            case 2:
                Level2(food);
                break;
            case 3:
                Level3(food);
                break;
        }

        countSpawnedIceCream++;
    }

    private void Level1(Food food1)
    {
        if (transform.GetChild(1).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            
            instance.activeFood = instance.iceCreamTypes[0];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);

            instance.transform.parent = transform.GetChild(4);
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
            Transform activeLevelTr = transform.GetChild(1);
            activeLevelTr.localScale=Vector3.one;
            if (activeLevelTr.localScale.x < 0 || 
                activeLevelTr.localScale.y < 0 ||
                activeLevelTr.localScale.z < 0 )
            {
                activeLevelTr.localScale = Vector3.one;
            }
            
            activeLevelTr.DOPunchScale(new Vector3(.1f,.1f,.1f), shakeDuration)
                .OnComplete(() =>
                {
                    if (canProduce == false)
                    {
                        activeLevelTr.DOScale(1f, shakeDuration);
                    }
                });
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandIceCream].position, 2.0f, 1, .7f).OnComplete(() =>
                    {
                        Completed(instance.transform);
                    }
                );
        }
    }

    private void Level2(Food food1)
    {
        if (transform.GetChild(2).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, quaternion.identity);
            
            instance.activeFood = instance.iceCreamTypes[1];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            
            instance.activeFood.SetActive(true);
                
            instance.transform.parent = transform.GetChild(4);
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
            Transform activeLevelTr = transform.GetChild(2);
            activeLevelTr.localScale=Vector3.one;
            if (activeLevelTr.localScale.x < 0 || 
                activeLevelTr.localScale.y < 0 ||
                activeLevelTr.localScale.z < 0 )
            {
                activeLevelTr.localScale = Vector3.one;
            }
            
            activeLevelTr.DOPunchScale(new Vector3(.1f,.1f,.1f), shakeDuration)
                .OnComplete(() =>
                {
                    if (canProduce == false)
                    {
                        activeLevelTr.DOScale(1f, shakeDuration);
                    }
                });
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandIceCream].position, 2.0f, 1, .7f).OnComplete(() =>
                    {
                        Completed(instance.transform);
                    }
                );
        }
    }

    private void Level3(Food food1)
    {
        if (transform.GetChild(3).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, quaternion.identity);
            
            instance.activeFood = instance.iceCreamTypes[2];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            
            instance.activeFood.SetActive(true);
            
            instance.transform.parent = transform.GetChild(4);
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
            Transform activeLevelTr = transform.GetChild(3);
            activeLevelTr.localScale=Vector3.one;
            if (activeLevelTr.localScale.x < 0 || 
                activeLevelTr.localScale.y < 0 ||
                activeLevelTr.localScale.z < 0 )
            {
                activeLevelTr.localScale = Vector3.one;
            }
            
            activeLevelTr.DOPunchScale(new Vector3(.1f,.1f,.1f), shakeDuration)
                .OnComplete(() =>
                {
                    if (canProduce == false)
                    {
                        activeLevelTr.DOScale(1f, shakeDuration);
                    }
                });
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandIceCream].position, 2.0f, 1, .7f).OnComplete(() =>
                    {
                        Completed(instance.transform);
                    }
                );
        }
    }
    
    private void Completed(Transform instanceTransform)
    {
        instanceTransform.GetComponent<Food>().enabled = false;
        
        EffectManager.Instance.StackOnStandEffect(instanceTransform.position, Quaternion.identity);
        SoundManager.Instance.StackedStandToAreaSound(instanceTransform.position);
        
        instanceTransform.DOPunchScale(new Vector3(.1f,.1f,.1f), shakeStrengthStA);
        
        StackManager.Instance.standIceCreamFoods.Add(instanceTransform.transform);
        
        instanceTransform.parent = foodAreaRef.standPoints[foodAreaRef.indexStandIceCream].transform;
        instanceTransform.localPosition = Vector3.zero;
        instanceTransform.localScale = Vector3.one;
            
        foodAreaRef.indexStandIceCream++;
    }
}
