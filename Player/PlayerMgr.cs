using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MSingleton<PlayerMgr>
{
    //public List<Player> m_PlayerList;
    private Player m_PlayerSelf;
    public Dictionary<int, Player> m_PlayerList;

    public override bool Init()
    {
        m_PlayerList = new Dictionary<int, Player>();
        m_PlayerList.Clear();
        return base.Init();
    }

    public override void Uninit()
    {
        base.Uninit();
    }

    public void OnLogout(bool clearAccountData = true)
    {

    }

    public void RegistePlayerSelf(Player player)
    {
        if (player == null)
        {
            return;
        }
        m_PlayerSelf = player;

    }

    public void MovePlayer(int uif, Vector3 pos, Vector3 rot)
    {
        
    }

    public void BroadMsg(int uif, string msg)
    {

    }

    public int[] GetBagItems()
    {
        return m_PlayerSelf.GetBagItems();
    }

    public int AddBagItem(int itemID)
    {
        return m_PlayerSelf.AddBagItem(itemID);
    }

    public int DelBagItem()
    {
        return m_PlayerSelf.DelBagItem();
    }


}
