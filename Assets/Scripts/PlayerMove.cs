﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerMove
    :
    MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Assert.IsNotNull( body );

        float dt = GetDT();

        if( Input.GetAxis( "Move Left" ) > 0.0f )
        {
            body.AddForce( new Vector2( -speed *
                dt,0.0f ) );
        }
        if( Input.GetAxis( "Move Right" ) > 0.0f )
        {
            body.AddForce( new Vector2( speed *
                dt,0.0f ) );
        }

        body.AddForce( -body.velocity * 3.0f * dt );

        if( Input.GetAxis( "Jump" ) > 0.0f )
        {
            if( canJump )
            {
                jumping = true;
                canJump = false;
            }
        }
        else if( minJump.IsDone() ) FinishJump();

        if( jumping )
        {
            body.AddForce( new Vector2( 0.0f,jumpPower *
                dt * ( ( 1.0f - maxJump.GetPercent() ) * 1.5f ) ) );

            maxJump.Update( Time.deltaTime );
            minJump.Update( Time.deltaTime );

            if( maxJump.IsDone() ) FinishJump();
        }
    }
    void FinishJump()
    {
        maxJump.Reset();
        minJump.Reset();
        jumping = false;
    }
    void OnCollisionEnter2D( Collision2D coll )
    {
        canJump = true;
    }
    float GetDT()
    {
        return ( Time.deltaTime * dtOffset );
    }
    // 
    const float dtOffset = 1.0f / 0.01700295f;
    const float speed = 20.0f;
    Rigidbody2D body;
    const float jumpPower = 45.4f;
    bool jumping = false;
    bool canJump = false;
    Timer minJump = new Timer( 0.11f );
    Timer maxJump = new Timer( 0.75f );
}