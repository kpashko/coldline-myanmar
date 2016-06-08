using UnityEngine;
using System;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	GameObject player;
	public bool patrol = true, guard = false, clockwise = false;
	public bool moving = true;
	public bool pursuingPlayer = false, goingToLastLoc = false;
	Rigidbody2D rid;
	public Vector3 playerLastPos;
	RaycastHit2D hit;
	float speed = 2.0f;
	int layerMask = 1<<8;

	ObjectManager obj;
	GameObject[] weapons;
	EnemyWeaponController ewc;
	public GameObject weaponToGoTo;
    public bool goingToWeapon = false;
	public bool hasGun = false;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerLastPos = this.transform.position;
		obj = GameObject.FindGameObjectWithTag("GameController").GetComponent<ObjectManager>();
        ewc = this.GetComponent<EnemyWeaponController>();
        rid = this.GetComponent<Rigidbody2D> ();
		layerMask = ~layerMask;
	}

	void Update()
    {
        if (PlayerHealth.dead == false)
        {
            playerDetect();
            canEnemyFindWeapon();
            movement();
        }
        else
        {
            this.GetComponent<EnemyAnimate>().enabled = false;
            this.GetComponent<EnemyWeaponController>().enabled = false;
        }
	}

	void movement()
    {
		Vector2 distance = player.transform.position - transform.position;
        hit = Physics2D.Raycast(new Vector2 (this.transform.position.x, this.transform.position.y), distance, Vector2.Distance(player.transform.position, this.transform.position), layerMask);
		Debug.DrawRay (transform.position, distance, Color.red);

		Vector2 fwt = this.transform.TransformDirection (Vector2.right);
        RaycastHit2D hit2 = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y), fwt, 1.0f, layerMask);
		Debug.DrawRay (new Vector2 (this.transform.position.x, this.transform.position.y), fwt, Color.cyan);

		if (moving == true)
        {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}

		if (patrol == true)
        {
			speed = 2.0f;

			if (hit2.collider != null)
            {
                if (hit2.collider.gameObject.tag != "Bullet")
                {
                    if (clockwise == false)
                    {
						transform.Rotate (0, 0, 90);
					}
                    else
                    {
						transform.Rotate (0, 0, -90);
					}
				}
			}  
		}

		if (pursuingPlayer == true)
        {
			speed = 3.5f;
			rid.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);

			if (hit.collider.gameObject.tag == "Player")
            {
				playerLastPos = player.transform.position;
			}
		}

		if (goingToLastLoc == true)
        {
			speed = 3.0f;
			rid.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);

			if (Vector3.Distance (this.transform.position, playerLastPos) < 1.5f)
            {
				patrol = true;
				goingToLastLoc = false;
			}
		}

		if(goingToWeapon == true)
        {
            speed = 3.0f;
			rid.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((weaponToGoTo.transform.position.y - transform.position.y), (weaponToGoTo.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
			if (ewc.getCur () != null || weaponToGoTo.activeSelf == false)
            {
				weaponToGoTo = null;
				patrol = true;
				goingToWeapon = false;
				pursuingPlayer = false;
				goingToLastLoc = false;
			}
		}					
	}


	void setWeaponToGoTo(GameObject weapon)
	{	
        if (pursuingPlayer == false)
        {
            weaponToGoTo = weapon;
            goingToWeapon = true;
            patrol = false;
            goingToLastLoc = false;
        }
	}

	void canEnemyFindWeapon()
	{
		if (ewc.getCur() == null && weaponToGoTo == null && goingToWeapon == false)
        {
			weapons = obj.getWeapons ();
			for (int i = 0; i < weapons.Length; i++)
            {
				if (AIcanSeeObject(weapons[i]))
                {
					setWeaponToGoTo(weapons[i]);
                    break;
				}
			}
		}
	}

    public bool AIcanSeeObject(GameObject objectToSee)
    {
        bool visible = false;

        Vector2 distanceVector2 = objectToSee.transform.position - transform.position;
        RaycastHit2D rhCheck = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), distanceVector2, Vector2.Distance(objectToSee.transform.position, this.transform.position), layerMask);

        float distance = Vector3.Distance(this.transform.position, objectToSee.transform.position);
        if (distance < 9.0f && 5 * this.transform.InverseTransformPoint(objectToSee.transform.position).x > 0.18 * distance && rhCheck.collider.gameObject == objectToSee)
        {
            visible = true;
        }
        return visible;
    }

	public void playerDetect()
	{
		if (hit.collider != null)
        {
            if (AIcanSeeObject(player))
            {
				patrol = false;
                weaponToGoTo = null;
                goingToWeapon = false;
                pursuingPlayer = true;
			}
            else
            {
				if (pursuingPlayer == true)
                {
					goingToLastLoc = true;
					pursuingPlayer = false;
				}
			}
		}
	}

    public float getSpeed()
    {
        return speed;
    }
}


