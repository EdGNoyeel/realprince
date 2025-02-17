using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class BomberMan : Enemy
{
    public bool canSelfDestruction=true;
    [SerializeField]
    private float selfDestructionTime;
    //[SerializeField]
    //private float damage;
    [SerializeField]
    GameObject explsion;

    //private bool explosion = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        base.Targetting();
        //TrySelfDest();
        SeletLine();
        targetBeacon.SetActive(true);
    }

    void TrySelfDest()
    {
        canSelfDestruction = true;
    }

    public override void KillCount()
    {
        int killcount = StatusManager.instance.killNumberD;
        killcount++;
        StatusManager.instance.killNumberD = killcount;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetTooth != null)
        {
            if (base.targetTooth.activeSelf == true)
            {
                if (base.canMove)
                {
                    base.Move();
                    //Debug.Log("이동중");
                }
                else
                {
                    if (canSelfDestruction)
                    {
                        canSelfDestruction = false;
                        Debug.Log("자폭계시");
                        SelfDestruction();
                    }
                }
            }
        }
        
        else
            base.Targetting();
    }
    private void SelfDestruction()
    {
        Invoke("Explosion", selfDestructionTime);
    }

    private void Explosion()
    {
        Debug.Log("꽝");
        
        //explosion = true;
        base.anim.SetTrigger("SelfDest");
        Invoke("Bang", 1);
        //Invoke("Death",2);
        //Death();
        
    }

    void Bang()
    {
        explsion.SetActive(true);
        GameObject exEf = Instantiate(EffectPrefab);
        exEf.transform.SetParent(this.transform, false);
        Timing.RunCoroutine(FadeAway().CancelWith(gameObject));
        CancelInvoke();
        KillCount();
        targetBeacon.SetActive(false);
    }

    private void Death()
    {
        canSelfDestruction = true;
        base.Damage(100);
    }

}
