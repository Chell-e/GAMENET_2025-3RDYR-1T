using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    Rigidbody2D rb;
    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        Collider2D thisCol = rb.GetComponent<Collider2D>();

        foreach (var other in FindObjectsOfType<PlayerController>())
        {
            if (other != null)
            {
                Collider2D otherCol = other.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(thisCol, otherCol);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        float movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movement * 5, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            CmdSendMessage("Hi!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(var contact in collision.contacts)
        {
            if (contact.normal.y > 0.5)
            {
                onGround = true;
                break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        onGround= false;
    }

    [Command]
    void CmdSendMessage(string message)
    {
        RpcSendMessage(message);
    }

    [ClientRpc] //Remote Procedure Call
    void RpcSendMessage(string message)
    {
        Debug.Log(message);
    }
}
