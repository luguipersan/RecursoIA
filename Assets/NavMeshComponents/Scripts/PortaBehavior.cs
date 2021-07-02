using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaBehavior : MonoBehaviour
{
    public GameObject portaParaAbrir;
    public GameObject posicaoParaAbrir;
    public GameObject posicaoParaFechar;
    public GameObject sniper;
    public Collider triggerBox;
    public bool isOpening = false;
    public bool playerIsNear = false;
    public bool isXMovement;
    public bool isClosing = false;
    public bool isClosed = true;

    public bool isLocked = false;

    public float timeAberto = 3.0f;
    public float timeParaFechar;

    public float timeFechado = 3.0f;
    public float timeParaAbrir;

    public float timeTranca = 10.0f;
    public float timeParaDestrancar;

    public float timeStart = 2.0f;

    // Update is called once per frame

    void Update()
    {
        //isLocked = sniper.GetComponent<SniperScript>().isLocked;
        if (timeStart > 0)
        {
            timeStart -= Time.deltaTime;
            playerIsNear = false;
        }
        if (isLocked)
        {
            timeParaDestrancar -= Time.deltaTime;
            isClosing = true;
            if (timeParaDestrancar <= 0)
            {
                isLocked = false;
            }
        }

        if (playerIsNear && !isLocked && Input.GetKeyDown("e") && isClosed)
        {
            isOpening = true;
            timeParaFechar = timeAberto;
            isClosed = false;
        }
        if (playerIsNear && !isLocked && Input.GetKeyDown("e") && !isClosed && !isOpening)
        {
            isClosing = true;
            timeParaAbrir = timeFechado;
        }

        if (isOpening)
        {
            if (portaParaAbrir.transform.position.x <= posicaoParaAbrir.transform.position.x && isXMovement)
            {
                portaParaAbrir.transform.position += new Vector3(0.1f, 0,0);
            }

            if (portaParaAbrir.transform.position.y <= posicaoParaAbrir.transform.position.y && !isXMovement)
            {
                portaParaAbrir.transform.position += new Vector3(0.1f, 0, 0);
            }

            timeParaFechar -= Time.deltaTime;
            if (timeParaFechar <= 0)
            {
                isOpening = false;
            }
        }
        if (isClosing)
        {
            if (portaParaAbrir.transform.position.x >= posicaoParaFechar.transform.position.x && isXMovement)
            {
                portaParaAbrir.transform.position -= new Vector3(0.1f, 0, 0);
            }

            if (portaParaAbrir.transform.position.y >= posicaoParaFechar.transform.position.y && !isXMovement)
            {
                portaParaAbrir.transform.position -= new Vector3(0.1f, 0, 0);
            }
            timeParaAbrir -= Time.deltaTime;
            if (timeParaAbrir <= 0)
            {
                isClosing = false;
                isClosed = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        playerIsNear = true;
    }
    private void OnTriggerExit(Collider other)
    {
        playerIsNear = false;
    }
}
