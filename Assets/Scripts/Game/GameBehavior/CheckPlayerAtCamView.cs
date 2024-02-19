using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerAtCamView : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Camera cam;
    private Vector3 startPos;

    public Vector3 StartPos { get => startPos; }

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindAnyObjectByType<Player>().gameObject;
        this.startPos = transform.position;
    }

    public bool CheckPlayerEagle() 
    {
        Vector3 viewPos = cam.WorldToViewportPoint(player.transform.position);
        this.startPos = new Vector3(this.startPos.x, this.startPos.y, this.player.transform.position.z - 2.48f);
        if ((viewPos.y >= -0.1f) && (viewPos.z > 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckPlayerRaft()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(player.transform.position);
        this.startPos = new Vector3(this.startPos.x, this.startPos.y, this.player.transform.position.z - 2.48f);
        if (viewPos.x > 1f || viewPos.x< 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
