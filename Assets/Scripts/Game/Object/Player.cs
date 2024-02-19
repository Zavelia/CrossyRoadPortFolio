using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Delegate
    private System.Action<Vector3> onCanceled;
    public System.Action onCoin;
    public System.Action onPoint;
    public System.Action onDie;
    #endregion
    private Vector3 moveDir;
    private Animator anim;
    #region PlayerMove
    private PlayerInput playerInput;
    private InputActionMap mainActionMap;
    private InputAction moveAction;
    #endregion
    #region State
    private bool isDie = false;
    private bool isMoveAble = false;

    public bool IsMoveAble { get => isMoveAble; set => isMoveAble = value; }
    public bool IsOut { get => isOut; set => isOut = value; }
    public Vector3 MoveDir { get => moveDir; }
    #endregion
    [SerializeField] private float turnSpeed = 3.0f;

    [SerializeField] private Transform raySpot;
    [SerializeField] private bool isFrontObstacle = false;
    [SerializeField] private bool isOnRaft = false;
    private bool isOut = false;

    [SerializeField] private AudioSource playerMoveSound;
    [SerializeField] private AudioSource playerDieSound;
    [SerializeField] private AudioSource playerWaterDieSound;
    [SerializeField] private AudioSource playerGetCoinSound;

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

        if (this.isOut) this.isDie = true;
    }
    // 도로에서는 Clamp 켜기 
    private void PlayerMoveInit()
    {
        this.playerInput = this.GetComponent<PlayerInput>();
        this.mainActionMap = this.playerInput.actions.FindActionMap("Player");
        this.moveAction = this.mainActionMap.FindAction("Move");
    }
    private void PlayerMove()
    {
        this.moveAction.performed += this.MovePerformedEvent;

        this.moveAction.canceled += this.MoveCanceledEvent;

        this.onCanceled = (moveDir) =>
        {
            if (this.isFrontObstacle == false)
            {
                this.anim.SetTrigger("Jump");
                // StartCoroutine(CoMove(this.transform.position,moveDir));
                this.transform.position = this.transform.position + (moveDir.normalized);
                this.playerMoveSound.Play();
            }
        };
    }

    private void MovePerformedEvent(InputAction.CallbackContext context)
    {
        if (this.isMoveAble && this.isDie == false)
        {
            Vector2 dir = context.ReadValue<Vector2>();
            this.moveDir = new Vector3(dir.x, 0, dir.y);
            //애니메이션 실행
            this.anim.SetTrigger("Input");
        }
    }

    private void MoveCanceledEvent(InputAction.CallbackContext context)
    {
        if (this.isMoveAble && this.isDie == false)
        {
            this.anim.SetTrigger("Idle");
            this.onCanceled(moveDir);
        }
    }
    private void PlayerRotation()
    {
        if (this.moveDir != Vector3.zero && this.isDie == false)
        {
            Quaternion rot = Quaternion.LookRotation(this.moveDir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot,
                Time.deltaTime * this.turnSpeed);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if (this.moveDir == Vector3.forward && other.gameObject.GetComponent<GroundPlayerCheck>().IsCheck==false)
            {
                this.onPoint();
            }
        }

        else if (other.CompareTag("Coin") && this.isMoveAble==true)
        {
            this.onCoin();
            this.playerGetCoinSound.Play();
        }
        else if (other.CompareTag("Vehicle"))
        {
            this.onDie();
            this.playerDieSound.Play();
            this.isDie = true;
            this.anim.SetTrigger("DieByCar");
        }

        else if (other.CompareTag("Raft"))
        {
            this.isOnRaft=true;
            Debug.Log("Raft");
            this.transform.SetParent(other.transform);

            if (this.moveDir == Vector3.forward && other.gameObject.GetComponent<RaftAnimControl>().IsCheck==false)
            {
                this.onPoint();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water") && this.isOnRaft == false)
        {
            //Debug.Log("Water2");
            this.onDie();
            this.playerWaterDieSound.Play();
            this.isDie = true;
            this.anim.SetTrigger("DieByWater");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Raft"))
        {
            this.isOnRaft = false;
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
