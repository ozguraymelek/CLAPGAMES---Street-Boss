using System.Collections;
using System;
using UnityEngine;
using EZ_Pooling;
using DG.Tweening;

public class CkyBehaviour : MonoBehaviour
{
    [SerializeField] bool isPressing = false;
    private float _screenWidthMultiplier, _screenHeightMultiplier;
    //public int bulletDamage;
    public float runnerSpeed;
    [SerializeField] float runnerBoundX = 3.5f;
    //private float _initRunnerSpeed;
    private float _speedDecreaseFactor = 120, _speedIncreaseFactor = 80;
    public float sensivity;
    public Transform stackFirstPointTrForRunner, stackFirstPointTrForIdle;
    private JoystickPlayer _joystickPlayer;
    private CapsuleCollider _collider;

    [SerializeField] GameObject runnerPlayer, idlePlayer;

    private void Awake()
    {
        GetComponents();
        SubscribeEvents();

        _screenWidthMultiplier = 1.0f / Screen.width;
        _screenHeightMultiplier = 1.0f / Screen.height;
    }

    private void GetComponents()
    {
        _joystickPlayer = GetComponent<JoystickPlayer>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void OnDisable()
    {

    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }
    private void SubscribeEvents()
    {
        CkyEvents.OnStart += OnStart;
        CkyEvents.OnInteractWithObstacle += OnInteractWithObstacle;
        CkyEvents.OnTransToRunner += OnTransitionToRunner;
        CkyEvents.OnTransToIdle += OnTransitionToIdle;

        InputHandler.PointerMoved += OnPointerMoved;
        InputHandler.PointerPressed += OnPointerPressed;
        InputHandler.PointerRemoved += OnPointerRemoved;
    }

    #region On Transition To Idle

    private void OnTransitionToIdle()
    {
        CkyEvents.OnUpdate -= MoveForward;
        InputHandler.PointerMoved -= OnPointerMoved;
        _collider.isTrigger = false;

        HandHittheGround();

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    /// <summary>
    ///     its called from 'Idle' animation event.
    /// </summary>
    private void SetAnimationLayerWeight()
    {
        if (!StackManager.Instance.foods.Equals(null))
        {
            print("Food-List is not empty!");
            AnimationController.SetLayerWeight(GetComponent<Animator>(),"isCarrying", true);
        }
        else
        {
            print("Food-List is empty!");
            AnimationController.SetLayerWeight(GetComponent<Animator>(), "isCarrying", false);
        }
    }
    private void HandHittheGround()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(runnerPlayer.transform.GetChild(0).DORotate(new Vector3(0, 270, 50), 0.6f, RotateMode.FastBeyond360));
        _seq.Append(runnerPlayer.transform.GetChild(0).DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.FastBeyond360));
        _seq.OnComplete(() => ChangePlayerRunnerToIdle());
    }

    private void ChangePlayerRunnerToIdle()
    {
        runnerPlayer.SetActive(false);
        _joystickPlayer.anim.enabled = true;
        idlePlayer.SetActive(true);

        AnimationController.SetTrigger(_joystickPlayer.anim, "isJumping");

        EffectManager.Instance.PopEffect(transform.position + new Vector3(0, 0.5f, 1), Quaternion.identity);

        Vector3 targetPos = transform.position + new Vector3(0, 0, 10);
        targetPos.x = 0;

        

        transform.DOJump(targetPos, 4, 1, 1.5f).OnComplete(() =>
        {
            //AnimationController.SetBool(_joystickPlayer.anim, "isJumping", false);
            ActivateJoystick(true);
            StackForIdle();
        });
    }

    #endregion

    #region On Transition To Runner

    private void OnTransitionToRunner()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);

        ActivateJoystick(false);

        _collider.isTrigger = true;

        PrinceHittheGround();
    }
    private void PrinceHittheGround()
    {
        Sequence _seq = DOTween.Sequence();

        _joystickPlayer.anim.SetTrigger("isFalling");
        _seq.Append(transform.DOJump(transform.position + new Vector3(0, 0, 5), 6, 1, 1.5f));
        _seq.OnComplete(() => ChangePlayerIdleToRunner());
    }

    private void ChangePlayerIdleToRunner()
    {
        transform.localPosition = Vector3.zero;

        runnerPlayer.SetActive(true);
        idlePlayer.SetActive(false);

        EffectManager.Instance.PopEffect(transform.position + new Vector3(0, 0.5f, 1), Quaternion.identity);


        CkyEvents.OnUpdate += MoveForward;
        InputHandler.PointerMoved += OnPointerMoved;
    }

    #endregion

    private void ActivateJoystick(bool b)
    {
        _joystickPlayer.enabled = b;
    }

    private void StackForIdle()
    {
        StackManager.Instance.StackForIdle();
    }

    //ıenumerator delayedtransitiontoıdle()
    //{
    //    yield return new waitforseconds(1);
    //}

    private void OnInteractWithObstacle()
    {
        float _initSpeed = runnerSpeed;

        StartCoroutine(DecreaseRunnerSpeed(_initSpeed));
    }
    public void ObstacleMovementKnife(Obstacle obstacle)
    {
        obstacle.transform.parent.DORotateQuaternion(Quaternion.Euler(0f, 0f, 43.2f), 1.2f).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnStart()
    {
        CkyEvents.OnUpdate += MoveForward;
    }

    private void UnSubscribeEvents()
    {
        CkyEvents.OnUpdate -= MoveForward;
        CkyEvents.OnInteractWithObstacle -= OnInteractWithObstacle;

        InputHandler.PointerMoved -= OnPointerMoved;
        InputHandler.PointerPressed -= OnPointerPressed;
        InputHandler.PointerRemoved -= OnPointerRemoved;
    }

    private void OnPointerPressed(Vector3 obj)
    {
        isPressing = true;

        //PointManager.Instance.PointPopUp(transform.position + new Vector3(0.4f, 1.75f, 0), 100);
        //EffectManager.Instance.PopEffect(transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
        //SoundManager.Instance.PopSound3D(transform.position);

        //SpawnBullet();
    }

    private void OnPointerRemoved(Vector3 obj)
    {
        isPressing = false;
    }

    //private void SpawnBullet()
    //{
    //    EffectManager.Instance.Bullet(transform.position + new Vector3(0, 1.75f, 0), transform.eulerAngles, bulletDamage);
    //}

    private void OnPointerMoved(Vector3 mouseMovementDirection)
    {
        BasicMove(mouseMovementDirection);
    }

    private void BasicMove(Vector3 mouseMovementDirection)
    {
        Vector3 mouseToWorldDirection = new Vector3(mouseMovementDirection.x * _screenWidthMultiplier, 0, 0);
        Vector3 addVector = mouseToWorldDirection * Time.deltaTime * sensivity;
        transform.position += addVector;

        Transform thisTr = transform;
        if (thisTr.position.x < -runnerBoundX)
            thisTr.position = new Vector3(-runnerBoundX, thisTr.position.y, thisTr.position.z);
        if (thisTr.position.x > runnerBoundX)
            thisTr.position = new Vector3(runnerBoundX, thisTr.position.y, thisTr.position.z);
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * runnerSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        IStackable _interactable = other.GetComponent<IStackable>();

        if (_interactable == null) return;

        _interactable.Stack();
    }


    IEnumerator DecreaseRunnerSpeed(float _initSpeed)
    {
        bool a = true;

        while (a == true)
        {
            runnerSpeed -= Time.deltaTime * _speedDecreaseFactor;

            if (runnerSpeed <= -_initSpeed)
            {
                StartCoroutine(IncreaseRunnerSpeed(_initSpeed));

                a = false;
            }
            yield return null;
        }
    }
    IEnumerator IncreaseRunnerSpeed(float _initSpeed)
    {
        bool a = true;

        while (a == true)
        {
            runnerSpeed += Time.deltaTime * _speedIncreaseFactor;

            if (runnerSpeed >= _initSpeed)
            {
                runnerSpeed = _initSpeed;

                a = false;
            }
            yield return null;
        }
    }
}