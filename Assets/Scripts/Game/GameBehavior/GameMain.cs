using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    #region Object
    [SerializeField] private Player player;
    [SerializeField] private CoinGenerator coinGenerator;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private CameraMove camMove;
    [SerializeField] private CheckPlayerAtCamView camCheck;
    [SerializeField] private FadeOut fadeOut;
    [SerializeField] private FadeIn fadeIn;
    [SerializeField] private GameObject reStartButton;
    [SerializeField] private GameObject eagle;
    #endregion
    #region UIText
    [SerializeField] private Image title;
    [SerializeField] private Text txtPoint;
    [SerializeField] private Text txtCoin;
    [SerializeField] private Text txtTopPoint;
    #endregion
    private int coin = 0;
    private int point = 0;
    private int topPoint = 0;

    private System.Action onCam;
    private System.Action onOut;

    private bool isMove = false;
    private bool isOut = false;
    private bool isOver = false;
    private bool isCanStart = false;
    private Animator uiAnim;

    [SerializeField] private AudioSource playerRaftDieSound;

    public int TopPoint { get => topPoint; }


    // Start is called before the first frame update
    void Start()
    {
        this.uiAnim = this.title.gameObject.GetComponent<Animator>();
        this.coinGenerator.Init();
        this.PlayerDelegate();
        this.onCam = () => {
            this.isOver = true;
            this.reStartButton.SetActive(true);
        };
        this.fadeIn.onFadeInComplete1 = () =>{
            this.isCanStart = true;
        };

        this.eagle.GetComponent<EagleMove>().onEagleTargetOn = () => {
            this.player.onDie();
            GameObject playerBody = GameObject.FindGameObjectWithTag("PlayerBody");
            playerBody.SetActive(false);
            Debug.Log("독수리로 인해 사망");
        };
        this.onOut = () =>{
            StartCoroutine(this.CoZoomIn());
            this.playerRaftDieSound.Play();
            this.player.onDie();
        };
        this.SetTopPointAndCoin();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.isOver==false) this.mapGenerator.CreateGround();
        if(this.isCanStart)this.GameStart();
        if (isMove) this.UpdateUIPointAndGold();
        if (this.camCheck.CheckPlayerEagle() == false && this.isOut==false)
        {
            this.isOut = true;
            this.player.IsOut = true;
            this.onOut();
            this.eagle.SetActive(true);
        }

        if (this.camCheck.CheckPlayerRaft() && this.isOut == false)
        {
            this.isOut = true;
            this.player.IsOut = true;
            this.player.gameObject.SetActive(false);
            this.onOut();
        }
        if (this.isOver)
        {
            this.Restart();
        }

        if (this.mapGenerator.PosZ == 11f) {
             this.fadeIn.gameObject.SetActive(true);
        } 
    }


    private void PlayerDelegate()
    {
        this.player.onCoin = () =>
        {
            coin++;
            Debug.LogFormat("<color=yellow>Coin:{0}</color>", coin);
        };
        this.player.onPoint = () =>
        {
            point++;
            Debug.LogFormat("<color=cyan>Point:{0}</color>", point);
        };
        this.player.onDie = () =>
        {
            Debug.Log("<color=red>Player Die!!!</color>");
            this.txtTopPoint.gameObject.SetActive(true);
            PlayerPrefs.SetInt("coin", this.coin);
            if (this.point > this.topPoint)
            {
                PlayerPrefs.SetInt("top_point", this.point);
                this.topPoint = this.point;
            }
            this.txtTopPoint.text = string.Format("TOP\t{0}", this.topPoint);
            this.player.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(this.CoZoomIn());
        };
    }

    private void SetTopPointAndCoin()
    {
        this.coin = PlayerPrefs.GetInt("coin");
        this.topPoint = PlayerPrefs.GetInt("top_point");
        Debug.LogFormat("<color=cyan>topPoint:{0}</color>",this.topPoint);
        Debug.LogFormat("<color=yellow>coin:{0}</color>",this.coin);
    }

    private IEnumerator CoZoomIn()
    {
        while (true)
        {
            this.camMove.Size -= 0.001f;
            this.camCheck.gameObject.transform.position = Vector3.Slerp(this.camCheck.gameObject.transform.position, this.camCheck.StartPos, Time.deltaTime);
            if (this.camMove.Size <= 3.5f)
            {
                break;
            }
        
        }
        yield return new WaitForSeconds(0.5f);
        this.isMove = false;
        yield return null;
        this.onCam();
    }
    private void GameStart()
    {
        if (Input.GetKey(KeyCode.Space)&&this.isOver==false)
        {
            this.isMove = true;
            this.player.IsMoveAble = true;
            this.uiAnim.SetTrigger("GameStart");
        }
        if (this.isMove)
        {
            this.camMove.CamMove();
        }
    }

    private void Restart()
    {
        if (Input.anyKeyDown)
        {
            this.uiAnim.SetTrigger("ReStart");
            this.fadeOut.gameObject.SetActive(true);
        }
    }

    private void UpdateUIPointAndGold()
    {
        this.txtPoint.text = string.Format("{0}", this.point);
        this.txtCoin.text = string.Format("{0}c", this.coin);
    }
}
