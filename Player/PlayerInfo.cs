using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo 
{
    public PlayerInfo(int id)
    {
        state = 0;
        uid = id;
    }

    public void SetState(int index)
    {
        state = index;
    }

    public string GetState(int index )
    {
        return statemap[index];
    }

    public int GetStateNow()
    {
        return state;
    }

    //=================================================================//
    public int uid;

    public int state; //0 - stand , 1 - walk, 2 - run, 3 - attack
    private string[] statemap = { "stand", "walk", "run", "attack" };

    public float speed = 100f;
    public float[] rotateSpeed = { 0, 2f, 0 };  // x/y/z

    public float player_R = 0.8f;
    public float player_angle = 120f;

}
