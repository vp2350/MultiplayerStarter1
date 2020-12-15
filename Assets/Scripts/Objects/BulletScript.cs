using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[SerializeField]Rigidbody2D rb;
	float speed;
	[SerializeField] float timeDuration = 20f;
	Vector2 direction;
	[SerializeField] float damage = 1f;
	bool fromPlayer;
	[SerializeField] GameObject bloodSplatter;


	private void Start()
	{
		GetComponents();
		Destroy(gameObject, timeDuration); //destroy this after the duration
	}

	public void Initialize(Vector3 dir, float Speed, bool FromPlayer)
	{
		speed = Speed;
		direction = new Vector2(dir.x,dir.y).normalized;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
		fromPlayer = FromPlayer;
	}

	public void FixedUpdate()
	{
		//update the position 60 times a second
		rb.MovePosition(new Vector2(transform.position.x + direction.x * speed * Time.deltaTime, transform.position.y + direction.y * speed * Time.deltaTime));
	}

	void GetComponents()
	{
		if (rb == null)
		{
			rb = GetComponent<Rigidbody2D>();
		}
	}

	/// <summary>
	/// Handles all the collision when things are hit by the bullet
	/// </summary>
	/// <param name="other"></param>
	public void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.gameObject.tag)
		{
			case "Damageable":
				// Do damage to other object then destroy this bullet
				Destroy(gameObject); 
				break;
			case "Breakable":
				// Break the other object then destroy this bullet
				other.gameObject.GetComponent<BreakableObjectScript>().GetHit(damage);
				Destroy(gameObject);
				break;
			case "Player":
				if(!fromPlayer)
                {
					other.gameObject.GetComponent<PlayerStats>().TakeDamage((int)damage);
					SpawnSplat();
					Destroy(gameObject);

				}

				break;
			case "Enemy":
				if(fromPlayer)
                {
					// DO damage to enemy
					other.gameObject.GetComponent<BreakableObjectScript>().GetHit(1f);
					SpawnSplat();
					Destroy(gameObject);
				}
				break;
			case "Bullet":
				// Do nothing so bullets don't break when hitting each other
				break;
			case "Wall":
				// Destroy this bullet
				Destroy(gameObject);
				break;
			default:
				// Do nothing
				break;

		}

	}

	public void SpawnSplat()
	{
		if (bloodSplatter != null)
		{
			GameObject splat = Instantiate(bloodSplatter);
			splat.transform.position = gameObject.transform.position;
			splat.transform.forward = transform.up;
		}
	}
}
