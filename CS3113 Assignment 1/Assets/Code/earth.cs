using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    float bottomY;

    public float minHeight;
    public float maxHeight;

    SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        bottomY = transform.position.y - _sprite.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (_sprite.size.y > maxHeight)
        {
            _sprite.size = new Vector2(_sprite.size.x, maxHeight);
            transform.localPosition = new Vector3(transform.localPosition.x, bottomY + maxHeight / 2, transform.localPosition.z);
        }
        if (_sprite.size.y < minHeight)
        {
            _sprite.size = new Vector2(_sprite.size.x, minHeight);
            transform.localPosition = new Vector3(transform.localPosition.x, bottomY + minHeight / 2, transform.localPosition.z);
        }
    }
}