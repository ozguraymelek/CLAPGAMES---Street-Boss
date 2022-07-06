using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StackManager : Singleton<StackManager>
{
    public List<Food> foods = new List<Food>();
    public List<Transform> moneys = new List<Transform>();
    public List<Food> foodsInCircle = new List<Food>();

    public List<Transform> collectedHamburgers;
    public List<Transform> collectedHotDogs;
    public List<Transform> collectedIceCreams;
    public List<Transform> collectedDonuts;
    public List<Transform> collectedPopcorns;
    public List<Transform> collectedChips;

    public List<Transform> collectedFromStandHamburgers;

    public List<Food> objectsOnDesk;

    public List<Transform> collectedMoneyFromCustomers = new List<Transform>();

    public List<Transform> standHamburgerFoods;
    public List<Transform> standHotDogFoods;
    public List<Transform> standIceCreamFoods;
    public List<Transform> standChipFoods;
    public List<Transform> standDonutFoods;
    public List<Transform> standPopcornFoods;


    public List<Food> stackedCurrentHamburgerObject;

    public Transform canvasParent;
    public Image instanceMoneyPrefab;
    public Image instanceMoneyIcon;

    [Header("Transform Settings")] [SerializeField]
    internal float refHamburgerPosY = 0f;

    [SerializeField] internal float refHotDogPosY = 0f;
    [SerializeField] internal float refIceCreamPosY = 0f;
    [SerializeField] internal float refDonutPosY = 0f;
    [SerializeField] internal float refPopcornPosY = 0f;
    [SerializeField] internal float refChipPosY = 0f;

    [Header("Scriptable Object Reference")] [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField] private GameSettings gameSettings;

    [Header("Settings Stack to Desk ")][Space]
    [SerializeField] private float shakeDurationStd;
    [SerializeField] private float shakeStrengthStd;
    
    public GameObject moneyPrefabForCustomer;

    [SerializeField] private float moneyMoveSpeed;
    
    public int indexHamburgerDesk = 0;
    public int indexHotDogDesk = 0;
    public int indexIceCreamDesk = 0;
    public int indexChipDesk = 0;
    public int indexDonutDesk = 0;
    public int indexPopcornDesk = 0;

    public float moneyThrowDuration;
    public enum FoodTypes
    {
        Hamburger,
        HotDog,
        IceCream,
        Donut,
        Popcorn,
        Chip
    }

    [SerializeField] float posDistRunner;
    [SerializeField] float postDistIdle;
    public float lerpSpeedForRunner;
    private float _speedX;
    [SerializeField] float lerpSpeedForIdle;

    Sequence seq;
    Vector3 normalScale;
    bool canMexicanVawe = true;
    [SerializeField] float delayedVaweT1 = 0.05f, delayedVaweT2 = 0.15f, delayedVaweT3 = 0.15f;

    private Transform _firstFoodTrForRunner, _firstFoodTrForIdle, _currentFirstFoodTr;

    [SerializeField] private List<Vector3> _circlePositions = new List<Vector3>();

    [SerializeField] List<Transform> stackPoints;

    public float posToStackY = .5f;
    [SerializeField] private Prince prince;

    private void Start()
    {
        _firstFoodTrForRunner = FindObjectOfType<CkyBehaviour>().stackFirstPointTrForRunner;
        _firstFoodTrForIdle = FindObjectOfType<CkyBehaviour>().stackFirstPointTrForIdle;
        _currentFirstFoodTr = _firstFoodTrForRunner;

        _speedX = lerpSpeedForRunner * Time.deltaTime;
        normalScale = Vector3.one;

        CkyEvents.OnStart += OnStart;
        CkyEvents.OnTransToRunner += OnTransToRunner;
        CkyEvents.OnTransToIdle += OnTransToIdle;
    }

    private void Update()
    {
        if (foods.Count != 0)
        {
            AnimationController.SetLayerWeight(prince.player.anim, "isCarrying", true);
        }
        else
        {
            AnimationController.SetLayerWeight(prince.player.anim, "isCarrying", false);
        }
    }

    private void OnTransToRunner()
    {
        canMexicanVawe = true;

        CkyEvents.OnUpdate += UpdatePositionsForRunner;
        CkyEvents.OnFixedUpdate -= UpdatePositionsForIdle;

        _currentFirstFoodTr = _firstFoodTrForRunner;

        if (foods.Count > 0)
        {
            foods[0].transform.parent = _currentFirstFoodTr;
            foods[0].transform.localPosition = Vector3.zero;
            foods[0].transform.localEulerAngles = Vector3.zero;
        }

        ResetRotateFoods();

        OpenFoodColliders(true);
    }

    private void ResetRotateFoods()
    {
        foreach (Food food in foods)
        {
            food.transform.eulerAngles = Vector3.zero;
        }
    }

    private void OnTransToIdle()
    {
        canMexicanVawe = false;

        CkyEvents.OnUpdate -= UpdatePositionsForRunner;
        CkyEvents.OnFixedUpdate += UpdatePositionsForIdle;

        _currentFirstFoodTr = _firstFoodTrForIdle;

        if (foods.Count > 0)
            foods[0].transform.parent = null;

        //FoodMovesToCircle();
    }

    private void OnStart()
    {
        CkyEvents.OnUpdate += UpdatePositionsForRunner;
    }

    #region Foods Count

    public int ReturnFoodCount()
    {
        return foods.Count;
    }

    public int ReturnMoneyCount()
    {
        return moneys.Count;
    }

    public int ReturnHamburgerCount()
    {
        return collectedHamburgers.Count;
    }

    public int ReturnHotDogCount()
    {
        return collectedHotDogs.Count;
    }

    public int ReturnIceCreamCount()
    {
        return collectedIceCreams.Count;
    }

    public int ReturnDonutCount()
    {
        return collectedDonuts.Count;
    }

    public int ReturnPopcornCount()
    {
        return collectedPopcorns.Count;
    }

    public int ReturnChipCount()
    {
        return collectedChips.Count;
    }

    #endregion

    public void AddFood(Food _food)
    {
        SoundManager.Instance.RunnerStackSound(_food.transform.position);
        
        foods.Add(_food);
        _food.isOnList = true;
        print("0.1.2");
        if (foods.Count == 1)
        {
            print("1.2.3.");
            _food.transform.parent = _currentFirstFoodTr;
            foods[0].transform.DOLocalMove(Vector3.zero, 1);
        }

        if (canMexicanVawe)
        {
            StartCoroutine(CanVaweOrNot());
            MexicanWave();
        }

        if (prince.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            if(_food != foods[0])
                _food.transform.parent = null;
            
            if (_food.activeFood == _food.hamburgerTypes[0] ||_food.activeFood == _food.hamburgerTypes[1] || 
                _food.activeFood == _food.hamburgerTypes[2])
            {
                collectedHamburgers.Add(_food.transform);
                _food.GetComponent<Rigidbody>().isKinematic = true;
            }
            
            if (_food.activeFood == _food.hotDogTypes[0] || _food.activeFood == _food.hotDogTypes[1] || 
                _food.activeFood == _food.hotDogTypes[2])
            {
                collectedHotDogs.Add(_food.transform);
                _food.GetComponent<Rigidbody>().isKinematic = true;
            }

            if (_food.activeFood == _food.iceCreamTypes[0] || _food.activeFood == _food.iceCreamTypes[1] ||
                _food.activeFood == _food.iceCreamTypes[2])
            {
                collectedIceCreams.Add(_food.transform);
                _food.GetComponent<Rigidbody>().isKinematic = true;
            }

            foods[0].transform.localEulerAngles = Vector3.zero;
        }

        if (prince.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            UI_Manager.Instance.IncreaseFoodCountToUI(_food);
        }
    }

    public void SetScaleIdleToRunner()
    {
        foreach (var food in foods)
        {
            food.activeFood.transform.DOScale(1f, .7f);
        }
    }
    public void Remove(Food _food)
    {
        if (foods.Count == 1)
        {
            foods[0].transform.parent = null;
        }

        _food.isOnList = false;

        foods.Remove(_food);

        _food.FoodOnScatter();
    }

    private void UpdatePositionsForRunner()
    {
        if (foods.Count == 0) return;

        float targetPosY = FindObjectOfType<CkyBehaviour>().stackFirstPointTrForRunner.position.y;

        for (int i = 1; i < foods.Count; i++)
        {
            float targetPosX = Mathf.Lerp(foods[i].transform.position.x, foods[i - 1].transform.position.x, _speedX);
            foods[i].transform.position =
                new Vector3(targetPosX, targetPosY, foods[i - 1].transform.position.z + posDistRunner);
        }
    }

    public void BeScattered(Food _food)
    {
        if (prince.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            int damagedIndex = foods.IndexOf(_food);

            print(damagedIndex);

            int foodIndex = foods.Count;

            for (int i = damagedIndex; i < foodIndex; i++)
            {
                Food _tempFood = foods[damagedIndex];
                UI_Manager.Instance.DecreaseFoodCountToUI(_tempFood);
                Remove(_tempFood);
            }
            
        }
    }


    #region Mexican Vawe

    IEnumerator CanVaweOrNot()
    {
        canMexicanVawe = false;

        yield return new WaitForSeconds(0.2f);
        canMexicanVawe = true;
    }

    void MexicanWave()
    {
        int _t = 0;
        int _foodCount = foods.Count;

        if (_foodCount < 2) return;
        for (int i = _foodCount - 1; i >= 0; i--)
        {
            seq = DOTween.Sequence();
            StartCoroutine(DelayedWave(i, _t * delayedVaweT1));
            _t++;
        }
    }

    IEnumerator DelayedWave(int _i, float _t)
    {
        Food _food = foods[_i];
        yield return new WaitForSeconds(_t);

        if (_food.gameObject != null)
        {
            _food.gameObject.transform
                .DOScale(new Vector3(normalScale.x * 1.45f, normalScale.y * 1.35f, normalScale.z * 1.35f),
                    delayedVaweT2).OnComplete(() =>
                    _food.gameObject.transform.DOScale(normalScale, delayedVaweT3));
        }
    }

    #endregion


    #region Align Circle

    [SerializeField] float circleRadius = 2.0f, foodArriveTimeToCircle = 0.2f;

    public void FoodMovesToCircle(Food _food)
    {
        if (foods.Count == 0)
        {
            return;
        }
        
        if (_food.activeFood == _food.hamburgerTypes[0] || _food.activeFood == _food.hamburgerTypes[1] || _food.activeFood == _food.hamburgerTypes[2])
        {
            collectedHamburgers.Add(_food.transform);
        }

        if (_food.activeFood == _food.hotDogTypes[0] || _food.activeFood == _food.hotDogTypes[1] || _food.activeFood == _food.hotDogTypes[2])
        {
            collectedHotDogs.Add(_food.transform);
        }

        if (_food.activeFood == _food.iceCreamTypes[0] || _food.activeFood == _food.iceCreamTypes[1] || _food.activeFood == _food.iceCreamTypes[2])
        {
            collectedIceCreams.Add(_food.transform);
        }

        if (_food.activeFood == _food.donutTypes[0] || _food.activeFood == _food.donutTypes[1] || _food.activeFood == _food.donutTypes[2])
        {
            collectedDonuts.Add(_food.transform);
        }

        if (_food.activeFood == _food.chipsTypes[0] || _food.activeFood == _food.chipsTypes[1] || _food.activeFood == _food.chipsTypes[2])
        {
            collectedChips.Add(_food.transform);
        }

        if (_food.activeFood == _food.popcornTypes[0] || _food.activeFood == _food.popcornTypes[1] || _food.activeFood == _food.popcornTypes[2])
        {
            collectedPopcorns.Add(_food.transform);
        }
        
        int i = foods.IndexOf(_food);

        if (i == foods.Count - 1)
        {
            FindPositionsInCircle();
        }

        if (i == 0) //TODO: el ile teması da ekle.
        {
            _food.transform.parent = null;
            FindObjectOfType<CkyEvents>().OnTransitionToIdle();
            FindObjectOfType<IdleTrigger>().EnableColliderTrigger(false);
        }

        Remove(_food);
        foodsInCircle.Add(_food);
        
        _food.GetComponent<Rigidbody>().isKinematic = true;
        _food.GetComponent<BoxCollider>().enabled = false;

        _food.transform.DOMove(_circlePositions[i], foodArriveTimeToCircle).OnComplete(() =>
        {
            _food.activeFood.transform.DOScale(.6f, 1f);
            
            
            SetBoxCollider(_food);
            
            // UI_Manager.Instance.DecreaseFoodCountToUI(_food);
            EffectManager.Instance.PopEffect(_food.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            SoundManager.Instance.RunnerToIdleStackSound(_food.transform.position);
        });
    }

    void SetBoxCollider(Food _food)
    {
        print("girdi");
        if (_food.activeFood == _food.hamburgerTypes[0] || _food.activeFood == _food.hamburgerTypes[1] ||
         _food.activeFood == _food.hamburgerTypes[2])
        {
            _food.boxCollider.center = new Vector3(0f, .5f, 0f);
            _food.boxCollider.size = new Vector3(.85f, .65f, .86f);
        }
        
        if (_food.activeFood == _food.hotDogTypes[0] || _food.activeFood == _food.hotDogTypes[1] ||
            _food.activeFood == _food.hotDogTypes[2])
        {
            _food.boxCollider.center = new Vector3(.04f, .635f, 0f);
            _food.boxCollider.size = new Vector3(1.15f, .55f, .67f);
        }
        
        if (_food.activeFood == _food.iceCreamTypes[0])
        {
            _food.boxCollider.center = new Vector3(0, .5f, 0f);
            _food.boxCollider.size = new Vector3(.7f, 1.2f, .7f);
        }
        if (_food.activeFood == _food.iceCreamTypes[1])
        {
            _food.boxCollider.center = new Vector3(.17f, .5f, -.025f);
            _food.boxCollider.size = new Vector3(1.05f, 1.18f, .74f);
        }
        if (_food.activeFood == _food.iceCreamTypes[2])
        {
            _food.boxCollider.center = new Vector3(0f, .5f, -.1f);
            _food.boxCollider.size = new Vector3(1.7f, 1.4f, 1f);
        }

        if (_food.activeFood == _food.donutTypes[0] || _food.activeFood == _food.donutTypes[1] ||
            _food.activeFood == _food.donutTypes[2])
        {
            _food.boxCollider.center = new Vector3(0, .5f, 0f);
            _food.boxCollider.size = new Vector3(.96f, .3f, .96f);
        }
        
        if (_food.activeFood == _food.chipsTypes[0])
        {
            _food.boxCollider.center = new Vector3(0, .5f, 0f);
            _food.boxCollider.size = new Vector3(.24f, .16f, .6f);
        }
        if (_food.activeFood == _food.chipsTypes[1])
        {
            _food.boxCollider.center = new Vector3(-.009f, .5f, -.09f);
            _food.boxCollider.size = new Vector3(.8f, .8f, .28f);
        }
        if (_food.activeFood == _food.chipsTypes[2])
        {
            _food.boxCollider.center = new Vector3(-.009f, .5f, -.09f);
            _food.boxCollider.size = new Vector3(.8f, .8f, .28f);
        }
        
        if (_food.activeFood == _food.popcornTypes[0] || _food.activeFood == _food.popcornTypes[1] ||
            _food.activeFood == _food.popcornTypes[2])
        {
            _food.boxCollider.center = new Vector3(0, .5f, 0f);
            _food.boxCollider.size = new Vector3(.9f, .9f, .85f);
        }

        
    }
    public void InteractHand()
    {
        print("TTSSDA");
        if (foods.Count == 0)
        {
            FindObjectOfType<CkyEvents>().OnTransitionToIdle();
            FindObjectOfType<IdleTrigger>().EnableColliderTrigger(false);
        }
    }
    public void FindPositionsInCircle()
    {
        Vector3 _pos = foods[foods.Count - 1].transform.position;
        _pos.x = 0;

        float _count = foods.Count;
        float angleSection = Mathf.PI * 2f / _count;
        for (int i = 0; i < _count; i++)
        {
            float angle = i * angleSection;
            Vector3 newPos = _pos + new Vector3(0, circleRadius + 1, 0) +
                             new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 1.5f) * circleRadius;

            _circlePositions.Add(newPos);
            EffectManager.Instance.PopEffect(newPos, Quaternion.identity);
        }
    }

    #endregion

    #region Stack For Idle

    public void StackForIdle()
    {
        StartCoroutine(DelayedStackForIdle());
    }

    private IEnumerator DelayedStackForIdle()
    {
        for (int i = 0; i < foodsInCircle.Count; i++)
        {
            Food food = foodsInCircle[i];
            //food.GetComponent<Rigidbody>().isKinematic = false;
            //food.GetComponent<BoxCollider>().enabled = true;
            AddFood(food);

            yield return new WaitForSeconds(0.2f);
        }

        foodsInCircle.Clear();
        _circlePositions.Clear();

        OpenFoodColliders(false);
    }

    private void OpenFoodColliders(bool b)
    {
        foreach (Food food in foods)
        {
            food.GetComponent<BoxCollider>().enabled = b;
        }
    }

    #endregion

    public void UpdatePositionsForIdle()
    {
        if (foods.Count == 0) return;

        for (int i = 1; i < foods.Count; i++)
        {
            Vector3 targetPos = new Vector3(foods[i - 1].transform.position.x,
                foods[0].transform.position.y + postDistIdle * i, foods[i - 1].transform.position.z);
            foods[i].transform.position =
                Vector3.Lerp(foods[i].transform.position, targetPos, lerpSpeedForIdle * Time.deltaTime);
            foods[i].transform.rotation = Quaternion.Lerp(foods[i].transform.rotation, foods[i - 1].transform.rotation,
                lerpSpeedForIdle * Time.deltaTime);
            //foods[i].transform.position = Vector3.Lerp(foods[i].transform.position, targetPos, (lerpSpeedForIdle - i * lerpSpeedForIdle / 40) * Time.deltaTime);
            // StartCoroutine(UpdatePositionsForIdleEffect(i, targetPos));
        }
    }

    IEnumerator UpdatePositionsForIdleEffect(int i, Vector3 targetPos)
    {
        bool isArrived = false;

        while (isArrived == false)
        {
            if (IsArrivedToTheFirstPosition(foods[i].transform.position, targetPos))
            {
                print("1.1111");
                // EffectManager.Instance.PopEffect(targetPos, Quaternion.identity);
                isArrived = true;
                print("1.2222");
            }

            yield return new WaitForFixedUpdate();
        }
    }

    bool IsArrived(Vector3 shieldPos, Vector3 targetPos)
    {
        Vector3 offset = targetPos - shieldPos;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen < 0.2f * 0.2f)
        {
            return true;
        }

        return false;
    }

    #region Using Money

    public void RemoveObjectMoney(Transform targetTr)
    {
        if (0 == ReturnMoneyCount()) return;

        Transform money = moneys[moneys.Count - 1];

        money.transform.parent = null;
        money.gameObject.SetActive(true);

        GoToTheActivator2Money(money.transform, targetTr);

        moneys.Remove(money);
    }

    private void GoToTheActivator2Money(Transform objTr, Transform targetTr)
    {
        var diss = 0.7f;
        var randomX = Random.Range(-diss, diss);
        var randomY = Random.Range(-diss, diss);
        var randomPos = new Vector3(randomX, 0, randomY);

        objTr.DOJump(targetTr.position + randomPos, 6.0f, 1, moneyThrowDuration).OnComplete(
            () =>
            {
                print("TTT");
                playerSettings.takedDeck--;
                objTr.gameObject.SetActive(false);
                EffectManager.Instance.PopEffect(targetTr.position, Quaternion.identity);
                UI_Manager.Instance.DecreasePlayerMoney();
                //SoundManager.Instance.UseShield(targetTr.position);
                //EffectManager.Instance.ShieldFountainSingle(targetTr.position + randomPos);
            });

        objTr.DORotate(new Vector3(180, Random.Range(-25, 25), 0), 0.7f, RotateMode.FastBeyond360);
    }

    #endregion

    #region Activator - Desk

    public void StackToDesk()
    {
        if (foods.Count > 0)
        {
            Food food = foods[foods.Count - 1];
            
            if (food.activeFood == food.hamburgerTypes[0] ||food.activeFood == food.hamburgerTypes[1] || 
                food.activeFood == food.hamburgerTypes[2])
            {
                if (indexHamburgerDesk >= ActivatorDesk.Instance.hamburgerFoodStackPoints.Count - 1)
                    indexHamburgerDesk = 0;
                
                print("food is hamburger!");
                food.transform.parent = ActivatorDesk.Instance.hamburgerFoodStackPoints[indexHamburgerDesk++];
                
                Activator(food,ActivatorDesk.Instance.hamburgerFoodStackPoints[indexHamburgerDesk]);
                
                foods.Remove(foods[foods.Count-1]);  
            }
            
            if (food.activeFood == food.hotDogTypes[0] || food.activeFood == food.hotDogTypes[1] || 
                food.activeFood == food.hotDogTypes[2])
            {
                if (indexHotDogDesk >= ActivatorDesk.Instance.hotDogFoodStackPoints.Count - 1)
                    indexHotDogDesk = 0;
                
                print("food is hotdog!" + " " + indexHotDogDesk);
                food.transform.parent = ActivatorDesk.Instance.hotDogFoodStackPoints[indexHotDogDesk++];
                
                Activator(food,ActivatorDesk.Instance.hotDogFoodStackPoints[indexHotDogDesk]);
                
                foods.Remove(foods[foods.Count-1]);  
            }

            if (food.activeFood == food.iceCreamTypes[0] || food.activeFood == food.iceCreamTypes[1] ||
                food.activeFood == food.iceCreamTypes[2])
            {
                if (indexIceCreamDesk >= ActivatorDesk.Instance.iceCreamFoodStackPoints.Count - 1)
                    indexIceCreamDesk = 0;
                
                print("food is icecream!" + " " + indexIceCreamDesk);
                food.transform.parent = ActivatorDesk.Instance.iceCreamFoodStackPoints[indexIceCreamDesk++];

                Activator(food, ActivatorDesk.Instance.iceCreamFoodStackPoints[indexIceCreamDesk]);

                foods.Remove(foods[foods.Count - 1]);
            }
            
            if (food.activeFood == food.chipsTypes[0] || food.activeFood == food.chipsTypes[1] ||
                 food.activeFood == food.chipsTypes[2])
            {
                if (indexChipDesk >= ActivatorDesk.Instance.chipFoodStackPoints.Count - 1)
                    indexChipDesk = 0;
                
                print("food is chip!" + " " + indexChipDesk);
                food.transform.parent = ActivatorDesk.Instance.chipFoodStackPoints[indexChipDesk++];

                Activator(food, ActivatorDesk.Instance.chipFoodStackPoints[indexChipDesk]);

                foods.Remove(foods[foods.Count - 1]);
            }
            
            if (food.activeFood == food.donutTypes[0] || food.activeFood == food.donutTypes[1] ||
                food.activeFood == food.donutTypes[2])
            {
                if (indexDonutDesk >= ActivatorDesk.Instance.donutFoodStackPoints.Count - 1)
                    indexDonutDesk = 0;
                
                print("food is donut!" + " " + indexDonutDesk);
                food.transform.parent = ActivatorDesk.Instance.donutFoodStackPoints[indexDonutDesk++];

                Activator(food, ActivatorDesk.Instance.donutFoodStackPoints[indexDonutDesk]);

                foods.Remove(foods[foods.Count - 1]);
            }
            
            if (food.activeFood == food.popcornTypes[0] || food.activeFood == food.popcornTypes[1] ||
                food.activeFood == food.popcornTypes[2])
            {
                if (indexPopcornDesk >= ActivatorDesk.Instance.popcornFoodStackPoints.Count - 1)
                    indexPopcornDesk = 0;
                
                print("food is popcorn!" + " " + indexPopcornDesk);
                food.transform.parent = ActivatorDesk.Instance.popcornFoodStackPoints[indexPopcornDesk++];

                Activator(food, ActivatorDesk.Instance.popcornFoodStackPoints[indexPopcornDesk]);

                foods.Remove(foods[foods.Count - 1]);
            }
            UI_Manager.Instance.DecreaseFoodCountToUI(food);
        }
    }

    void Activator(Food food, Transform targetTr)
    {
        food.transform.DOJump(targetTr.position, 2.0f, 1, 0.8f).OnComplete(
            () =>
            {
                objectsOnDesk.Add(food);
                food.transform.localPosition = Vector3.zero;
                
                List(food);
                
                food.activeFood.transform.DOShakeScale(shakeDurationStd, shakeStrengthStd);
                
                Effects(food, food.transform.position);
                
                SetFoodsBoxColliderProperties(food.GetComponent<BoxCollider>());
                SetFoodsRigidbodyProperties(food.GetComponent<Rigidbody>());
            });
        
        food.transform.DORotate(new Vector3(360, Random.Range(-25, 25), 0), 0.7f, RotateMode.FastBeyond360);
    }

    private void SetFoodsBoxColliderProperties(BoxCollider boxCollider)
    {
        boxCollider.enabled = true;
        boxCollider.isTrigger = false;
    }
    private void SetFoodsRigidbodyProperties(Rigidbody rigidBody)
    {
        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
        rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    void List(Food food)
    {
        if (food.activeFood == food.hamburgerTypes[0] ||
            food.activeFood == food.hamburgerTypes[1] ||
            food.activeFood == food.hamburgerTypes[2])
        {
            stackedCurrentHamburgerObject.Add(food);
        }
    }
    private void GoToTheActivator(Food food, Vector3 targetTr)
    {
        var diss = 0.7f;
        var randomX = Random.Range(-diss, diss);
        var randomY = Random.Range(-diss, diss);
        var randomPos = new Vector3(randomX, 0, randomY);


        food.transform.DOScale(1.5f, 1f);
        food.transform.DOJump(targetTr, 6.0f, 1, 0.8f).OnComplete(
            () =>
            {
                objectsOnDesk.Add(food);
                food.GetComponent<BoxCollider>().enabled = false;
            });

        food.transform.DORotate(new Vector3(360, Random.Range(-25, 25), 0), 0.7f, RotateMode.FastBeyond360);
    }
    #endregion
    
    #region Effects
    
    private void Effects(Food food, Vector3 effectPoint)
    {
        if (food.activeFood == food.hamburgerTypes[0] || food.activeFood == food.hamburgerTypes[1] || food.activeFood == food.hamburgerTypes[2])
        {
            EffectManager.Instance.StackOnDeskEffect(effectPoint, Quaternion.identity, FoodTypes.Hamburger);
        }
        
        if (food.activeFood == food.hotDogTypes[0] || food.activeFood == food.hotDogTypes[1] || food.activeFood == food.hotDogTypes[2])
        {
            EffectManager.Instance.StackOnDeskEffect(effectPoint, Quaternion.identity, FoodTypes.HotDog);
        }
        
        if (food.activeFood == food.iceCreamTypes[0] || food.activeFood == food.iceCreamTypes[1] || food.activeFood == food.iceCreamTypes[2])
        {
            EffectManager.Instance.StackOnDeskEffect(effectPoint, Quaternion.identity, FoodTypes.IceCream);
        }
        
        if (food.activeFood == food.donutTypes[0] || food.activeFood == food.donutTypes[1] || food.activeFood == food.donutTypes[2])
        {
            EffectManager.Instance.StackOnDeskEffect(effectPoint, Quaternion.identity, FoodTypes.Donut);
        }
        
        if (food.activeFood == food.popcornTypes[0] ||food.activeFood == food.popcornTypes[1] || food.activeFood == food.popcornTypes[2])
        {
            EffectManager.Instance.StackOnDeskEffect(effectPoint, Quaternion.identity, FoodTypes.Popcorn);
        }
        
        if (food.activeFood == food.chipsTypes[0] || food.activeFood == food.chipsTypes[1] || food.activeFood == food.chipsTypes[2] )
        {
            EffectManager.Instance.StackOnDeskEffect(effectPoint, Quaternion.identity, FoodTypes.Chip);
        }
    }
    #endregion

    #region Activator - Customer

    public bool canSale = false;
    [SerializeField] private float decreaseIdle;

    public void GetAllFoods(Customer customer, ActivatorMoney activatorMoney, GameSettings gameSettings)
    {
        SetAnimation(customer);
        objectsOnDesk[objectsOnDesk.Count - 1].transform.DOJump(customer.transform.position, 6.0f, 1, .6f).OnComplete(
            () => ResetObjectTr(customer, activatorMoney, gameSettings));
    }

    void SetAnimation(Customer targetTr)
    {
        targetTr.animator.SetBool("isCarrying", true);
    }

    void ResetObjectTr(Customer customer, ActivatorMoney activatorMoney, GameSettings gameSettings)
    {
        Destroy(objectsOnDesk[objectsOnDesk.Count - 1].GetComponent<Rigidbody>());
        objectsOnDesk[objectsOnDesk.Count - 1].GetComponent<BoxCollider>().enabled = false;
        
        objectsOnDesk[objectsOnDesk.Count - 1].transform.parent = customer.transform;
        objectsOnDesk[objectsOnDesk.Count - 1].transform.localPosition = new Vector3(-.055f, .95f, .5f);
        objectsOnDesk[objectsOnDesk.Count - 1].transform.eulerAngles = new Vector3(0f, 163.967f, 0f);
        //objectsOnDesk[objectsOnDesk.Count - 1].DOScale(.5f, 1f); 

        Sale(customer, activatorMoney, gameSettings);

        objectsOnDesk.Remove(objectsOnDesk[objectsOnDesk.Count - 1]);
    }

    void ResetandAddMoneyTr(GameObject instance, ActivatorMoney activatorMoney)
    {
        collectedMoneyFromCustomers.Add(instance.transform);
        instance.transform.parent = activatorMoney.moneyIndexes[ActivatorMoney.i].transform;
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
    }

    void GoToStartPoint(Customer customer)
    {
        customer.agent.destination = customer.pathway[3].position;
    }

    void Sale(Customer customer, ActivatorMoney activatorMoney, GameSettings gameSettings)
    {
        Food food = objectsOnDesk[objectsOnDesk.Count - 1].GetComponent<Food>();

        if (food.activeFood == food.hamburgerTypes[0] || food.activeFood == food.hamburgerTypes[1] ||
            food.activeFood == food.hamburgerTypes[2])
        {
            UI_Manager.Instance.ActivateHappyIcon(customer);
            collectedHamburgers.Remove(food.transform);
            for (int i = 0; i < HamburgerLevel(gameSettings); i++)
            {
                var diss = 0.7f;
                var randomX = Random.Range(-diss, diss);
                var randomY = Random.Range(-diss, diss);
                var randomPos = new Vector3(randomX, .174f, randomY);

                GameObject instanceMoneyToCustomer = Instantiate(moneyPrefabForCustomer, customer.transform);
                instanceMoneyToCustomer.transform
                    .DOJump(activatorMoney.moneyIndexes[ActivatorMoney.i].position, 2.0f, 1, .7f).OnComplete(() =>
                        {
                            // UI_Manager.Instance.DecreaseFoodCountToUI(food);
                            ResetandAddMoneyTr(instanceMoneyToCustomer, activatorMoney);
                            ActivatorMoney.i++;
                            GoToStartPoint(customer);
                        }
                    );
            }
        }

        if (food.activeFood == food.hotDogTypes[0] || food.activeFood == food.hotDogTypes[1] ||
            food.activeFood == food.hotDogTypes[2])
        {
            UI_Manager.Instance.ActivateHappyIcon(customer);
            collectedHotDogs.Remove(food.transform);
            for (int i = 0; i < HotDogLevel(gameSettings); i++)
            {
                var diss = 0.7f;
                var randomX = Random.Range(-diss, diss);
                var randomY = Random.Range(-diss, diss);
                var randomPos = new Vector3(randomX, .174f, randomY);

                GameObject instanceMoneyToCustomer = Instantiate(moneyPrefabForCustomer, customer.transform);
                instanceMoneyToCustomer.transform
                    .DOJump(activatorMoney.moneyIndexes[ActivatorMoney.i].position, 2.0f, 1, .7f).OnComplete(() =>
                        {
                            // UI_Manager.Instance.DecreaseFoodCountToUI(food);
                            ResetandAddMoneyTr(instanceMoneyToCustomer, activatorMoney);
                            ActivatorMoney.i++;
                            GoToStartPoint(customer);
                        }
                    );
            }
        }

        if (food.activeFood == food.iceCreamTypes[0] || food.activeFood == food.iceCreamTypes[1] ||
            food.activeFood == food.iceCreamTypes[2])
        {
            UI_Manager.Instance.ActivateHappyIcon(customer);
            collectedIceCreams.Remove(food.transform);
            for (int i = 0; i < IceCreamLevel(gameSettings); i++)
            {
                var diss = 0.7f;
                var randomX = Random.Range(-diss, diss);
                var randomY = Random.Range(-diss, diss);
                var randomPos = new Vector3(randomX, .174f, randomY);

                GameObject instanceMoneyToCustomer = Instantiate(moneyPrefabForCustomer, customer.transform);
                instanceMoneyToCustomer.transform
                    .DOJump(activatorMoney.moneyIndexes[ActivatorMoney.i].position, 2.0f, 1, .7f).OnComplete(() =>
                        {
                            // UI_Manager.Instance.DecreaseFoodCountToUI(food);
                            ResetandAddMoneyTr(instanceMoneyToCustomer, activatorMoney);
                            ActivatorMoney.i++;
                            GoToStartPoint(customer);
                        }
                    );
            }
        }

        if (food.activeFood == food.donutTypes[0] || food.activeFood == food.donutTypes[1] ||
            food.activeFood == food.donutTypes[2])
        {
            UI_Manager.Instance.ActivateHappyIcon(customer);
            collectedDonuts.Remove(food.transform);
            for (int i = 0; i < DonutLevel(gameSettings); i++)
            {
                var diss = 0.7f;
                var randomX = Random.Range(-diss, diss);
                var randomY = Random.Range(-diss, diss);
                var randomPos = new Vector3(randomX, .174f, randomY);

                GameObject instanceMoneyToCustomer = Instantiate(moneyPrefabForCustomer, customer.transform);
                instanceMoneyToCustomer.transform.DOJump(activatorMoney.moneyIndexes[ActivatorMoney.i].position, 2.0f, 1, .7f)
                    .OnComplete(() =>
                        {
                            // UI_Manager.Instance.DecreaseFoodCountToUI(food);
                            ResetandAddMoneyTr(instanceMoneyToCustomer, activatorMoney);
                            ActivatorMoney.i++;
                            GoToStartPoint(customer);
                        }
                    );
            }
        }

        if (food.activeFood == food.popcornTypes[0] || food.activeFood == food.popcornTypes[1] ||
            food.activeFood == food.popcornTypes[2])
        {
            UI_Manager.Instance.ActivateHappyIcon(customer);
            collectedPopcorns.Remove(food.transform);
            for (int i = 0; i < PopcornLevel(gameSettings); i++)
            {
                var diss = 0.7f;
                var randomX = Random.Range(-diss, diss);
                var randomY = Random.Range(-diss, diss);
                var randomPos = new Vector3(randomX, .174f, randomY);

                GameObject instanceMoneyToCustomer = Instantiate(moneyPrefabForCustomer, customer.transform);
                instanceMoneyToCustomer.transform.DOJump(activatorMoney.moneyIndexes[ActivatorMoney.i].position, 2.0f, 1, .7f)
                    .OnComplete(() =>
                        {
                            // UI_Manager.Instance.DecreaseFoodCountToUI(food);
                            ResetandAddMoneyTr(instanceMoneyToCustomer, activatorMoney);
                            ActivatorMoney.i++;
                            GoToStartPoint(customer);
                        }
                    );
            }
        }

        if (food.activeFood == food.chipsTypes[0] || food.activeFood == food.chipsTypes[1] ||
            food.activeFood == food.chipsTypes[2])
        {
            UI_Manager.Instance.ActivateHappyIcon(customer);
            collectedChips.Remove(food.transform);
            for (int i = 0; i < ChipLevel(gameSettings); i++)
            {
                var diss = 0.7f;
                var randomX = Random.Range(-diss, diss);
                var randomY = Random.Range(-diss, diss);
                var randomPos = new Vector3(randomX, .174f, randomY);

                GameObject instanceMoneyToCustomer = Instantiate(moneyPrefabForCustomer, customer.transform);
                instanceMoneyToCustomer.transform.DOJump(activatorMoney.moneyIndexes[ActivatorMoney.i].position, 2.0f, 1, .7f)
                    .OnComplete(() =>
                        {
                            // UI_Manager.Instance.DecreaseFoodCountToUI(food);
                            ResetandAddMoneyTr(instanceMoneyToCustomer, activatorMoney);
                            ActivatorMoney.i++;
                            GoToStartPoint(customer);
                        }
                    );
            }
        }
    }

    #region Level Check

    int HamburgerLevel(GameSettings gameSettings)
    {
        switch (gameSettings.hamburgerBuildingIndex)
        {
            case 1:
                return gameSettings.hamburgerPrizeLVL1;
            case 2:
                return gameSettings.hamburgerPrizeLVL2;
            case 3:
                return gameSettings.hamburgerPrizeLVL3;
            default:
                return gameSettings.hamburgerPrizeLVL0;
        }
    }

    int HotDogLevel(GameSettings gameSettings)
    {
        switch (gameSettings.hotdogbuildingIndex)
        {
            case 1:
                return gameSettings.hotDogPrizeLVL1;
            case 2:
                return gameSettings.hotDogPrizeLVL2;
            case 3:
                return gameSettings.hotDogPrizeLVL3;
            default:
                return gameSettings.hotDogPrizeLVL0;
        }
    }

    int IceCreamLevel(GameSettings gameSettings)
    {
        switch (gameSettings.iceCreambuildingIndex)
        {
            case 1:
                return gameSettings.iceCreamPrizeLVL1;
            case 2:
                return gameSettings.iceCreamPrizeLVL2;
            case 3:
                return gameSettings.iceCreamPrizeLVL3;
            default:
                return gameSettings.iceCreamPrizeLVL0;
        }
    }

    int DonutLevel(GameSettings gameSettings)
    {
        switch (gameSettings.donutbuildingIndex)
        {
            case 1:
                return gameSettings.donutPrizeLVL1;
            case 2:
                return gameSettings.donutPrizeLVL2;
            case 3:
                return gameSettings.donutPrizeLVL3;
            default:
                return gameSettings.donutPrizeLVL0;
        }
    }

    int PopcornLevel(GameSettings gameSettings)
    {
        switch (gameSettings.popcornbuildingIndex)
        {
            case 1:
                return gameSettings.popcornPrizeLVL1;
            case 2:
                return gameSettings.popcornPrizeLVL2;
            case 3:
                return gameSettings.popcornPrizeLVL3;
            default:
                return gameSettings.popcornPrizeLVL0;
        }
    }

    int ChipLevel(GameSettings gameSettings)
    {
        switch (gameSettings.chipsbuildingIndex)
        {
            case 1:
                return gameSettings.chipPrizeLVL1;
            case 2:
                return gameSettings.chipPrizeLVL2;
            case 3:
                return gameSettings.chipPrizeLVL3;
            default:
                return gameSettings.chipPrizeLVL0;
        }
    }

    #endregion

    #endregion

    #region Activator - Money w/Desk

    public void TakeAllMonies(Prince prince)
    {
        MoneyAnimationWithCodeButEZ(prince);
    }

    void MoneyAnimationWithCodeButEZ(Prince prince)
    {
        if (collectedMoneyFromCustomers.Count == 0) return;

        int listCount = collectedMoneyFromCustomers.Count;
        float indexMoney = listCount -= 1;

        var obj = collectedMoneyFromCustomers[(int)indexMoney];
        obj.parent = null;

        float randY = Random.Range(4f, 5.2f);
        float randZ = Random.Range(72f, 76f);

        Vector3 randPos = new Vector3(obj.transform.localPosition.x, randY, randZ);


        obj.DOLocalMove(randPos, .5f).OnComplete(() => { StartCoroutine(MoneyMoveToPlayer(obj, prince)); }
        );

        collectedMoneyFromCustomers.Remove(collectedMoneyFromCustomers[(int)indexMoney]);
    }

    IEnumerator MoneyMoveToPlayer(Transform objTr, Prince prince)
    {
        bool isMoving = true;

        while (isMoving)
        {
            objTr.position = Vector3.MoveTowards(objTr.position, prince.playerCollectedMoneyParent.position,
                moneyMoveSpeed * Time.deltaTime);

            if (IsArrivedToTheFirstPosition(objTr.position, prince.playerCollectedMoneyParent.position))
            {
                objTr.parent = prince.playerCollectedMoneyParent;
                objTr.gameObject.SetActive(false);

                moneys.Add(objTr);

                playerSettings.takedDeck++;

                UI_Manager.Instance.IncreasePlayerMoney();

                isMoving = false;
            }

            yield return null;
        }
    }

    bool IsArrivedToTheFirstPosition(Vector3 shieldPos, Vector3 targetPos)
    {
        Vector3 offset = targetPos - shieldPos;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen < 0.2f * 0.2f)
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Throw to Food Area

    public void Throw(CkyBehaviour bhv)
    {
        FoodAnimationWithCodeButEZ(bhv);
    }

    void FoodAnimationWithCodeButEZ(CkyBehaviour bhv)
    {
        if (standHamburgerFoods.Count == 0) return;

        int listCount = standHamburgerFoods.Count-1;
        float indexMoney = listCount -= 1;

        var obj = standHamburgerFoods[(int)indexMoney];
        obj.parent = null;

        float randY = Random.Range(4f, 5.2f);
        float randZ = Random.Range(72f, 76f);

        Vector3 randPos = new Vector3(obj.transform.localPosition.x, randY, randZ);


        obj.DOLocalMove(randPos, .5f).OnComplete(() => { StartCoroutine(FoodAnimation(obj,bhv)); }
        );

        standHamburgerFoods.Remove(standHamburgerFoods[(int)indexMoney]);
    }

    IEnumerator FoodAnimation(Transform objTr,CkyBehaviour bhv)
    {
        bool isMoving = true;

        while (isMoving)
        {
            objTr.position = Vector3.MoveTowards(objTr.position, bhv.transform.position+new Vector3(0f,posToStackY,0f),
                moneyMoveSpeed * Time.deltaTime);

            if (IsArrivedToTheFirstPosition(objTr.position, bhv.transform.position+new Vector3(0f,posToStackY,0f)))
            {
                objTr.parent = bhv.transform;
                posToStackY += 1f;
                // FoodArea.indexStand--;
                isMoving = false;
            }
            yield return null;
        }
    }
    #endregion
}