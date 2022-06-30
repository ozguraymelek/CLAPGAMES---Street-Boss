using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class HotDog : Singleton<HotDog>
{
    [Header("Scriptable Object References")] [Space]
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private FoodArea foodAreaRef;

    [Header("Components")] [Space] 
    [SerializeField] internal Transform foodArea;
    [SerializeField] private Food food;
    [SerializeField] internal Transform[] areas;

    [Header("Settings")] [Space] 
    public float spawnTime = 0;
    public float spawnRate;
    public bool canProduce = true;
    public int countSpawnedHotDog = 0;

    [Header("Settings Shake")] [Space] 
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;
    [SerializeField] private float shakeDurationStA;
    [SerializeField] private float shakeStrengthStA;
    
    private void OnDisable()
    {
        canProduce = false;
    }

    private void Update()
    {
        if (canProduce)
        {
            if (countSpawnedHotDog < foodAreaRef.standPoints.Count)
            {
                spawnTime += Time.deltaTime;

                if (spawnTime >= spawnRate)
                {
                    HotDogSpawn();
                    spawnTime = 0;
                } 
            }
        }
    }

    private void HotDogSpawn()
    {
        switch (gameSettings.hotdogbuildingIndex)
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

        countSpawnedHotDog++;
    }
    
    private void Level1(Food food)
    {
        if (transform.GetChild(1).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, quaternion.identity);
            instance.activeFood = instance.hotDogTypes[0];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);
            
            instance.transform.parent = transform.GetChild(4);
            instance.activeFood.transform.localPosition = new Vector3(0f, .465f, 0f);
            
            Transform activeLevelTr = transform.GetChild(1);
            activeLevelTr.DOShakeScale(shakeDuration, shakeStrength); 
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[FoodArea.indexStandHotDog].position, 2.0f, 1, .7f).OnComplete(() =>
                    {
                        Completed(instance.transform);
                    }
                );
        }
    }
    
    private void Level2(Food food)
    {
        if (transform.GetChild(2).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, quaternion.identity);
            instance.activeFood = instance.hotDogTypes[1];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);
                
            instance.transform.parent = transform.GetChild(4);
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
            Transform activeLevelTr = transform.GetChild(2);
            activeLevelTr.DOShakeScale(shakeDuration, shakeStrength); 
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[FoodArea.indexStandHotDog].position, 2.0f, 1, .7f).OnComplete(() =>
                    {
                        Completed(instance.transform);
                    }
                );
        }
    }
    
    private void Level3(Food food)
    {
        if (transform.GetChild(3).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, quaternion.identity);
            instance.activeFood = instance.hotDogTypes[2];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);
            
            instance.transform.parent = transform.GetChild(4);
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
            Transform activeLevelTr = transform.GetChild(3);
            activeLevelTr.DOShakeScale(shakeDuration, shakeStrength); 
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[FoodArea.indexStandHotDog].position, 2.0f, 1, .7f).OnComplete(() =>
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
        
        instanceTransform.DOShakeScale(shakeDurationStA, shakeStrengthStA);
        
        StackManager.Instance.standHotDogFoods.Add(instanceTransform.transform);
        
        instanceTransform.parent = foodAreaRef.standPoints[FoodArea.indexStandHotDog].transform;
        instanceTransform.localPosition = Vector3.zero;
        instanceTransform.localScale = Vector3.one;
            
        FoodArea.indexStandHotDog++;
    }
}
