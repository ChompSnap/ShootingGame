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


    private void Awake()
    {
        renderSprite = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if(collision.gameObject.layer == LayerMask.NameToLayer("Lazer"))
        {
            killed.Invoke();
            gameObject.SetActive(false);
        }
    }
}
