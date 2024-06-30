using System;
using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    
    GameManager gameManager;
    Vector3 moveDir;
    [SerializeField] float moveSpeed = 0.5f;//�� �̵� �ӵ�
    private Transform mobTrnspos;
    private float beforeX;
    CapsuleCollider2D mobCollider;

    [Header("���� ����")]
    int HP = 1;
    Animator anim;
    float deathTime = 0.5f;
    float deathTimer = 0.0f;


    private void Awake()
    {
        mobTrnspos = transform;
        gameManager = FindObjectOfType<GameManager>();
        //public ������ ������ ������ ����
        //�ڵ� �ϼ� ������� ����
        beforeX = mobTrnspos.position.x;//������ x�� Ȯ��
        mobCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    

    private void Update()
    {
        Mobmoving();
        seeCheack();
        death();

    }

    private void death()
    {
        if (HP == 0)//�����
        {
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;
            //Destroy(gameObject);
            if (deathTimer > deathTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            HP = HP - 1;//�ӽ�
        }
    }
    private void Atteak()//��⿹��
    {
        //�ӽñ��
        if (mobCollider.IsTouchingLayers(LayerMask.GetMask("Player")) == true) 
        {
            Destroy(gameObject);
        }
    }



    /// <summary>
    /// �÷��̾ �ִ� �������� �ڵ����� �̵��մϴ�
    /// ������ �߰��ϸ� ��� ����(����! public��)
    /// </summary>
    public void Mobmoving() 
    {
        Vector3 playerPos;
       
        if (gameManager.trsTarget == null)//player�� �׾��� ���
        {
            return;
        }
        else 
        {
            gameManager.PlayerTrsPosiTion(out playerPos);//��¿�
            Vector3 distance = playerPos - mobTrnspos.position;
            distance.z = 1.0f;
            mobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distance}");//�����ʿ�

        }

    }
    /// <summary>
    /// �̵��ϴ� ���⿡ ���� Scale�� ������ �¿�� �̵��ϴ°� ó�� ���̰� �Ѵ�
    /// </summary>
    private void seeCheack()
    {
        Vector3 scale = mobTrnspos.localScale;
        float affterX = mobTrnspos.position.x;
       
        if (affterX > beforeX)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (affterX < beforeX)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        beforeX = affterX;

    }
}