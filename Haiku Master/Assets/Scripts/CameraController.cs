using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.isPlayerDead != true)
        {
            Vector2 moveDistance = target.position - transform.position;
            moveDistance = moveDistance.normalized * Time.deltaTime * moveSpeed;
            float maxDistance = Vector2.Distance(transform.position, target.position);
            transform.position += Vector3.ClampMagnitude(new Vector3(moveDistance.x, moveDistance.y, 0), maxDistance);
        }       
    }
}
