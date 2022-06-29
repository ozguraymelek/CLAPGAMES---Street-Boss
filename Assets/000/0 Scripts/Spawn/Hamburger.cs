using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hamburger : Singleton<Hamburger>
{
    [Header("Scriptable Object References")]
    [Space]
    [SerializeField] private GameSettings gameSettings;

    [Header("Components")]
    [Space]
    [SerializeField] internal Transform foodArea;
    [SerializeField] private Food food;
    [SerializeField] internal Transform[] areas;
    [SerializeField] internal List<Transform> hamburgers; 

    [Header("Settings")]
    [Space]
    public float spawnTime = 0;
    public bool canProduce = true;
    public float hamburgerLevel1PosY = 0;
    public float hamburgerLevel2PosY = 0;
    public float hamburgerLevel3PosY = 0;

    private void Update()
    {
        if (canProduce)
        {
            spawnTime += Time.deltaTime;

            if (spawnTime >= 5)
            {
                HamburgerSpawn();
                spawnTime = 0;
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
    }
    private void Level1(Food food)
    {
        if (transform.GetChild(1).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            instance.activeFood = instance.hamburgerTypes[0];
            instance.activeFood.SetActive(true);

            instance.transform.parent = areas[0];
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            instance.transform.DOJump(foodArea.position + new Vector3(0f, hamburgerLevel1PosY, 0f), 2.0f, 1, 1f).OnComplete(() => Completed(instance.transform, 1));
        }
    }
    private void Level2(Food food)
    {
        if (transform.GetChild(2).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            instance.activeFood = instance.hamburgerTypes[1];
            instance.activeFood.SetActive(true);

            instance.transform.parent = areas[1];
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            instance.transform.DOJump(foodArea.position + new Vector3(0f, hamburgerLevel2PosY, 0f), 2.0f, 1, 1f).OnComplete(() => Completed(instance.transform, 2));
        }
    }
    private void Level3(Food food)
    {
        if (transform.GetChild(3).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            instance.activeFood = instance.hamburgerTypes[2];
            instance.activeFood.SetActive(true);

            instance.transform.parent = areas[2];
            instance.transform.localPosition = new Vector3(0f, 1.35f, 0f);
            instance.transform.DOJump(foodArea.position + new Vector3(0f, hamburgerLevel3PosY, 0f), 2.0f, 1, 1f).OnComplete(() => Completed(instance.transform, 3));
        }
    }
    
    private void Completed(Transform food, int level)
    {
        switch (level)
        {
            case 1:
                StackManager.Instance.standHamburgerFoods.Add(food);
                hamburgerLevel1PosY += 1.2f;
                break;
            case 2:
                StackManager.Instance.standHamburgerFoods.Add(food);
                hamburgerLevel2PosY += 1.2f;
                break;
            case 3:
                StackManager.Instance.standHamburgerFoods.Add(food);
                hamburgerLevel3PosY += 1.2f;
                break;
        } 
    }
}
