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
    public bool canProduce = true;
    public int countSpawnedHotDog = 0;
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

                if (spawnTime >= 2)
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
            
            instance.transform.parent = areas[0];
            instance.activeFood.transform.localPosition = new Vector3(0f, .465f, 0f);
            
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
                
            instance.transform.parent = areas[1];
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
                
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
        if (transform.GetChild(2).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, quaternion.identity);
            instance.activeFood = instance.hotDogTypes[2];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);
            
            instance.transform.parent = areas[2];
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
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
        
        StackManager.Instance.standHotDogFoods.Add(instanceTransform.transform);
        instanceTransform.transform.parent = foodAreaRef.standPoints[FoodArea.indexStandHotDog].transform;
        instanceTransform.transform.localPosition = Vector3.zero;
        FoodArea.indexStandHotDog++;
    }
}
