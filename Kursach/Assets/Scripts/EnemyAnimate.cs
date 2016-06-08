using UnityEngine;
using System.Collections;

public class EnemyAnimate : MonoBehaviour {
    Sprite[] torsoSpr, attackingSpr, legsSpr;
    int tCounter = 0, aCounter = 0, lCounter = 0;
    EnemyAI eai;
    float torsoTimer = 0.15f, legsTimer = 0.15f, legReset = 0.15f, torsoReset = 0.15f;
    public SpriteRenderer torso, legs; 
    SpriteContainer sc;
    public bool attacking = false;

	void Start ()
    {
        eai = this.GetComponent<EnemyAI>();
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
        torsoSpr = sc.getEnemyWalk("");
        attackingSpr = sc.getEnemyWeapon("");
		legsSpr = sc.eWalk;
    }
	
	void Update ()
    {
	    if(eai.moving == true)
        {
            animateLegs();
        }

        animateTorso();

        legResetSpeed();
	}

    void animateTorso()
    {
        torsoTimer -= Time.deltaTime;

        if (attacking == false)
        {
            if (torsoSpr.Length > tCounter)
            {
                torso.sprite = torsoSpr[tCounter];
            }
            else
            {
                torso.sprite = torsoSpr[0];
            }
            if (torsoTimer <= 0)
            {
                if (tCounter < torsoSpr.Length - 1)
                {
                    tCounter++;
                }
                else
                {
                    tCounter = 0;
                }
                torsoTimer = torsoReset;
            }
        }
        else
        {
            torso.sprite = attackingSpr[aCounter];
            if (torsoTimer <= 0)
            {
                if (aCounter < attackingSpr.Length - 1)
                {
                    aCounter++;
                }
                else
                {
                    attacking = false;
                    aCounter = 0;
                }
            }
            torsoTimer = torsoReset;
        }
    }

    void animateLegs()
    {
        legs.sprite = legsSpr[lCounter];
        legsTimer -= Time.deltaTime;
        if (legsTimer <= 0)
        {
            if(lCounter < legsSpr.Length - 1)
            {
                lCounter++;
            }
            else
            {
                lCounter = 0;
            }
            legsTimer = legReset;
        }
    }

    void legResetSpeed()
    {
        if(eai.getSpeed() > 2.1f)
        {
            legReset = 0.03f;
            torsoReset = 0.03f;
        } else
        {
            legReset = 0.05f;
            torsoReset = 0.05f;
        }
    }

    public void setAttacking()
    {
        attacking = true;
    }

    public void setTorsoSpr(string name)
    {
        torsoSpr = sc.getEnemyWalk(name);
        attackingSpr = sc.getEnemyWeapon(name);
        tCounter = 0;
        aCounter = 0;
    }
}
