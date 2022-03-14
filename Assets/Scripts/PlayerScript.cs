using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public GameObject winTextObject;

    public GameObject loseTextObject;

    public Text score;

    public Text lives;

    private int scoreValue = 0;

    private int livesValue = 3;

    public int jumpForce;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    Animator anim;

    private bool facingRight = true;

    private bool isOnGround;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        lives.text = livesValue.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue == 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector3(59.0f, -0.15f);
            livesValue = 3;
            lives.text = livesValue.ToString();
        }
        if (livesValue == 0)
        {
            loseTextObject.SetActive(true);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        }
    }
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
