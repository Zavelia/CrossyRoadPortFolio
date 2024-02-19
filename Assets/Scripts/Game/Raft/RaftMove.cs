using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        this.startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * this.moveSpeed);
        this.SelfComeback();
    }

    //Å×½ºÆ®
    private void SelfComeback()
    {
        if (this.transform.position.x > 24f)
        {
            this.transform.position = this.startPos;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {

    //    }
    //}

}
