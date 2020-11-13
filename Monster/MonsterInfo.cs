using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterInfo 
{
    public MonsterInfo(GameObject mons)
    {
        blood = 100;
        isDead = false;
        monR = 0.5f;
        monsObject = mons;
        m_animator = monsObject.transform.GetComponent<Animator>();
    }

    public bool GetisDead() { return isDead; }
    public int Getblood() { return blood; }
    public float GetmonR() { return monR; }

    public void Gethurt(int des)
    {
        blood -= des;
        if (blood <= 0)
        {
            blood = 0;
            isDead = true;
            monsObject.transform.GetComponent<Animator>().SetTrigger("ToDeath");
        }
        return;
    }

    public void lookatPlayer(Vector3 pos)
    {

        if (isDead)
        {
            Debug.LogError("monster already dead.");
            return;

        }
        Vector3 tmp = pos - monsObject.transform.position; //获得enemy到player的向量
        tmp.y = 0; //y 高度保持不变
        monsObject.transform.rotation = Quaternion.Slerp(monsObject.transform.rotation, Quaternion.LookRotation(tmp), 0.3f);
        return;
    }

    //伤害、坐标（x、z）、攻击距离、朝向、攻击范围（扇形角度）
    public void CheckEnemy(int des, float px, float pz, float player_R, float forward, float player_angle)
    {
        float mx = monsObject.transform.position.x;
        float mz = monsObject.transform.position.z;

        float dis = Vector2.Distance(new Vector2(mx, mz), new Vector2(px, pz));

        if (dis <= monR + player_R)
        {
            float angle = Vector2.Angle(new Vector2(mx - px, mz - pz), new Vector2(0, 1f));  // player到monster 在平面坐标和基准向量的角度
            if (mx - px < 0)
            {
                angle = 360 - angle;
            }

            float angle2 = Mathf.Acos((player_R * player_R + dis * dis - monR * monR) / (2 * player_R * dis)) * Mathf.Rad2Deg;    //两个圆的交点和player连线向量在品面坐标中的角度

            float incl = Mathf.Abs(forward - angle);
            incl = incl > 180 ? 360 - incl : incl;

            /*
            Debug.LogError("angel ----------------- " + angle);
            Debug.LogError("forward -------- " + forward);
            Debug.LogError("incl -------- " + incl);
            Debug.LogError("angel2 ----------------- " + angle2);
            */
            if ((incl - angle2) * 2f <= player_angle)
            {
                Gethurt(des);
                Debug.LogError("Player to Monster dir = " + dis + " blood = " + blood);
            }

        }
        return;
    }
    private bool isDead;
    private int blood;
    private float monR;
    private GameObject monsObject;
    private Animator m_animator;
}
