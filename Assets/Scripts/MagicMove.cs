﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MagicMove
    :
    MonoBehaviour
{
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        lifetime.Update( Time.deltaTime );

        if( lifetime.IsDone() )
        {
            Destroy( gameObject );
        }
    }
    void OnTriggerEnter2D( Collider2D coll )
    {
        if( coll.tag == "Player" )
        {
            coll.gameObject.GetComponent<Player>()
                .Attack( transform.position );
        }
    }
    public void SetVel( Vector2 vel )
    {
        Assert.IsNotNull( body );
        body.AddForce( vel * speed,ForceMode2D.Impulse );
    }
    // 
    Rigidbody2D body;
    Timer lifetime = new Timer( 2.4f );
    const float speed = 2.9f;
}
