using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth : MonoBehaviour
{
    //public int speed;
    public int ySpeed;
    Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    //public Vector3 minScale;
    public float minY;
    //public Vector3 maxScale;
    public float maxY;

    public float minHeight;
    public float maxHeight;

    SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_sprite.size.y > maxHeight)
        {
            _sprite.size = new Vector2(_sprite.size.x, maxHeight);
            transform.localPosition = new Vector3(transform.localPosition.x, maxY, transform.localPosition.z);
        }
        if (_sprite.size.y < minHeight)
        {
            _sprite.size = new Vector2(_sprite.size.x, minHeight);
            transform.localPosition = new Vector3(transform.localPosition.x, minY, transform.localPosition.z);
        }
    }
}
