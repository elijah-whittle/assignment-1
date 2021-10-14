using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch : MonoBehaviour{
    public int dir = 1;
    public float health = 50f;
    public GameObject Player;
    public Transform fire_pos;
    public GameObject Bullet1;
    Animator _animator;
    Rigidbody2D _rigidbody;
    public float Speed = 0.5f;
    private int direction = -1;
    enum State{
        stay,
        move,
        attack,
        stage2
    }
    State currentState;
    void lookplayer(){
        if (gameObject.transform.position.x < Player.transform.position.x){
            transform.eulerAngles = new Vector3(0,180,0);
            direction = 1;
        }
        else{
            transform.eulerAngles = new Vector3(0,0,0);
            direction = -1;
        }
    }
    private void Start(){
        //Vector2 currPos = transform.position;
        //float moveRight = transform.position.x;
        //float moveLeft = -transform.position.x;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        StartCoroutine(NewState());
    }
    IEnumerator NewState(){
        while(health>=25){
            _animator.SetFloat("health", health);
            yield return new WaitForSeconds(3);
            currentState = (State)Random.Range(0,3);
        }
    }
    void Attack1(){
        _animator.SetTrigger("Attack1");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spell"))
        {
            health -= 1;
        }
    }

    // Update is called once per frame
    private void FixedUpdate(){
        lookplayer();
        switch(currentState){
            case State.move:
                _animator.SetBool("is_attack1", false);
                _rigidbody.velocity = new Vector2(Speed*direction, 0);
                _animator.SetFloat("speed", Mathf.Abs(_rigidbody.velocity.x));
                break;
            case State.stay:
                _animator.SetBool("is_attack1", false);
                _rigidbody.velocity = new Vector2(0.0f,0.0f);
                _animator.SetFloat("speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
                break;
            case State.attack:
                Attack1();
                _rigidbody.velocity = new Vector2(0.0f,0.0f);
                StartCoroutine(attackmotion());
                break;
            default:
                break;
        }
    }
    IEnumerator attackmotion(){
        while (true){
            if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime>1.0f){
                    break;
            }
            else{
                _rigidbody.velocity = new Vector2(0.0f,0.0f);
            }
            yield return new WaitForSeconds(0);
        }
    }
}
