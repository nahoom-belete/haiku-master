using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform target;
    public Object[] objects;
    public int spawnFrequency = 5;
    private int randomObj;
    private bool spawning = false;
    private int maxSpawn = 3;

    private void spawn()
    {
        randomObj = Random.Range(0, objects.Length);
        GameObject clone = Instantiate(objects[randomObj], transform.position, transform.rotation) as GameObject;

        if(target != null && clone.gameObject.GetComponent<EnemyChaser>() != null)
        {
            clone.gameObject.GetComponent<EnemyChaser>().SetTarget(target);
        }
        if (target != null && clone.gameObject.GetComponent<EnemyBomber>() != null)
        {
            clone.gameObject.GetComponent<EnemyBomber>().SetTarget(target);
            clone.gameObject.transform.GetChild(0).gameObject.GetComponent<DamageZone>().SetTarget(target.gameObject);
        }
        maxSpawn -= 1;
        GameManager.enemies++;
        spawning = false;
    }
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.enemies < 3)
        {
            maxSpawn = 3;
        }
        if (spawning == false && maxSpawn != 0) 
        {
            spawning = true;
            Invoke(nameof(spawn), spawnFrequency);
        }
        if (GameManager.isLevelWin == true || GameManager.isLevelLose == true)
        {
            Destroy(gameObject);
        }
    }
}
