using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hamburger : Singleton<Hamburger>
{
    [Header("Scriptable Object References")]
    [Space]
    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private FoodArea foodAreaRef;
    
    [Header("Components")]
    [Space]
    [SerializeField] internal Transform foodArea;
    [SerializeField] private Food food;
    [SerializeField] internal Transform[] areas;

    [Header("Settings")]
    [Space]
    public float spawnTime = 0;
    public bool canProduce = true;
    public int countSpawnedHamburger = 0;

    private void Update()
    {
        if (canProduce)
        {
            if (countSpawnedHamburger < foodAreaRef.standPoints.Count)
            {
                spawnTime += Time.deltaTime;

                if (spawnTime >= 5)
                {
                    HamburgerSpawn();
                    spawnTime = 0;
                }
            }
        }
    }
    private void HamburgerSpawn()
    {
        switch (gameSettings.hamburgerBuildingIndex)
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

        countSpawnedHamburger++;
    }
    private void Level1(Food food)
    {
        if (transform.GetChild(1).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            instance.activeFood = instance.hamburgerTypes[0];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);

            instance.transform.parent = areas[0];
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[FoodArea.indexStandHamburger].position, 2.0f, 1, .7f).OnComplete(() =>
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
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            instance.activeFood = instance.hamburgerTypes[1];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);

            instance.transform.parent = areas[1];
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[FoodArea.indexStandHamburger].position, 2.0f, 1, .7f).OnComplete(() =>
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
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            instance.activeFood = instance.hamburgerTypes[2];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);

            instance.transform.parent = areas[2];
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            
            instance.transform
                .DOJump(foodAreaRef.standPoints[FoodArea.indexStandHamburger].position, 2.0f, 1, .7f).OnComplete(() =>
                    {
                        Completed(instance.transform);
                    }
                );
        }
    }
    
    private void Completed(Transform instanceTransform)
    {
        instanceTransform.GetComponent<Food>().enabled = false;
        
        StackManager.Instance.standHamburgerFoods.Add(instanceTransform.transform);
        instanceTransform.transform.parent = foodAreaRef.standPoints[FoodArea.indexStandHamburger].transform;
        instanceTransform.transform.localPosition = Vector3.zero;
        FoodArea.indexStandHamburger++;
    }
}
