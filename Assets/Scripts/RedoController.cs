using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedoController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    public float redoJumpForce = 500f;
    private float redoHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (redoHurtTime == -1) {

            if (Input.GetButtonUp("Jump") && jumpsLeft > 0)
            {
                if (myRigidBody.velocity.y < 0){
                    myRigidBody.velocity = Vector2.zero;
                }

                if(jumpsLeft == 1){
                    myRigidBody.AddForce(transform.up * redoJumpForce * 0.75f);
                } else {
                    myRigidBody.AddForce(transform.up * redoJumpForce);
                }
                
                jumpsLeft--;

                jumpSfx.Play();
            } 

            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y);
            scoreText.text = (Time.time - startTime).ToString("0");

        } else {
            if (Time.time > redoHurtTime + 2)
            {
                Application.LoadLevel("GameOver");
                //Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }

            redoHurtTime = Time.time;
            myAnim.SetBool("redoHurt", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * redoJumpForce);
            myCollider.enabled = false;

            deathSfx.Play();

            float currentHighScore = PlayerPrefs.GetFloat("HighScore", 0);
            float currentScore = Time.time - startTime;

            if (currentScore > currentHighScore){
                PlayerPrefs.SetFloat("HighScore", currentScore);
            }
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
            jumpsLeft = 2;
        }
    }
}
