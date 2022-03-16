using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] animationE;
    public float animationTime;
    private SpriteRenderer renderSprite;
    private int frames;
    public System.Action killed;
    public Animator animator;
    private bool isDead = false;
    private bool shoot = false;
    private float timer; 


    private void Awake()
    {
        renderSprite = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            timer += Time.deltaTime;
            float seconds = timer % 60;
            if (seconds > 3)
            {
                killed.Invoke();
                gameObject.SetActive(false);
            }
        }
        else if (shoot)
        {
            timer += Time.deltaTime;
            float seconds = timer % 60;
            if (seconds > 3)
            {
                shoot = false;
                animator.SetBool("isAttack", shoot);
            }
        }
    }

    private void animateSprite()
    {
        frames++;
        
        if(frames >= animationE.Length)
        {
            frames = 0;
        }

        renderSprite.sprite = animationE[frames];

    }

    public void invaderAttack()
    {
        timer = 0;
        shoot = true;
        animator.SetBool("isAttack", shoot);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if(collision.gameObject.layer == LayerMask.NameToLayer("Lazer"))
        {
            timer = 0;
            animator.SetTrigger("isDead");
            GetComponent<BoxCollider2D>().enabled = false;
            isDead = true;
            //killed.Invoke();
            //gameObject.SetActive(false);
        }
    }
}
