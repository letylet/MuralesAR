using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour
{

    public Sprite[] sprites = new Sprite[3];
   
    private Rigidbody rb;

    private float speed = 7.5f;

    
    private const float maxHeight = 0.5f;

    public MusicNotesSpawner controller { get; private set; }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();


        setSprite(sprites[Random.Range(0, sprites.Length)]);
   

        rb.velocity = new Vector3(Random.Range(-0.6f, 0.6f), 1, 0) * this.speed * Time.fixedDeltaTime;

    }

    private void FixedUpdate()
    {
        
        if (this.transform.position.y > this.controller.transform.position.y + maxHeight)
        {
            this.controller.ReturnNoteToPool(this);
        }
    }

    public void Setup(MusicNotesSpawner controller)
    {
        this.controller = controller;
    }


    void setSprite(Sprite image)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = image ;
        
    }
}
