using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    Rigidbody2D rb;
    bool onGround;

    [SyncVar]
    public string playerName;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //CmdSetName(MenuUI.chosenName);
    }

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
        rb.velocity = new Vector2(movement * 5f, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }

        float x = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(var contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
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

    //[Command]
    //void CmdSetName(string name)
    //{
    //    playerName = name;
    //}
}
