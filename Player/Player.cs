using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Player : MonoBehaviour
{
    private PlayerInfo m_playerinfo;
    private PlayerBag m_playerbag;
    private CharacterController p_cc;
    private Animator p_animator;
    private GameObject player = null;
    public float rotate_Y = 0f;
    
    private void Awake()
    {
        m_playerinfo = new PlayerInfo(Random.Range(1, 1000000));
        m_playerbag = new PlayerBag();
        PlayerMgr.singleton.RegistePlayerSelf(this);
        p_cc = GetComponent<CharacterController>();
        player = GameObject.Find("Player_warrior");
        p_animator = player.transform.GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerinfo.SetState(0);
        p_animator.SetBool(m_playerinfo.GetState(0), true);

    }
    
    // Update is called once per frame
    void Update()
    {
        //todo
        MonstersMgr.singleton.lookatPlayer(player.transform.position);
        
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            Vector3 playerForward = this.transform.TransformDirection(Vector3.forward);
            Vector3 playerRight = this.transform.TransformDirection(Vector3.right);

            p_cc.SimpleMove(playerForward * m_playerinfo.speed * Time.deltaTime * Input.GetAxis("Vertical"));
            p_cc.SimpleMove(playerRight * m_playerinfo.speed * Time.deltaTime * Input.GetAxis("Horizontal"));
            
            p_animator.SetBool(m_playerinfo.GetState(m_playerinfo.state), false);
           
            if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift))
            {
                
                m_playerinfo.SetState(2);
                p_animator.SetBool(m_playerinfo.GetState(2), true);
                m_playerinfo.speed = 200f;
            }
            else
            {
                m_playerinfo.SetState(1);
                p_animator.SetBool(m_playerinfo.GetState(1), true);
                m_playerinfo.speed = 100f;
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                rotate_Y += Input.GetAxis("Horizontal") * m_playerinfo.rotateSpeed[1];
                if (rotate_Y < 0)
                {
                    rotate_Y += 360;
                }
                if (rotate_Y > 360) 
                {
                    rotate_Y -= 360;
                }
                transform.localEulerAngles = new Vector3(0, rotate_Y, 0);
            }
        }
        else
        {
            int t_s = m_playerinfo.GetStateNow();
            if (t_s == 0)
            {
                return;
            }
            p_animator.SetBool(m_playerinfo.GetState(t_s), false);
            m_playerinfo.SetState(0);
            p_animator.SetBool(m_playerinfo.GetState(0), true);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            p_animator.SetTrigger("attackT1");
            CheckEnemy(30);
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            p_animator.SetTrigger("attackT2");
            CheckEnemy(50);
            return;
        }
    }

    private void LateUpdate()
    {

    }

    private void CheckEnemy(int des)
    {
        Debug.LogError("CheckEnemy   !!!!");
        if (MonstersMgr.singleton == null)
        {
            Debug.LogError("singleton is null   !!!!");
        }
        MonstersMgr.singleton.CheckEnemy(des, player.transform.position.x, player.transform.position.z, m_playerinfo.player_R, player.transform.localEulerAngles.y, m_playerinfo.player_angle);
    }  

    public int GetUID()
    {
        return m_playerinfo.uid;
    }

    public int[] GetBagItems()
    {
        return m_playerbag.GetItems();
    }

    public int AddBagItem(int itemID)
    {
        return m_playerbag.AddItem(itemID);

    }

    public int DelBagItem()
    {
        return m_playerbag.DelItem();

    }

}
