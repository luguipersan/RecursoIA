using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
public class SniperScript : MonoBehaviour
{
    public GameObject player;

    public Vector3 lastPlayerPos;

    public float speed = 1f;

    public Vector3 Display;

    public bool visao;


    public bool isLocked = false;
    public GameObject bulletSpawn;
    public GameObject bullet;

    PortaBehavior portaControl = new PortaBehavior();

    public float rateOfFire = 0.1f;
    public float recharge;

    public bool podeAtirar = true;


    [Task]
    void lockDoors()
    {
        Task.current.Succeed();
    }

    [Task]
    bool CanSeePlayer()
    {
        if (visao == true)
        {
            lastPlayerPos = player.transform.position;
            this.transform.LookAt(player.transform);

            if (recharge >= 0)
            {
                podeAtirar = false;
                recharge -= Time.deltaTime;
            }
            if (recharge <= 0)
            {
                podeAtirar = true;
            }

            if (podeAtirar)
            {
                Atirar();
                recharge = rateOfFire;
            }

            Task.current.Succeed();
            return true;
        }
        else
        {
            Task.current.Fail();
            return false;
        }
    }
    void Atirar()
    {
        Instantiate(bullet.transform, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
    }

    [Task]
    void Snipe()
    {
        Task.current.Succeed();
    }

    Vector3 randomPos(Vector3 LastPlayerPos)
    {
        Vector3 minRange = new Vector3(LastPlayerPos.x, LastPlayerPos.y, LastPlayerPos.z);
        Vector3 maxRange = new Vector3(LastPlayerPos.x + 20, LastPlayerPos.y, LastPlayerPos.z + 20);

        return new Vector3(Random.Range(minRange.x, maxRange.x), Random.Range(minRange.y, maxRange.y), Random.Range(minRange.z, maxRange.z));
    }

    [Task]
    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed / 4);
        Task.current.Succeed();
    }


    [Task]
    void ReportLastSeen()
    {
        lastPlayerPos = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, 30) && hit.collider.CompareTag("Player"))
        {
            visao = true;
        }
        else
        {
            visao = false;
        }
    }



}