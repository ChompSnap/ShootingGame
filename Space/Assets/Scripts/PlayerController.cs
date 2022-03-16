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
    

    private bool attack = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {  attack = true;
            animator.SetBool("isAttack", true);
            shoot(); 
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
            getHit();
        }
    }

    public void getHit()
    {
        animator.SetTrigger("isDed");
        
    }

}
