using UnityEngine;
using System.Collections;

public class EnemyWeaponController : MonoBehaviour {

	public GameObject oneHandSpawn, twoHandSpawn, bullet, blood;
	GameObject curWeapon;
	public bool gun = false;
	float weaponFireRate = 0.1f, timerReset = 0.1f;

    SpriteContainer sc;

	float weaponChange = 0.5f;
	bool changingWeapon = false;
	bool oneHanded = false;

	EnemyAI eai;
	GameObject player;

    bool attacking = false;
	SpriteRenderer sr;
    EnemyAnimate ea;

	void Start ()
    {
		eai = this.GetComponent<EnemyAI> ();
		player = GameObject.FindGameObjectWithTag ("Player");
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
		sr = this.GetComponent<SpriteRenderer> ();
        ea = this.GetComponent<EnemyAnimate>();
	}
	
	void Update ()
    {
		if (gun == true)
        {
			eai.hasGun = true;
		}
        else
        {
			eai.hasGun = false;
		}

		if (weaponFireRate > 0) {
            weaponFireRate -= Time.deltaTime;
		}

        if (PlayerHealth.dead == false)
        {
            if (eai.hasGun == false && gun == false && eai.pursuingPlayer == true && weaponFireRate <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= 1.6f)
            {
                attack();
                ea.setAttacking();
            }
            else if (eai.hasGun == true && eai.pursuingPlayer == true && weaponFireRate <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= 5.0f)
            {
                attack();
                ea.setAttacking();
            }
        }

		if (changingWeapon == true)
        {
			weaponChange -= Time.deltaTime;
			if(weaponChange <= 0)
            {
				changingWeapon = false;
                weaponChange = 0.5f;
            }
		}
	}

	public void setWeapon(GameObject cur, string name, float fireRate, bool gun, bool oneHanded)
	{
		changingWeapon = true;
		curWeapon = cur;
		this.gun = gun;
		timerReset = fireRate;
		weaponFireRate = timerReset;
		this.oneHanded = oneHanded;
        ea.setTorsoSpr(name);
	}

	public void attack ()
    {			
		if (gun == true)
        {
			Bullet bl = bullet.GetComponent<Bullet> ();
			Vector3 dir;
			dir.x = Vector3.right.x;
			dir.y = Vector3.right.y;
			dir.z = 0;
			bl.setVals (dir, "Enemy");
			if (oneHanded == true)
            {
				Instantiate (bullet, oneHandSpawn.transform.position, this.transform.rotation);
			}
            else
            {
				Instantiate (bullet, twoHandSpawn.transform.position, this.transform.rotation);
			}
            weaponFireRate = timerReset;		
		}
        else
        {
			int layerMask = 1 << 8;
			layerMask = ~layerMask;

			RaycastHit2D ray = Physics2D.Raycast (new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2 (transform.right.x, transform.right.y), 1.5f, layerMask);
			Debug.DrawRay (new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2 (transform.right.x, transform.right.y), Color.green);

			if (ray.collider.gameObject.tag == "Player")
            {
                PlayerHealth.dead = true;
                Instantiate(blood, this.transform.position, this.transform.rotation);
            }

			Instantiate (blood, player.transform.position, player.transform.rotation);
            weaponFireRate = timerReset;
		}
	}

	public GameObject getCur()
	{
		return curWeapon;
	}

	public void dropWeapon()
	{
		if (curWeapon != null)
        {
			curWeapon.GetComponent<Rigidbody2D> ().isKinematic = false;
			curWeapon.transform.position = oneHandSpawn.transform.position;
			curWeapon.transform.eulerAngles = this.transform.eulerAngles;
			curWeapon.SetActive (true);
			setWeapon (null, "", 0.5f, false, false);
		}
	}
}
