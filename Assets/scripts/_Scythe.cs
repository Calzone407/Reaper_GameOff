using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Scythe : MonoBehaviour
{
    private bool buffer;
    private bool isClick;
    private bool isTouching;
    private bool hasClicked; 
    private Vector2 _hitPoint;
    private Vector2 targetVelocity;
    private Vector2 velocity = Vector2.zero;
    private Vector3 direction;
    private Vector3 objectPoint;
    private Vector3 worldPosition;
    private Vector3 scythePosition;
    public Rigidbody2D rb;
    public GameObject hook;
    public Transform player;
    public GameObject scytheObj;
    public GameObject scythePoint;
    public Animator anim;
    public BoxCollider2D box;
    public CircleCollider2D circle;
    public float speed, smoothTime, floatHeight, moveForce, damping;
   
    void LateUpdate()
    {
      scytheLaunch();
      StartCoroutine(scythePos());
    }

    void FixedUpdate()
    {
        scythePosition = scythePoint.transform.position;
    }
    
    void EnableColliders()
     {
        box.isTrigger = true;
        circle.isTrigger = true;
     }
    void DisableColliders()
      {
        box.isTrigger = false;
        circle.isTrigger = false;
     }

    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.gameObject.tag == "scythePoint")
     {
        scytheObj.transform.parent = player.transform;
        buffer = false;
        isClick = false;
     }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            DisableColliders();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.tag == "Wall")
        {
          rb.velocity = new Vector2(0, 0);
        }
    }

    void scytheLaunch()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, worldPosition);
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);  
    
    if(Input.GetMouseButton(0))
    {
            if(Input.GetMouseButtonDown(0))
            {
                hasClicked = true;
                buffer = true;
                Instantiate(hook);
                hook.transform.position = worldPosition;
                objectPoint = hook.transform.position;
                anim.applyRootMotion = true;
            }  
    }
    else if(Input.GetMouseButton(1) && hasClicked)
        {
            if(Input.GetMouseButtonDown(1))
            {
            isClick = true; 
            hasClicked = false;
            }           
        }

    if(buffer)
    {
        transform.parent = null;
        anim.SetBool("isAction", true);
        direction = objectPoint - transform.position;
        direction = direction.normalized;
        targetVelocity = new Vector2(direction.x * speed, direction.y * speed);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
    }

    else if(!buffer)
    {
        anim.applyRootMotion = false;
        anim.SetBool("isAction", false);
        scytheObj.transform.localScale = new Vector3(-0.6f, scytheObj.transform.localScale.y, scytheObj.transform.localScale.z);
    }
    }

    public IEnumerator scythePos()
    {
        if(isClick)
        {  
            objectPoint = scythePosition;
            buffer = true;
            EnableColliders();
        }    
        yield return null;
    }
}
