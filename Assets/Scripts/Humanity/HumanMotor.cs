using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class HumanMotor : MonoBehaviour
{
    protected Rigidbody _rb;
    public Transform _thisTransform;
    protected float _moveSpeed;


    public EnemyHuman enemyHuman;
    public EnemyParasite enemyParasite;
    public PlayerHuman playerHuman;


    // delete then
    public Material red;
    public Material blue;
    public GameObject sphere;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        _thisTransform = transform;
        _moveSpeed = 8f;


        enemyHuman = GetComponent<EnemyHuman>();
        enemyParasite = GetComponent<EnemyParasite>();
        playerHuman = GetComponent<PlayerHuman>();
        enemyParasite.enabled = false;
        playerHuman.enabled = false;


        //delete then
        sphere.SetActive(false);
    }

    private void Update()
    {
        Movement();
        Jump();
        Attack();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (enemyHuman.isThereWasParasite)
            {
                enemyParasite.enabled = true;
                sphere.GetComponent<MeshRenderer>().material = blue;
            }
            else
                enemyHuman.enabled = true;
            playerHuman.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (enemyHuman.isThereWasParasite)
            {
                enemyParasite.ResetComponents();
                enemyParasite.enabled = false;
            }
            else
            {
                enemyHuman.ResetComponents();
                enemyHuman.isThereWasParasite = true;
                enemyHuman.enabled = false;

                RuntimeData.enemyParasites.Add(enemyParasite);
                RuntimeData.enemyHumen.Remove(enemyHuman);
            }
            playerHuman.enabled = true;
            sphere.SetActive(true);
            sphere.GetComponent<MeshRenderer>().material = red;
        }
    }

    virtual protected void Movement()
    {
        float moveHor = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        _rb.velocity = new Vector3(moveHor, _rb.velocity.y, _rb.velocity.z);
    }
    virtual protected void Jump() { }
    virtual protected void Attack() { }
}
