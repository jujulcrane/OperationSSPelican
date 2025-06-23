using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Pelican pelican;
    private static float moveSpeed = 1f;
    public float rotateSpeed = 200f;
    [SerializeField] Transform targetPelican;
    Vector2 lastRotation;
    public LogicScript logicScript;
    public Collider2D myCol;
    public Rigidbody2D myRb;
    public bool rotate;
    [SerializeField] float interval = 3f;
    private float time;
    private float stuckTime;
    [SerializeField] float stuckInterval = 3f;
    private GameObject[] ghosts;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-5, 4), 0);
        if (StateNameController.level == 1)
        {
            moveSpeed = 1f;
        }
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        time = 0f;
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        RunAway(targetPelican);
        Physics.IgnoreLayerCollision(6, 3, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (ghosts[0].transform.position != transform.position)
        {
            float ghostDistance = Vector3.Distance(transform.position, ghosts[0].transform.position);
            if (ghostDistance < 2)
            {
                teleport();
            }
        }
        float distance = Vector3.Distance(transform.position, pelican.transform.position);
        if (distance < 7)
        {
            RunAway(targetPelican);
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
            int num = (int) Random.Range(0, 1);
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

    public void RunAway([SerializeField] Transform target)
    {
        Vector2 direction = target.position - transform.position;
        if (lastRotation != direction)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, -direction);
        }
        lastRotation = direction;
        transform.Translate(new Vector2(0f, moveSpeed) * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cloud")
        {
           myCol.enabled = false;
        }
        if (collision.tag == "GameController" || collision.tag == "Lava" || collision.tag == "Portal")
        {
            myCol.enabled = true;
        }
        if (collision.tag == "Finish")
        {
            myCol.enabled = true;
        }
    }

    public static void increaseMoveSpeed()
    {
        moveSpeed+= 0.5f;
    }

    public void teleport()
    {
        Vector3 viewPos = transform.position;
        float xpos = Random.Range(-8.5f, 8.5f);
        float ypos = Random.Range(-3f, 4.4f);
        transform.position = new Vector3(xpos, ypos, viewPos.z);
    }

    public void stuck()
    {
        stuckTime += Time.deltaTime;
        stuckInterval = 2;
        Vector3 oldPos = transform.position;
        while (stuckTime >= stuckInterval)
        { 
            float slefDis = Vector3.Distance(transform.position, oldPos);
            if (slefDis < 2)
            {
                teleport();
            }
            time -= interval;
        }
    }
}
