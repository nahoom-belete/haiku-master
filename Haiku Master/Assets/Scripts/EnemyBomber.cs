using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 4f;
    public bool isNearPlayer = false;
    public Object explosion;
    public GameObject damageRing;
    public AudioClip explosionAudio;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void destroySelf()
    {
        GameManager.enemies--;
        Destroy(gameObject);
    }

    private void explodeSelf()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        damageRing.GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(explosion, transform);
        Invoke(nameof(destroySelf), 1);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPlayerDead != true || GameManager.isLevelLose != true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            if (GetComponentInChildren<DamageZone>().targetInBlastRadius == true && isNearPlayer == false)
            {
                isNearPlayer = true;
                GetComponent<AudioSource>().PlayOneShot(explosionAudio);
                Invoke(nameof(explodeSelf), 2);
            }
        }
        if (GameManager.isLevelWin == true)
        {
            destroySelf();
        }
    }
}
