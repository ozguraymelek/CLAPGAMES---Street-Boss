//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using EZ_Pooling;

//public class Bullet : MonoBehaviour
//{
//    [System.NonSerialized] public Vector3 targetPos;
//    [SerializeField] private Rigidbody _rb;

//    public int damage;


//    Vector3 direc;

//    private void Start()
//    {
//        _rb = GetComponent<Rigidbody>();
//    }

//    private void OnEnable()
//    {
//        //StartCoroutine(Delay());
//    }

//    IEnumerator Delay()
//    {
//        yield return null;
//        AddForce();
//    }

//    private void AddForce()
//    {
//        _rb.velocity = Vector3.zero;
//        _rb = GetComponent<Rigidbody>();
//        direc = (targetPos - transform.position).normalized;
//        _rb.AddForce(direc * 35, ForceMode.Impulse);
//        transform.LookAt(targetPos);
//        transform.eulerAngles += new Vector3(-90, 0, 0);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // Layerlarla kendine vurmasý engellendi.
//        Debug.Log("Layerlarý ayarla !");

//        //IDamageable damageableObject = other.GetComponent<IDamageable>();

//        //if (damageableObject == null)
//        //    return;

//        //damageableObject.GetDamage(damage, transform.position, transform.eulerAngles);

//        DeSpawn();
//    }

//    private void DeSpawn()
//    {
//        EZ_PoolManager.Despawn(transform);
//    }
//}