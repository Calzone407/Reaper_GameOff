using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Scythe : MonoBehaviour
{
    public GameObject trans;
    public Rigidbody2D rb;
    public float speed, smoothTime;
    public float floatHeight;
    public float moveForce;
    public float damping;
    private Vector2 velocity = Vector2.zero;
    private Vector2 targetVelocity;
    private bool buffer;
    private bool buffer2;
    private float i = 1;
    private Vector2 _hitPoint;
    private Vector3 direction;
    Vector3 worldPosition;
    public GameObject hook;
    public GameObject scythePoint;
    public Animator anim;
    public BoxCollider2D box;
    public CircleCollider2D circle;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

 void LateUpdate()
 {
    scytheLaunch();

 }

 void OnTriggerEnter2D(Collider2D col)
 {
    if(col.gameObject.tag == "scytheHook")
    {
        Debug.Log("Hello World!");
    }
    if(col.gameObject.tag == "scythePoint")
    {
        //trans.parent = player;
        Debug.Log("Wassup my tigga!");
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
    worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);  

    RaycastHit2D hit = Physics2D.Raycast(transform.position, worldPosition);
    
    if(Input.GetMouseButton(0))
    {
       buffer = true;
       if(Input.GetMouseButtonDown(0))
       {
        Instantiate(hook);
        hook.transform.position = worldPosition;
        box.isTrigger = false;
        circle.isTrigger = false;
        anim.applyRootMotion = true;
       }
       
    }
    else if(!buffer)
    {
       // rb.velocity = new Vector2(0, 0);
        anim.SetBool("isAction", false);
        box.isTrigger = true;
        circle.isTrigger = true;
        anim.applyRootMotion = false;
    }

    if(Input.GetMouseButton(1))
    {
        buffer = true;
        hook.transform.position = scythePoint.transform.position;
        box.isTrigger = true;
        circle.isTrigger = true;
    }
    //else{rb.velocity = new Vector2(0, 0);}
    if(buffer)
    {
        transform.parent = null;
       
        if(transform.position == hook.transform.position)
        {
            i = 0;
            Debug.Log("i is = to 0");
        }

         if(i <= 0)
        {
            buffer = false;
            Debug.Log("Working!");
            
        }
        
        anim.SetBool("isAction", true);
        direction = hook.transform.position - transform.position;
        direction = direction.normalized;
        targetVelocity = new Vector2(direction.x * speed, direction.y * speed);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
    }

 }

 
}
