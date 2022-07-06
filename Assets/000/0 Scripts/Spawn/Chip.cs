using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chip : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [Space]
    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private ChipFoodArea foodAreaRef;
    
    [Header("Components")]
    [Space]
    [SerializeField] private Food food;

    [Header("Settings")]
    [Space]
    public float spawnTime = 0;
    public float spawnRate;
    public bool canProduce = true;
    public int countSpawnedChip = 0;
    
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
        countSpawnedChip = 0;
    }

    private void Update()
    {
        if (canProduce)
        {
            if (countSpawnedChip < foodAreaRef.standPoints.Count)
            {
                spawnTime += Time.deltaTime;
                if (spawnTime >= spawnRate)
                {
                    ChipSpawn();
                    spawnTime = 0;
                }
            }else
                canProduce = false;
        }
    }
    private void ChipSpawn()
    {
        switch (gameSettings.chipsbuildingIndex)
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

        countSpawnedChip++;
    }
    private void Level1(Food food)
    {
        if (transform.GetChild(1).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            
            instance.activeFood = instance.chipsTypes[0];
            instance.activeFood.transform.localScale = new Vector3(.6f, .6f, .6f);
            instance.activeFood.SetActive(true);

            instance.boxCollider.center = new Vector3(0, .5f, 0f);
            instance.boxCollider.size = new Vector3(.24f, .16f, .6f);
            
            instance.boxCollider.enabled = false;
            
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
            
            if (foodAreaRef.indexStandChip != foodAreaRef.standPoints.Count) 
            {
                activeLevelTr.DOPunchScale(new Vector3(.1f,.1f,.1f), shakeDuration)
                    .OnComplete(() =>
                    {
                        if (canProduce == false)
                        {
                            activeLevelTr.DOScale(1f, shakeDuration);
                        }
                    });
                
                instance.transform
                    .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandChip].position, 2.0f, 1, .7f).OnComplete(() =>
                        {
                            instance.boxCollider.enabled = true;
                            Completed(instance.transform);
                        }
                    );
            }
        }
    }
    private void Level2(Food food)
    {
        if (transform.GetChild(2).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            
            instance.activeFood = instance.chipsTypes[1];
            instance.activeFood.transform.localScale = new Vector3(.6f, .6f, .6f);
            instance.activeFood.SetActive(true);
            
            instance.boxCollider.center = new Vector3(-.009f, .5f, -.09f);
            instance.boxCollider.size = new Vector3(.8f, .8f, .28f);
            
            instance.boxCollider.enabled = false;
            
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
            
            if (foodAreaRef.indexStandChip != foodAreaRef.standPoints.Count) 
            {
                activeLevelTr.DOPunchScale(new Vector3(.1f,.1f,.1f), shakeDuration)
                    .OnComplete(() =>
                    {
                        if (canProduce == false)
                        {
                            activeLevelTr.DOScale(1f, shakeDuration);
                        }
                    });
                
                instance.transform
                    .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandChip].position, 2.0f, 1, .7f).OnComplete(() =>
                        {
                            instance.boxCollider.enabled = true;
                            Completed(instance.transform);
                        }
                    );
            }
        }
    }
    private void Level3(Food food)
    {
        if (transform.GetChild(3).gameObject.activeInHierarchy)
        {
            Food instance = Instantiate(food, transform.position, Quaternion.identity);
            
            instance.activeFood = instance.chipsTypes[2];
            instance.activeFood.transform.localScale = new Vector3(.6f, .6f, .6f);
            instance.activeFood.SetActive(true);
            
            instance.boxCollider.center = new Vector3(-.009f, .5f, -.09f);
            instance.boxCollider.size = new Vector3(.8f, .8f, .28f);
            
            instance.boxCollider.enabled = false;
            
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
            
            if (foodAreaRef.indexStandChip != foodAreaRef.standPoints.Count) 
            {
                activeLevelTr.DOPunchScale(new Vector3(.1f,.1f,.1f), shakeDuration)
                    .OnComplete(() =>
                    {
                        if (canProduce == false)
                        {
                            activeLevelTr.DOScale(1f, shakeDuration);
                        }
                    });
                
                instance.transform
                    .DOJump(foodAreaRef.standPoints[foodAreaRef.indexStandChip].position, 2.0f, 1, .7f).OnComplete(() =>
                        {
                            instance.boxCollider.enabled = true;
                            Completed(instance.transform);
                        }
                    );
            }
        }
    }
    
    private void Completed(Transform instanceTransform)
    {
        instanceTransform.GetComponent<Food>().enabled = false;

        EffectManager.Instance.StackOnStandEffect(instanceTransform.position, Quaternion.identity);
        SoundManager.Instance.StackedStandToAreaSound(instanceTransform.position);
        
        instanceTransform.DOShakeScale(shakeDurationStA, shakeStrengthStA);
        
        StackManager.Instance.standChipFoods.Add(instanceTransform.transform);
        
        instanceTransform.parent = foodAreaRef.standPoints[foodAreaRef.indexStandChip].transform;
        instanceTransform.localPosition = Vector3.zero;
        instanceTransform.localScale = Vector3.one;
        
        foodAreaRef.indexStandChip++;
    }
}
