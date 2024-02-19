using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject trainAlert;
    private Vector3 startPos;

    private bool isMove=false;
    // Start is called before the first frame update
    void Start()
    {
        this.startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.trainAlert.activeSelf) isMove = true;
        else isMove = false;
        if(this.isMove)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * this.moveSpeed);
        }
        else
        {
            this.transform.position = this.startPos;
        }
        this.SelfComeback();
    }

    //Å×½ºÆ®
    private void SelfComeback()
    {
        if (this.transform.position.x > 50f)
        {
            this.transform.position = this.startPos;
        }
    }
}
