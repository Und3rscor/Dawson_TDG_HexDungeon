using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float speed;

    private Vector3 targetPos;
    public Vector3 TargetPos
    {
        set
        {
            if (targetPos != value)
            {
                targetPos = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }
}
