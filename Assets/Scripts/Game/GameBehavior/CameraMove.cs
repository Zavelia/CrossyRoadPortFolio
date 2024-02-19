using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float size;

    public float Size { get => size; set => size = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void CamMove()
    {
        this.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y, this.transform.position.z + moveSpeed);
        this.GetComponent<Camera>().orthographicSize=this.size;
    }
}
