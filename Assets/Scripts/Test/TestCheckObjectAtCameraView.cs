using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCheckObjectAtCameraView : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindAnyObjectByType<TestPlayer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(player.transform.position);
        if ((viewPos.x >= 0 && viewPos.x <= 1) &&
            (viewPos.y >= 0 && viewPos.y <= 1) &&
            (viewPos.z > 0))
        {
            //Debug.Log($"Camera in Object : {player.name}");
        }
        //else Debug.Log("¹Û");

    }
}
