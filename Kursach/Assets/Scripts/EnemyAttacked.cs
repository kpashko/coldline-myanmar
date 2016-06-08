using UnityEngine;
using System.Collections;

public class EnemyAttacked : MonoBehaviour {
    public Sprite knockedDown, stabbed, bulletWound, backUp;
    public GameObject bloodPool, bloodSpurt;
    SpriteRenderer sr;
    bool EnemyKnockedDown = false;
    float knockDownTimer = 3.0f;
    GameObject player;
	ScoreController sc;

	void Start ()
    {
        sr = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
		sc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScoreController> ();
	}
	
	void Update ()
    {
	    if(EnemyKnockedDown == true)
        {
            knockDown();
        }
	}

    public void knockDownEnemy()
    {
        EnemyKnockedDown = true;
    }

    void knockDown()
    {
		
		if (this.GetComponent<EnemyWeaponController> ().enabled == true)
        {
			sc.AddScore (500, this.transform.position);
            this.GetComponent<EnemyWeaponController>().dropWeapon();
            sr.sprite = knockedDown;
            this.GetComponent<CircleCollider2D>().enabled = false;
            sr.sortingOrder = 2;
            this.GetComponent<EnemyAI>().enabled = false;
            this.GetComponent<EnemyAnimate>().enabled = false;
            this.GetComponent<EnemyWeaponController> ().enabled = false;
		}
			
        knockDownTimer -= Time.deltaTime;

        if(knockDownTimer <= 0)
        {
            EnemyKnockedDown = false;
            sr.sprite = backUp;
            this.GetComponent<CircleCollider2D>().enabled = true;
			this.GetComponent<EnemyAI> ().enabled = true;
            this.GetComponent<EnemyWeaponController>().enabled = true;
            this.GetComponent<EnemyAnimate>().enabled = true;
            sr.sortingOrder = 5;
            knockDownTimer = 3.0f;
        }
    }

    public void killBullet()
    {
		sc.AddScore (500, this.transform.position);
		sc.increaseMultiplier ();
		this.GetComponent<EnemyWeaponController> ().dropWeapon();
		this.GetComponent<EnemyWeaponController> ().enabled = false;
        sr.sprite = bulletWound;
        Instantiate(bloodPool, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 2;
		this.GetComponent<EnemyAI> ().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<EnemyAnimate>().enabled = false;
        this.gameObject.tag = "Dead";
    }

    public void killMelee()
    {
		sc.AddScore (1000, this.transform.position);
		sc.increaseMultiplier ();
		this.GetComponent<EnemyWeaponController> ().dropWeapon();
		this.GetComponent<EnemyWeaponController> ().enabled = false;
        sr.sprite = stabbed;
        Instantiate(bloodPool, this.transform.position, this.transform.rotation);
        Instantiate(bloodSpurt, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 2;
		this.GetComponent<EnemyAI> ().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<EnemyAnimate>().enabled = false;
        this.gameObject.tag = "Dead";
    }
}
