using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour, ICustomer
{
    [Header("Components")]
    public NavMeshAgent agent;
    public List<Transform> money;
    public Animator animator;
    public Transform foodStackPoint;

    [Header("Settings")]
    [Space]
    public List<Transform> pathway;

    public bool interacted = false;

    public Transform canvas;
    public GameObject happyIcon;
    public GameObject angryIcon;
    public List<GameObject> iconsHolder;
    public int randPos;

    private void Start()
    {
        
        pathway[0] = GameObject.FindWithTag("F*").transform;
        pathway[1] = GameObject.FindWithTag("S*").transform;
        pathway[2] = GameObject.FindWithTag("Desk").transform;
        pathway[3] = GameObject.FindWithTag("FinishPoint").transform;

        randPos = Random.Range(0, 2);
        agent.SetDestination(pathway[randPos].position);
    }

    private void Update()
    {
        Check();
        
        canvas.rotation = Camera.main.transform.rotation;
    }

    public void Check()
    {
        float dist = Vector3.Distance(agent.transform.position, pathway[randPos].position);
        float distAgentTo2 = Vector3.Distance(agent.transform.position, pathway[2].position);


        if (dist <= .1f)
        {
            agent.destination = pathway[2].position;
        }

        if (distAgentTo2 <= .1f)
        {
            if (StackManager.Instance.objectsOnDesk.Count == 0)
            {
                UI_Manager.Instance.ActivateAngryIcon(this);

                agent.destination = agent.destination = pathway[3].position;
            }
        }
    }
}
