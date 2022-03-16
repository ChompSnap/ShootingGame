using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5.0f;
    public Projectile lazer;
    private bool lazerActive = false;
    public Animator animator;

    private float timer = 0.0f;

    private bool attack = false;
    private bool isDead = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                attack = true;
                animator.SetBool("isAttack", true);
                shoot();
            }
        }
        else
        {
            timer += Time.deltaTime;
            float seconds = timer % 60;
            if (seconds > 8)
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }
    private void shoot()
    {
        if(!lazerActive)
        {
            Projectile bullet = Instantiate(lazer, transform.position, Quaternion.identity);
            bullet.destroy += destroyedLazer;
            lazerActive = true;
        }
    }
    private void destroyedLazer()
    {
        attack = false;
        animator.SetBool("isAttack", false);
        lazerActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Invader") || other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //SceneManager.LoadScene("Credits");
            isDead = true;
            getHit();
        }
    }

    public void getHit()
    {
        animator.SetTrigger("isDed");
        
    }

}
