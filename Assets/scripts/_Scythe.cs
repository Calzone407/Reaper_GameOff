using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Scythe : MonoBehaviour
{
    public Transform trans;
    public Rigidbody2D rb;
    public float speed, smoothTime;
    public float floatHeight;
    public float moveForce;
    public float damping;
    private Vector2 velocity = Vector2.zero;
    private Vector2 targetVelocity;
    private bool buffer;
    private float i;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

 void LateUpdate()
 {
    Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);  

    RaycastHit2D hit = Physics2D.Raycast(transform.position, worldPosition);
    
    if(Input.GetMouseButton(0))
    {
       buffer = true;
       Debug.Log(buffer);
    }
    else if(!buffer)
    {
        rb.velocity = new Vector2(0, 0);
    }
    //else{rb.velocity = new Vector2(0, 0);}
    while(buffer)
    {
        i += 1;
            if(i <= 0)
            {
                buffer = false;
            }
        if(transform.position == worldPosition)
        {
            i = 0;
        }
        var direction = worldPosition - transform.position;
        direction = direction.normalized;
        targetVelocity = new Vector2(direction.x * speed, direction.y * speed);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
        
    }
    
   
    
 }
}
