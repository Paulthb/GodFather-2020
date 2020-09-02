using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public AudioClip spawn;
    public AudioClip safeLand;
    public AudioClip hardLand;

    private AudioSource _audio;
    private Animator _anim;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _audio.clip = spawn;
        _audio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CatGround"))
        {
            _audio.clip = hardLand;
            _audio.Play();
            _anim.SetTrigger("Land");
            _rb.gravityScale = 0.0f;
            _rb.velocity = new Vector2(0, 0);
            transform.position = new Vector3(transform.position.x,-3.03f, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Collider2D>().enabled = false;
        }
        if(collision.CompareTag("CatSaver"))
        {
            _audio.clip = safeLand;
            _audio.Play();
            _rb.gravityScale = 0.0f;
            _rb.velocity = new Vector2(0, 0);
            transform.position = new Vector3(transform.position.x, -3.03f, transform.position.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
