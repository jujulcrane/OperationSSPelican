using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public Pelican pelican;
    public GameObject eggs;
    public GameObject AttackInstructions;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {

        spawnEgg();
    }

    // Update is called once per frame
    void spawnEgg()
    {
        int Rand = Random.Range(1, 4);
        if (Rand == 1)
        {
            eggs.transform.position = new Vector3(-7.69f, 2.3f, 0f);
        }
        if (Rand == 2)
        {
            eggs.transform.position = new Vector3(-0.23f, -3.65f, 0f);
        }
        if (Rand == 3)
        {
            eggs.transform.position = new Vector3(-2.95f, -0.68f, 0f);
        }
        if (Rand == 2)
        {
            eggs.transform.position = new Vector3(4.82f, 4.26f, 0f);
        }
    }

    void Update()
    {
        animator.SetInteger("HP", pelican.pHP);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Attack();
            if (!started)
            {
                AttackInstructions.SetActive(false);
                started = true;
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<GullScript>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
