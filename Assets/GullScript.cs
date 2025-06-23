using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GullScript : MonoBehaviour
{
    public int maxHealth = 80;
    int currentHealth;
    public LogicScript logic;
    private static int eggsEaten = 0;
    public SpriteRenderer eggSpriteRenderer;
    public Sprite twoEggs;
    public Sprite oneEgg;
    public Sprite noEgg;
    public GameObject eggs;
    private bool stop;

    public float moveSpeed = 2f;
    public float rotateSpeed = 200f;
    public Rigidbody2D myRb;
    public bool right;
    [SerializeField] Transform target;
    Vector2 lastRotation;
    private float time;
    [SerializeField] float interval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        eggSpriteRenderer = eggs.GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //int Rand = Random.Range(0, 99);
        //if (Rand <= 10)
       // {
          //  chase();
        //}
        float distance = Vector3.Distance(transform.position, eggs.transform.position);
        if (distance < 7)
        {
            chase();
        }
        else
        {
            time += Time.deltaTime;
            interval = Random.Range(1f, 3f);
            while (time >= interval)
            {
                transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
                time -= interval;
            }
            transform.Translate(new Vector2(0f, moveSpeed) * Time.deltaTime, Space.Self);
        }
    }

    public void chase()
    {
        Vector2 direction = target.position - transform.position;
        if (lastRotation != direction)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
        lastRotation = direction;
        transform.Translate(new Vector2(0f, moveSpeed) * Time.deltaTime, Space.Self);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(this.gameObject);
    }

    void respawn()
    {
        int highestPoint = 5;
        int lowestPoint = -5;
        Instantiate(this.gameObject, new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Eggs")
        {
            Debug.Log("eating egg");
            eggsEaten++;
            Debug.Log("eggsEaten " + eggsEaten);
            if (eggsEaten == 1)
            {
                eggSpriteRenderer.sprite = twoEggs;
            }
            if (eggsEaten == 2)
            {
                eggSpriteRenderer.sprite = oneEgg;
            }
            if (eggsEaten == 3)
            {
                eggSpriteRenderer.sprite = noEgg;
                logic.gameOver();
                eggsEaten = 0;
                stop = true;
            }
            if (!stop)
            {
                respawn();
            }
            Die();
        }
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        if (viewPos.x < -8.45)
        {
            transform.position = new Vector3(-8.45f, viewPos.y, viewPos.z);
            transform.rotation = Quaternion.FromToRotation(Vector3.left, new Vector3(0, 0, 0)); ;
        }
        if (viewPos.x > 8.45)
        {
            transform.position = new Vector3(8.45f, viewPos.y, viewPos.z);
            transform.rotation = Quaternion.FromToRotation(Vector3.left, Vector3.right);
        }
        if (viewPos.y > 4.5)
        {
            transform.position = new Vector3(viewPos.x, 4.4f, viewPos.z);
            int num = (int)Random.Range(0, 1);
            if (num == 0)
            {
                transform.rotation = Quaternion.FromToRotation(new Vector3(0, 0, 0), Vector3.left);
            }
            else if (num == 1)
            {
                transform.rotation = Quaternion.FromToRotation(new Vector3(0, 0, 0), Vector3.right);
            }
        }
        if (viewPos.y < -5.07)
        {
            transform.position = new Vector3(viewPos.x, -5.07f, viewPos.z);
        }
    }
}
