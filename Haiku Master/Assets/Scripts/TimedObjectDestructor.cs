using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestructor : MonoBehaviour
{
    public int time = 1;
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroySelf), time);
    }
}
