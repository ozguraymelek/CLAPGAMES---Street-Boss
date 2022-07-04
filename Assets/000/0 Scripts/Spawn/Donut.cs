using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Donut : MonoBehaviour
{
   [Header("Scriptable Object References")]
    [Space]
    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private DonutFoodArea foodAreaRef;
    
    [Header("Components")]
    [Space]
    [SerializeField] private Food food;

    [Header("Settings")]
    [Space]
    public float spawnTime = 0;
    public float spawnRate;
    public bool canProduce = true;
    public int countSpawnedDonut = 0;
    
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
        countSpawnedDonut = 0;
    }

    private void Update()
    {
        if (canProduce)
        {
            if (countSpawnedDonut < foodAreaRef.standPoints.Count)
            {
                spawnTime += Time.deltaTime;
                if (spawnTime >= spawnRate)
                {
                    DonutSpawn();
                    spawnTime = 0;
                }
            }else
                canProduce = false;
        }
    }
    private void DonutSpawn()
    {
        switch (gameSettings.donutbuildingIndex)
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

        countSpawnedDonut++;
    }
    private void Level1(Food food)
    {
        if (transform.GetChild(1).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            
            instance.activeFood = instance.donutTypes[0];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);
            
            instance.boxCollider.center = new Vector3(0, .5f, 0f);
            instance.boxCollider.size = new Vector3(1.14f, .5f, 1.14f);
            
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
                .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandDonut].position, 2.0f, 1, .7f).OnComplete(() =>
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
            
            instance.activeFood = instance.donutTypes[1];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);
            
            instance.boxCollider.center = new Vector3(0, .5f, 0f);
            instance.boxCollider.size = new Vector3(1.14f, .5f, 1.14f);
            
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
                .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandDonut].position, 2.0f, 1, .7f).OnComplete(() =>
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
            
            instance.activeFood = instance.donutTypes[2];
            instance.activeFood.transform.localScale = new Vector3(.7f, .7f, .7f);
            instance.activeFood.SetActive(true);

            instance.boxCollider.center = new Vector3(0, .5f, 0f);
            instance.boxCollider.size = new Vector3(1.14f, .5f, 1.14f);
            
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
                .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandDonut].position, 2.0f, 1, .7f).OnComplete(() =>
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
        
        StackManager.Instance.standDonutFoods.Add(instanceTransform.transform);
        
        instanceTransform.parent = foodAreaRef.standPoints[foodAreaRef.indexStandDonut].transform;
        instanceTransform.localPosition = Vector3.zero;
        instanceTransform.localScale = Vector3.one;
        
        foodAreaRef.indexStandDonut++;
    }
}