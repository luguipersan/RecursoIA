using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float maxDistance;

    public GameObject Shooter;

    private GameObject playerTrigger;

    private GameObject enemyTrigger;
    public float damage;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if (hit.gameObject.tag == "Player" && Shooter.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("acertou player");
            playerTrigger = hit.gameObject;
            playerTrigger.GetComponent<COMPLETE_PlayerController>().playerHealth -= damage;

        }

        if (hit.gameObject.tag == "Enemy" && Shooter.gameObject.tag != "Enemy")
        {
            enemyTrigger = hit.gameObject;
            enemyTrigger.GetComponent<EnemyDeath>().enemyHealth -= damage;
            Destroy(this.gameObject);
            Debug.Log("hit");
        }
    }
}

