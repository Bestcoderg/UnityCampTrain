using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    UIManager ui_manager_;
    public List<MBaseSingleton> mSingletons;
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        ui_manager_ = new UIManager();
        //Debug.LogError("GameEntity awake !!!");
        
        mSingletons = new List<MBaseSingleton>();
        mSingletons.Add(MonstersMgr.singleton);
        mSingletons.Add(PlayerMgr.singleton);

    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.LogError("GameEntity start !!!");
        
        for (int i=0;i<mSingletons.Count;i++)
        {
            mSingletons[i].Init();
        }
        ui_manager_.OnMainCity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        for (int i = 0; i < mSingletons.Count; i++)
        {
            mSingletons[i].Uninit();

        }
    }

}
