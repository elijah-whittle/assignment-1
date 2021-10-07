using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth : MonoBehaviour
{
    //public int speed;
    public int ySpeed;
    Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    public Vector3 minScale;
    public Vector3 minPos;
    public Vector3 maxScale;
    public Vector3 maxPos;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y < minScale.y)
        {
            transform.localScale = minScale;
            transform.localPosition = minPos;
        }
        if (transform.localScale.y > maxScale.y)
        {
            transform.localScale = maxScale;
            transform.localPosition = maxPos;
        }

    }
}
