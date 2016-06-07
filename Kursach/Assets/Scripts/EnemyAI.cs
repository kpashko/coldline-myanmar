using UnityEngine;
using System;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	GameObject player;
	public bool patrol = true, guard = false, clockwise = false;
	public bool moving = true;
	public bool pursuingPlayer = false, goingToLastLoc = false;
	Vector3 target;
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

	void Update() {
        if (PlayerHealth.dead == false)
        {
            movement();
            playerDetect();
            canEnemyFindWeapon();
        }
        else
        {
            this.GetComponent<EnemyAnimate>().enabled = false;
            this.GetComponent<EnemyWeaponController>().enabled = false;
        }
	}

	void movement() {
		float dist = Vector3.Distance (player.transform.position, this.transform.position);
		Vector3 dir = player.transform.position - transform.position;
        hit = Physics2D.Raycast(new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2 (dir.x, dir.y), dist, layerMask);
		Debug.DrawRay (transform.position, dir, Color.red);

		Vector3 fwt = this.transform.TransformDirection (Vector3.right);

        RaycastHit2D hit2 = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2 (fwt.x, fwt.y), 1.0f, layerMask);

		Debug.DrawRay (new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2 (fwt.x, fwt.y), Color.cyan);

		if (moving == true)
        {
			if (hasGun == false)
            {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			} else
            {
				if (Vector3.Distance (this.transform.position, player.transform.position) < 5 && pursuingPlayer == true)
                {
					//new enemy weapon
				} else
                {
					transform.Translate (Vector3.right * speed * Time.deltaTime);
				}
			}
		}

		if (patrol == true) {
			//Debug.Log ("Patrolling normally");
			speed = 2.0f;

			if (hit2.collider != null) {

                //if (hit2.collider.gameObject.tag == "Wall") {
                if (hit2.collider.gameObject.tag != "Player")
                {

                    if (clockwise == false) {
						
						transform.Rotate (0, 0, 90);
					} else {
						transform.Rotate (0, 0, -90);
					}
				}
			}
            if(weaponToGoTo != null)
            {
                patrol = false;
                goingToWeapon = true;
            }
                
		}

		if (pursuingPlayer == true) {
			//Debug.Log ("Pusuing player");
			speed = 3.5f;
			rid.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);

			if (hit.collider.gameObject.tag == "Player") {
				playerLastPos = player.transform.position;
			}
		}

		if (goingToLastLoc == true) {
			//Debug.Log ("Checking last known player location");
			speed = 3.0f;
			rid.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);

			if (Vector3.Distance (this.transform.position, playerLastPos) < 1.5f) {
				patrol = true;
				goingToLastLoc = false;
					}
				}

		if(goingToWeapon == true){
            
            speed = 3.0f;
			rid.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((weaponToGoTo.transform.position.y - transform.position.y), (weaponToGoTo.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
			if (ewc.getCur () != null || weaponToGoTo.activeSelf == false || weaponToGoTo) {
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
		weaponToGoTo = null;
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
		if (ewc.getCur () == null && weaponToGoTo == null && goingToWeapon == false)
        {
			weapons = obj.getWeapons ();
			for (int x = 0; x < weapons.Length; x++)
            {
				float distance = Vector3.Distance (this.transform.position, weapons [x].transform.position);
				if (distance < 10)
                {
					Vector3 dir = weapons [x].transform.position - transform.position;
					RaycastHit2D wepCheck = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2 (dir.x, dir.y), distance, layerMask);
					if (wepCheck.collider.gameObject.tag == "Weapon")
                    {
						setWeaponToGoTo (weapons [x]);
					}
				}
			}
		}
	}


	public void playerDetect()
	{
		if (hit.collider != null)
        {
            float distance = Vector3.Distance(this.transform.position, player.transform.position);

            if (hit.collider.gameObject.tag == "Player" && distance < 9.0f && 5 * this.transform.InverseTransformPoint(player.transform.position).x > 0.18 * distance)
            {
				patrol = false;
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


