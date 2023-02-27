using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Only")]
    [SerializeField]
    private int attackDamage;

    private bool isAttacking;

    Animator animator;


    [Header("Barrel Only")]
    [SerializeField]
    private GameObject droppedObj;

    [SerializeField]
    private int health;

    Entity player;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = FindObjectOfType<Player>().gameObject.GetComponent<Entity>();

        if (this.tag == "Enemy")
        {
            animator = GetComponentInChildren<Animator>();
            isAttacking = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && player.ActionPoints >= 2)
        {
            player.Attack(this.transform);
            this.health -= player.AttackDamage;
        }

        if (health <= 0)
        {
            if (tag == "Barrel")
                DestroyBarrel();

            if (tag == "Enemy")
            {
                DestroyEnemy();
            }
        }
    }

    public void AttackPlayer()
    {
        if (this.tag == "Enemy" && Vector3.Distance(this.transform.position, player.transform.position) <= 3)
        {
            if (!isAttacking)
            {
                isAttacking = true;

                transform.LookAt(player.transform);

                animator.SetBool("Attack", true);

                player.Health -= attackDamage;

                Invoke("ResetAttack", .5f);
            }
        }
    }

    private void ResetAttack()
    {
        animator.SetBool("Attack", false);
        isAttacking = false;
    }

    private void DestroyEnemy()
    {
        gameManager.score += 100;
        Destroy(gameObject);
    }

    private void DestroyBarrel()
    {
        gameManager.score += 10;
        GameObject banana = Instantiate(droppedObj, this.transform);
        banana.transform.parent = null;
        Destroy(gameObject);
    }
}
