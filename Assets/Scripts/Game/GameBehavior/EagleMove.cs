using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Player player;
    public System.Action onEagleTargetOn;
    private Vector3 startPos;

    private bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        this.startPos = new Vector3(this.player.transform.position.x, 2.0f, this.player.transform.position.z + 15f);
        this.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * this.moveSpeed);
        if(this.isOn==false) this.SelfComeback();
    }

    private void SelfComeback()
    {
        if (this.transform.position.z <= this.player.transform.position.z-1)
        {
            isOn = true;
            this.onEagleTargetOn();
        }
    }
}
