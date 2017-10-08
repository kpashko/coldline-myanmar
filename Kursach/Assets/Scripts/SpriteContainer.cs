using UnityEngine;
using System.Collections;

public class SpriteContainer : MonoBehaviour {
    public Sprite[] pLegs, pUnarmedWalk, pPunch, pMac10Walk, pMac10Attack, pBowieWalk, pBowieAttack, ePunch, eMac10Walk, eMac10Attack, eBowieWalk, eBowieAttack, eUnarmedWalk, eWalk;
    public Sprite enemySMG, enemyKnife, enemyUnarmed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Sprite[] getPlayerLegs()
    {
        return pLegs;
    }

    public Sprite[] getPlayerUnarmedWalk()
    {
        return pUnarmedWalk;
    }

	public Sprite[] getPlayerPunch()
	{
		return pPunch;
	}

	public Sprite[] getWeapon(string weapon)
	{
		switch (weapon) {
		case "Mac10":
			return pMac10Attack;
            break;
		case "Bowie":
			return pBowieAttack;
			break;
		default:
			return getPlayerPunch ();
			break;
		}
	}

	public Sprite[] getWeaponWalk(string weapon)
	{
		switch (weapon)
		{
		case "Mac10":
			return pMac10Walk;
			break;
		case "Bowie":
			return pBowieWalk;
			break;
		default:
			return getPlayerUnarmedWalk();
			break;
		}
	}

	public Sprite getEnemySprite(string weapon)
	{
		if (weapon == "Mac10") {
			return enemySMG;
		} else if (weapon == "Bowie") {
			return enemyKnife;
		} else {
			return enemyUnarmed;
		}
	}

    public Sprite[] getEnemyPunch()
    {
        return ePunch;
    }

    public Sprite[] getEnemyWeapon(string weapon)
    {
        switch (weapon)
        {
            case "Mac10":
                return eMac10Attack;
                break;
            case "Bowie":
                return eBowieAttack;
                break;
            default:
                return getEnemyPunch();
                break;
        }
    }

    public Sprite[] getEnemyWalk(string weapon)
    {
        switch (weapon)
        {
            case "Mac10":
                return eMac10Walk;
                break;
            case "Bowie":
                return eBowieWalk;
                break;
            default:
                return eUnarmedWalk;
                break;
        }
    }
}
