using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManagerWithJoint : Singleton<StackManagerWithJoint>
{
    [SerializeField] List<Transform> _objects;
    public int _count;
    [SerializeField] Transform[] _stackTransforms;
    private ParticleSystem[] _fogParticles;
    public int _maxStack = 30;

    [Header("Joint Operations")]
    [SerializeField] Transform _jointParent;
    private int _jointCount;
    private List<ConfigurableJoint> _joints = new List<ConfigurableJoint>();

    private void Start()
    {
        GetJoints();
        GetFogParticles();

        CkyEvents.OnFail += OnPlayerFail;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))

            StartCoroutine(CloseShieldsVisible());
        if (Input.GetKeyDown(KeyCode.N))
            StartCoroutine(OpenShieldsVisible());
    }
    private void OnCapturedFlag()
    {
        StartCoroutine(CloseShieldsVisible());
    }

    #region Visible Operations

    IEnumerator CloseShieldsVisible()
    {
        if (_count <= 0) yield break;

        for (int i = _count - 1; i >= 0; i--)
        {
            _objects[i].gameObject.SetActive(false);
            _objects[i].localScale = Vector3.one * 2.0f;

            yield return new WaitForSeconds(0.02f);
            _objects[i].localScale = Vector3.one * 1.0f;

            //EffectManager.Instance.FogGo(_objects[i].transform.position);
        }
    }
    IEnumerator OpenShieldsVisible()
    {
        for (int i = 0; i < _count; i++)
        {
            _objects[i].gameObject.SetActive(true);
            _objects[i].localScale = Vector3.one * 2.0f;

            yield return new WaitForSeconds(0.02f);
            _objects[i].localScale = Vector3.one * 1.0f;

            //EffectManager.Instance.FogGo(_objects[i].transform.position);
        }
    }

    #endregion

    private void GetJoints()
    {
        _jointCount = _jointParent.childCount;
        for (int i = 0; i < _jointCount; i++)
        {
            _joints.Add(_jointParent.GetChild(i).GetComponent<ConfigurableJoint>());
        }
    }

    private void GetFogParticles()
    {
        _fogParticles = new ParticleSystem[_stackTransforms.Length];

        for (int i = 0; i < _stackTransforms.Length; i++)
            _fogParticles[i] = _stackTransforms[i].GetComponentInChildren<ParticleSystem>();
    }

    #region Gathering

    public bool IsPlayerFull()
    {
        if (_count >= _maxStack)
            return true;

        return false;
    }

    public bool CanAdd() // Check It On ShieldCollectible
    {
        if (_maxStack <= _count)
            return false;

        return true;
    }

    public void AddObject(Transform shieldTr)
    {
        _count++;
        _objects.Add(shieldTr);

        StartCoroutine(ShieldMove(shieldTr, _count - 1));
    }

    IEnumerator ShieldMove(Transform shieldTr, int index)
    {
        float duration = 0.7f + index * 0.01f;
        shieldTr.GetComponent<BoxCollider>().enabled = false;
        // shieldTr.DORotate(new Vector3(360, 0, 0), duration, RotateMode.FastBeyond360);
        Vector3 shieldFirstPos = shieldTr.transform.position;
        bool isShieldFlying = true;
        float t = 0;

        while (isShieldFlying)
        {
            t += Time.deltaTime;
            float arriveTime = t / duration;

            Vector3 a = shieldFirstPos;
            Vector3 b = _stackTransforms[index].position;
            Vector3 pos = Vector3.Lerp(a, b, arriveTime);
            Vector3 arc;

            // shieldTr.transform.position = pos;
            if (arriveTime <= duration)
            {
                arc = Vector3.up * (2.0f + index * 0.05f) * Mathf.Sin(arriveTime * 3.14f);
                shieldTr.transform.position = pos + arc;

                if (IsArrivedToTheFirstPosition(shieldTr.position, _stackTransforms[index].position) == true)
                {
                    shieldTr.parent = _stackTransforms[index];
                    shieldTr.localPosition = Vector3.zero;
                    shieldTr.localEulerAngles = Vector3.zero;

                    WhenArrived(index);

                    isShieldFlying = false;
                }
            }
            if (arriveTime > duration)
            {
                StartCoroutine(Movee(shieldTr, index));
                isShieldFlying = false;
            }
            yield return null;
        }
    }

    private void WhenArrived(int i)
    {
        //PointManager.Instance.AddPoint();

        //SoundManager.Instance.Pop(transform.position);
        Boing();
        // EffectManager.Instance.Fog(_stackTransforms[_count - 1].position, _stackTransforms[_count - 1].eulerAngles);
        _fogParticles[i].Play();

        //UIManager.Instance.IncreaseQuantityOf(UIManager.IconTypes.ShieldIcon);
    }

    IEnumerator Movee(Transform shieldTr, int index)
    {
        bool isMoving = true;

        while (isMoving)
        {
            shieldTr.position = Vector3.MoveTowards(shieldTr.position, _stackTransforms[index].position, 30 * Time.deltaTime);

            if (IsArrivedToTheFirstPosition(shieldTr.position, _stackTransforms[index].position) == true)
            {
                shieldTr.parent = _stackTransforms[index];
                shieldTr.localPosition = Vector3.zero;
                shieldTr.localEulerAngles = Vector3.zero;

                WhenArrived(index);

                isMoving = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public bool IsArrivedToTheFirstPosition(Vector3 shieldPos, Vector3 targetPos)
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

    #region Using

    public void RemoveObject(Transform targetTr)
    {
        if (0 == _count) return;

        _count--;

        //PointManager.Instance.RemovePoint();

        var objTr = _objects[_count];
        objTr.parent = null;
        GoToTheActivator(objTr, targetTr);

        _objects.Remove(_objects[_count]);

        // if (_count == 0)
        //     joystickPlayer.ActivateCarryingMode(false);

        //UIManager.Instance.DecreaseQuantityOf(UIManager.IconTypes.ShieldIcon);
    }

    private void GoToTheActivator(Transform objTr, Transform targetTr)
    {
        var diss = 0.7f;
        var randomX = Random.Range(-diss, diss);
        var randomY = Random.Range(-diss, diss);
        var randomPos = new Vector3(randomX, 0, randomY);

        objTr.DOJump(targetTr.position + randomPos, 6.0f, 1, 0.8f).OnComplete(
            () =>
            {
                objTr.gameObject.SetActive(false);

                //SoundManager.Instance.UseShield(targetTr.position);
                //EffectManager.Instance.ShieldFountainSingle(targetTr.position + randomPos);
            });

        objTr.DORotate(new Vector3(180, Random.Range(-25, 25), 0), 0.7f, RotateMode.FastBeyond360);
    }

    #endregion

    #region Boing

    private void Boing()
    {
        for (int i = 0; i < _jointCount; i++)
        {
            _joints[i].connectedAnchor = new Vector3(0, 0.17f, 0);
        }

        StartCoroutine(BoingBack());
    }

    IEnumerator BoingBack()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _jointCount; i++)
        {
            _joints[i].connectedAnchor = new Vector3(0, 0.2f, 0);
        }
    }

    #endregion


    #region On Player Fail

    private void OnPlayerFail()
    {
        BeScattered();
    }

    private void BeScattered()
    {
        var c = _objects.Count;

        for (int i = 0; i < c; i++)
        {
            var objTr = _objects[0];
            _objects.Remove(objTr);
            objTr.parent = null;
            _count--;

            Rigidbody objRb = objTr.gameObject.AddComponent<Rigidbody>();
            objRb.AddForce(new Vector3(Random.Range(-3, 3), 1, Random.Range(-3, 3)), ForceMode.Impulse);

            StartCoroutine(Delayed_ActivateCollider(objTr));
        }
    }

    private IEnumerator Delayed_ActivateCollider(Transform objTr)
    {
        yield return new WaitForSeconds(0.15f);

        // BoxCollider objCollider = objTr.GetComponent<BoxCollider>();
        // objCollider.enabled = true;
        // objCollider.isTrigger = false;

        MeshCollider objCollider = objTr.GetChild(0).gameObject.AddComponent<MeshCollider>();
        objCollider.convex = true;
    }

    #endregion

    /////////////////////////////
    ///

    //public void ForTutorial0()
    //{
    //    if (_count == 10)
    //    {
    //        FindObjectOfType<Tutorial0>().InvokeTutorial0();

    //        CkyEvents.ckyOnUpdate -= ForTutorial0;
    //    }
    //}
}