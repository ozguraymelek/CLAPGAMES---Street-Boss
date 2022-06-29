using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IStackable
{
    [Header("References")]
    private Prince _prince;
    
    [Header("Components")]
    [Space]
    private Rigidbody _rb;
    private BoxCollider _coll;
    [SerializeField] internal GameObject[] hamburgerTypes;
    [SerializeField] internal GameObject[] hotDogTypes;
    [SerializeField] internal GameObject[] iceCreamTypes;
    [SerializeField] internal GameObject[] donutTypes;
    [SerializeField] internal GameObject[] popcornTypes;
    [SerializeField] internal GameObject[] chipsTypes;

    public GameObject activeFood;

    [Header("Settings")]
    [Space]
    public bool isOnList;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _coll = GetComponent<BoxCollider>();
        _prince = FindObjectOfType<Prince>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOnList == false) return;

        IStackable _stackable = other.GetComponent<IStackable>();

        if (_stackable == null) return;

        _stackable.Stack();
    }

    void IStackable.Stack()
    {
        if (isOnList == true) return;

        OnList(true);
        
        StackManager.Instance.AddFood(this);
    }

    void IStackable.Scatter()
    {
        StackManager.Instance.BeScattered(this);
    }

    public void FoodOnScatter()
    {
        OnList(false);

        float _randomX = Random.Range(-2.5f, 2.5f);
        _rb.AddForce(new Vector3(_randomX, 2, _randomX), ForceMode.Impulse);
        _rb.useGravity = true;
    }

    private void OnList(bool _b)
    {
        isOnList = _b;
        _coll.isTrigger = _b;
        _rb.useGravity = !_b;

        if (_b == false)
        {
            _rb.detectCollisions = false;
            StartCoroutine(Delayed());
        }
    }

    IEnumerator Delayed()
    {
        yield return new WaitForSeconds(0.5f);

        _rb.detectCollisions = true;
    }

    void IStackable.Change(Door.DoorType _doorType, GameSettings gameSettings)
    {
        activeFood.SetActive(false);

        switch (_doorType)
        {
            case Door.DoorType.Hamburger:
                HamburgerInfo(gameSettings);
                break;
            case Door.DoorType.HotDog:
                HotDogInfo(gameSettings);
                break;
            case Door.DoorType.IceCream:
                IceCreamInfo(gameSettings);
                break;
            case Door.DoorType.Chips:
                ChipInfo(gameSettings);
                break;
            case Door.DoorType.Donut:
                DonutInfo(gameSettings);
                break;
            case Door.DoorType.Popcorn:
                PopcornInfo(gameSettings);
                break;
        }

        activeFood.SetActive(true);
    }

    #region Door Activator - Hamburger
    private int GetHamburgerLevel(GameSettings gameSettings)
    {
        return gameSettings.hamburgerBuildingIndex;
    }
    private void HamburgerInfo(GameSettings gameSettings)
    {
        switch (GetHamburgerLevel(gameSettings))
        {
            case 1:
                activeFood = hamburgerTypes[0];
                print("hamburger changed to lvl 1");
                break;
            case 2:
                activeFood = hamburgerTypes[1];
                print("hamburger changed to lvl 2");
                break;
            case 3:
                activeFood = hamburgerTypes[2];
                print("hamburger changed to lvl 3");
                break;

            default:
                activeFood = hamburgerTypes[0];
                print("hamburger changed to lvl 0");
                break;
        }
        transform.tag = "Hamburger";
    }
    #endregion

    #region Door Activator - Hot Dog
    private int GetHotDogLevel(GameSettings gameSettings)
    {
        return gameSettings.hotdogbuildingIndex;
    }
    private void HotDogInfo(GameSettings gameSettings)
    {
        activeFood.tag = "HotDog";
        switch (GetHotDogLevel(gameSettings))
        {
            case 0:
                activeFood = hotDogTypes[0];
                print("Hot Dog changed to lvl 0");
                break;
            case 1:
                activeFood = hotDogTypes[0];
                print("Hot Dog changed to lvl 1");
                break;
            case 2:
                activeFood = hotDogTypes[1];
                print("Hot Dog changed to lvl 2");
                break;
            case 3:
                activeFood = hotDogTypes[2];
                print("Hot Dog changed to lvl 3");
                break;
        }
        transform.tag = "Hot Dog";
    }
    #endregion

    #region Door Activator - Ice Cream
    private int GetIceCreamLevel(GameSettings gameSettings)
    {
        return gameSettings.iceCreambuildingIndex;
    }
    private void IceCreamInfo(GameSettings gameSettings)
    {
        activeFood.tag = "IceCream";
        switch (GetIceCreamLevel(gameSettings))
        {
            case 0:
                activeFood = iceCreamTypes[0];
                print("Ice Cream changed to lvl 0");
                break;
            case 1:
                activeFood = iceCreamTypes[0];
                print("Ice Cream changed to lvl 1");
                break;
            case 2:
                activeFood = iceCreamTypes[1];
                print("Ice Cream changed to lvl 2");
                break;
            case 3:
                activeFood = iceCreamTypes[2];
                print("Ice Cream changed to lvl 3");
                break;
        }
        transform.tag = "Ice Cream";
    }
    #endregion

    #region Door Activator - Donut
    private int GetDonutLevel(GameSettings gameSettings)
    {
        return gameSettings.donutbuildingIndex;
    }
    private void DonutInfo(GameSettings gameSettings)
    {
        activeFood.tag = "Donut";
        switch (GetDonutLevel(gameSettings))
        {
            case 0:
                activeFood = donutTypes[0];
                print("Donut changed to lvl 0");
                break;
            case 1:
                activeFood = donutTypes[0];
                print("Donut changed to lvl 1");
                break;
            case 2:
                activeFood = donutTypes[1];
                print("Donut changed to lvl 2");
                break;
            case 3:
                activeFood = donutTypes[2];
                print("Donut changed to lvl 3");
                break;
        }
        transform.tag = "Donut";
    }
    #endregion

    #region Door Activator - Popcorn
    private int GetPopcornLevel(GameSettings gameSettings)
    {
        return gameSettings.popcornbuildingIndex;
    }
    private void PopcornInfo(GameSettings gameSettings)
    {
        activeFood.tag = "Popcorn";
        switch (GetPopcornLevel(gameSettings))
        {
            case 0:
                activeFood = popcornTypes[0];
                print("Popcorn changed to lvl 0");
                break;
            case 1:
                activeFood = popcornTypes[0];
                print("Popcorn changed to lvl 1");
                break;
            case 2:
                activeFood = popcornTypes[1];
                print("Popcorn changed to lvl 2");
                break;
            case 3:
                activeFood = popcornTypes[2];
                print("Popcorn changed to lvl 3");
                break;
        }
        transform.tag = "Popcorn";
    }
    #endregion

    #region Door Activator - Chip
    private int GetChipLevel(GameSettings gameSettings)
    {
        return gameSettings.chipsbuildingIndex;
    }
    private void ChipInfo(GameSettings gameSettings)
    {
        activeFood.tag = "Chip";
        switch (GetChipLevel(gameSettings))
        {
            case 0:
                activeFood = chipsTypes[0];
                print("Chip changed to lvl 0");
                break;
            case 1:
                activeFood = chipsTypes[0];
                print("Chip changed to lvl 1");
                break;
            case 2:
                activeFood = chipsTypes[1];
                print("Chip changed to lvl 2");
                break;
            case 3:
                activeFood = chipsTypes[2];
                print("Chip changed to lvl 3");
                break;
        }
        transform.tag = "Chip";
    }
    #endregion
}