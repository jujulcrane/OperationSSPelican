using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pelican : MonoBehaviour
{
    public GameObject portal;
    public SpriteRenderer spriteRenderer;
    public Sprite hurtSprite;
    public Sprite healthySprite;
    public Sprite deadSprite;
    public LogicScript logicScript;
    public int pHP = 100;
    public float flapStrength = 20f;
    public float moveSpeed = 5f;
    public Rigidbody2D myRb;
    private Vector3 targetAngles;
    public float smooth = 1f;
    private bool firstLeftKey;
    private bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        firstLeftKey = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
        if (pHP < 51)
        {
            spriteRenderer.sprite = hurtSprite;
        }
        if (pHP <= 0)
        {
            logicScript.gameOver();
            canMove = false;
            spriteRenderer.sprite = deadSprite;
        }
    }

    private void LateUpdate()
    {
        logicScript.keepInBounds(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Explode")
        {
            Debug.Log("health -50");
            pHP -= 50;
            logicScript.updateHP();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Lava")
        {
            Debug.Log("health -5");
            pHP -= 5;
            logicScript.updateHP();
        }
        else if (collision.tag == "Fish")
        {
            pHP += 5;
            logicScript.updateHP();
            Debug.Log("Cash");
            Destroy(collision.gameObject);
            if (pHP > 50)
            {
                spriteRenderer.sprite = healthySprite;
            }
        }
        else if (collision.tag == "Ghost")
        {
            StateNameController.incrementScore();
            Ghost.increaseMoveSpeed();
            logicScript.updateScore();
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Portal")
        {
            SceneManager.LoadSceneAsync("StallStories");
        }
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            firstLeftKey = true;
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            transform.rotation = Quaternion.FromToRotation(Vector3.left, new Vector3(0, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            if (firstLeftKey)
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.left, Vector3.right);
                firstLeftKey = false;
            }
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    firstLeftKey = true;
        //    transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        //}
        if (Input.GetKey(KeyCode.DownArrow))
        {
            firstLeftKey = true;
            transform.position -= new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            myRb.velocity = Vector2.up * flapStrength;
        }
    }
}
