using EZ_Pooling;
using UnityEngine;

public class JoystickPlayer : Singleton<JoystickPlayer>
{
    [Header("Scriptable Object References")] [SerializeField]
    private PlayerSettings playerSettings;
    
    public float speed;
    public float rotationSpeed;
    public DynamicJoystick dynamicJoystick;
    public Rigidbody rb;
    [SerializeField] internal Animator anim;
    Vector3 _velocity;

    [Header("Components")]
    [SerializeField] internal Transform playerMoneyParentTr;

    [SerializeField] internal Transform dollarPrefab;
    private void Start()
    {
        GetComponents();
        SpawnMoney();
    }

    private void OnEnable()
    {
        SubscribeEvents();
        dynamicJoystick.gameObject.SetActive(true);
    }

    private void SpawnMoney()
    {
        for (int i = 0; i < playerSettings.takedDeck; i++)
        {
            Transform instanceDollar = EZ_PoolManager.Spawn(dollarPrefab, Vector3.zero, Quaternion.identity);
            instanceDollar.parent = playerMoneyParentTr;
            instanceDollar.localPosition = Vector3.zero;
            
            StackManager.Instance.moneys.Add(instanceDollar);
            
            instanceDollar.gameObject.SetActive(false);
        }
    }
    private void GetComponents()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        transform.eulerAngles = Vector3.zero;

        UnSubscribeEvents();
        dynamicJoystick.gameObject.SetActive(false);
    }

    internal void SubscribeEvents()
    {
        CkyEvents.OnFixedUpdate += MyUpdate;
    }

    internal void UnSubscribeEvents()
    {
        CkyEvents.OnFixedUpdate -= MyUpdate;
    }

    private void MyUpdate()
    {
        JoystickControl();
    }

    public void JoystickControl()
    {
        Vector3 direction = (Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, rotationSpeed * Time.deltaTime);

            AnimationController.SetFloat(anim, "Direction", direction.magnitude);
            rb.velocity = transform.GetChild(0).forward * speed * Time.fixedDeltaTime;
            _velocity = rb.velocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
            _velocity = rb.velocity;
            AnimationController.SetFloat(anim, "Direction", direction.magnitude);
            direction = Vector3.zero;
        }
    }

    private void RotateToVelocityDirection()
    {
        if (_velocity != Vector3.zero)
        {
            //ragdollToggle.transform.rotation = Quaternion.LookRotation(_velocity, Vector3.up);
        }
    }

    private void OnSuccess()
    {
        Debug.Log("Joystick Player - On Success");
        CkyEvents.OnFixedUpdate -= MyUpdate;
    }

    private void OnFail()
    {
        Debug.Log("Joystick Player - On Fail");
        CkyEvents.OnFixedUpdate -= MyUpdate;
    }
}