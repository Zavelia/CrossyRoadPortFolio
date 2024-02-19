using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    private System.Action<Vector3> onCanceled;

    private Vector3 moveDir;
    private Animator anim;
    private PlayerInput playerInput;
    private InputActionMap mainActionMap;
    private InputAction moveAction;
    [SerializeField] private float turnSpeed = 3.0f;

    [SerializeField] private Transform raySpot;
    [SerializeField] private bool isFrontObstacle=false;
    // Start is called before the first frame update
    void Start()
    {
        this.anim = GetComponent<Animator>();

        this.PlayerMoveInit();
        this.PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        this.PlayerRotation();
        this.RayHitObstacle();
        
    }

    private void RayHitObstacle()
    {
        Ray ray = new Ray(this.raySpot.position, this.raySpot.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);
        int layerMask = 1 << LayerMask.NameToLayer("Obstacle");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f, layerMask))
        {
            this.isFrontObstacle = true;
        }
        else
        {
            this.isFrontObstacle = false;
        }
    }

    private void PlayerMoveInit()
    {
        this.playerInput = this.GetComponent<PlayerInput>();
        this.mainActionMap = this.playerInput.actions.FindActionMap("Player");
        this.moveAction = this.mainActionMap.FindAction("Move");
    }

    private void PlayerMove()
    {
        //대리자 이벤트가 중복으로 붙으면서 생기는 문제
        this.moveAction.performed += this.MovePerformedEvent;

        this.moveAction.canceled += this.MoveCanceledEvent;

        this.onCanceled = (moveDir) =>
        {
            if (this.isFrontObstacle == false)
            {
                this.anim.SetTrigger("Jump");
                this.transform.position = this.transform.position + (moveDir.normalized);
            }
        };
    }

    private void MovePerformedEvent(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        this.moveDir = new Vector3(dir.x, 0, dir.y);

        //애니메이션 실행
        this.anim.SetTrigger("Input");
    }

    private void MoveCanceledEvent(InputAction.CallbackContext context)
    {
        this.anim.SetTrigger("Idle");
        this.onCanceled(moveDir);
    }
    private void PlayerRotation()
    {
        if (this.moveDir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(this.moveDir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot,
                Time.deltaTime * this.turnSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raft"))
        {
            this.transform.SetParent(other.transform);
            this.transform.position =
                new Vector3(this.transform.position.x, 
                this.transform.position.y,
                this.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Raft"))
        {
            this.transform.SetParent(null);
            this.transform.position = new Vector3((int)this.transform.position.x,
                this.transform.position.y, this.transform.position.z);
        }
    }

    private void OnDestroy()
    {
        this.moveAction.performed -= this.MovePerformedEvent;

        this.moveAction.canceled -= this.MoveCanceledEvent;
    }
}
