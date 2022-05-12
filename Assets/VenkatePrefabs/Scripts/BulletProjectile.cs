using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform VxHitGreen;
    [SerializeField] private Transform VxHitRed;
    private Rigidbody b_rbody;

    private void Awake()
    {
       b_rbody= GetComponent<Rigidbody>();

    }
    private void Start()
    {
        float speed = 20f;
        b_rbody.velocity = transform.forward*speed;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BulleTarget>()!=null)
        {
            Instantiate(VxHitGreen,transform.position,Quaternion.identity);
            //Hit Target
        }
        else
        {
            Instantiate(VxHitRed,transform.position,Quaternion.identity);
            // hit Something Else
        }
        Destroy(gameObject);
    }
}
