using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public GameObject target;
    public bool targetInBlastRadius = false;
    private bool checkAgain = true;
    // Start is called before the first frame update
    private void doDamage()
    {
        if (GameManager.isLevelWin != true)
        {
            if (targetInBlastRadius == true)
            {
                GameManager.isPlayerDead = true;
                Destroy(target);
            }
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    // Update is called once per frame
    void Update()
    { 
        if(targetInBlastRadius == true && checkAgain == true)
        {
            checkAgain = false;
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.5f);
            Invoke(nameof(doDamage), 2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            targetInBlastRadius = true;
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            targetInBlastRadius = false;
        }
    }
}
