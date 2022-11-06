using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scytheHook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(destruction());
    }

    IEnumerator destruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
