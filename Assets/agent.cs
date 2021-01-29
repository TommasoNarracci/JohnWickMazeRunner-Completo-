using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;

public class agent : Agent
{
    public int score = 0;
    public Transform shotpoint;
    public float speed = 3f;
    public float rotationSpeed = 3f;
    public int timeBetweenShots = 50;
    public int damage = 100;
    public bool shotReady = true;
    public int shotwait = 0;
    public Vector3 startPos;
    public Rigidbody RB;
    private EnvironmentParameters EnvironmentParameters;
    public event Action OnEnvironmentReset;
    /*public void Shoot()
    {
        if (!shotReady)
        {
            //Debug.Log("Non pronto");
            return;
        }
        var direction = transform.forward;
        var layerMask = 1 << LayerMask.NameToLayer("Enemy");
        //Debug.Log("Sparo");
        Debug.DrawRay(shotpoint.position, direction * 50f, Color.green, 2f);
        if (Physics.Raycast(shotpoint.position, direction, out var hit, 50f, layerMask))
        {
            Debug.Log("Colpito");
            hit.transform.GetComponent<Enemy>().getShot(damage, this);
        }
        else
        {
            AddReward(-0.033f);
        }
        shotwait = timeBetweenShots;
        shotReady = false;
    }
    public void KillCount()
    {
        score++;
        AddReward(1.0f);
        Debug.Log("Fine Episodio");
        EndEpisode();
    }*/
    public void FixedUpdate()
    {
       /* if (!shotReady)
        {
            shotwait--;
            if (shotwait <= 0)
            {
                shotReady = true;
            }
        }*/
       // AddReward(-1f / MaxStep);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        //Debug.Log(vectorAction[0]);
       /* if (Mathf.RoundToInt(vectorAction[0]) >= 1)
        {
            Shoot();
        }*/
        RB.velocity = new Vector3(vectorAction[0] * speed, 0f, vectorAction[1] * speed);
        transform.Rotate(Vector3.up, vectorAction[3] * rotationSpeed);
        //RB.velocity = new Vector3(vectorAction[1] * speed, 0f, vectorAction[2] * speed);
        /*switch (vectorAction[1])
        {
            case 1:
                RB.velocity = new Vector3(1* speed, 0, 0);
                break;
            case 2:
                RB.velocity = new Vector3(0, 0,1 * speed);
                break;
            case 3:
                RB.velocity = new Vector3(-1 *speed, 0, 0);
                break;
            case 4:
                RB.velocity = new Vector3(0, 0, -1 * speed);
                break;
            default:break;
        }
        Debug.Log(vectorAction[1]);*/
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(shotReady);
    }
    public override void Initialize()
    {
        startPos = transform.position;
        //startPos = transform.InverseTransformDirection(startPos);
        RB = GetComponent<Rigidbody>();
        EnvironmentParameters = Academy.Instance.EnvironmentParameters;
        RB.freezeRotation = true;//da togliere poi
    }
    public override void Heuristic(float[] actionsOut)
    {
        //actionsOut[0] = Input.GetKey(KeyCode.P) ? 1f : 0f;
        actionsOut[0] = Input.GetAxis("Vertical");
        actionsOut[1] = -Input.GetAxis("Horizontal");
       // actionsOut[3] = Input.GetAxis("Horizontal");
        /*actionsOut[1] = 0;
        if (Input.GetKey(KeyCode.W)){
            actionsOut[1] = 1;
        }
        if (Input.GetKey(KeyCode.A)){
            actionsOut[1] = 2;
        }
        if (Input.GetKey(KeyCode.S)){
            actionsOut[1] = 3;
        }
        if (Input.GetKey(KeyCode.D)){
            actionsOut[1] = 4;
        }*/
    }
    public override void OnEpisodeBegin()
    {
        OnEnvironmentReset?.Invoke();
        Debug.Log("Inizio Episodio");
        transform.position = startPos;
        RB.velocity = Vector3.zero;
        shotReady = true;
    }
    /* public void OnMouseDown()
     {
         Shoot();
     }*/
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
