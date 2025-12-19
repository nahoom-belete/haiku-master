using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    //Declarations for following player
    public Transform target;
    private float moveSpeed = 4f;
    public Object keyObj;
    public static bool slowingPlayer = false;
    Vector3 dropLoc;
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.sceneLoaded == true)
        {
            slowingPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPlayerDead != true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
        if (GameManager.isLevelWin == true || GameManager.isLevelLose == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int i = Random.Range(-3, 4);
        switch(i)
        {
            case -1: 
                dropLoc = transform.position + Vector3.right * i;
                break;
            case -2: 
                dropLoc = transform.position + Vector3.right * i;
                break;
            case -3:
                dropLoc = transform.position + Vector3.right * i;
                break;
            case 0:
                dropLoc = transform.position + Vector3.right * i;
                break;
            case 1:
                dropLoc = transform.position + Vector3.right * i;
                break;
            case 2:
                dropLoc = transform.position + Vector3.right * i;
                break;
            case 3:
                dropLoc = transform.position + Vector3.right * i;
                break;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.keys != 0)
            {
                GameManager.slowPlayer = true;
                GameObject clone = Instantiate(keyObj, dropLoc, transform.rotation) as GameObject;
                GameManager.keys--;
            }
            else if(slowingPlayer == false)
            {
                GameManager.slowPlayer = true;
            }
            GameManager.enemies--;
            Destroy(gameObject);
        }
    }
}
