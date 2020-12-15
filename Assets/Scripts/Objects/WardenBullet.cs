using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenBullet : MonoBehaviour
{
	[SerializeField] Rigidbody2D rb;
	float speed;
	[SerializeField] float timeDuration = 20f;
	Vector2 direction;
	[SerializeField] float damage = 1f;
	[SerializeField] float trackingamount = .1f;
	GameObject player;
	bool fromPlayer;


	private void Start()
	{
		GetComponents();
		Destroy(gameObject, timeDuration); //destroy this after the duration
	}

	public void Initialize(Vector3 dir, float Speed, bool FromPlayer)
	{
		speed = Speed;
		direction = new Vector2(dir.x, dir.y).normalized;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
		fromPlayer = FromPlayer;
	}

	public void FixedUpdate()
	{
		Vector2 toPlayer = gameObject.transform.position - player.transform.position;
		float angle = Vector2.SignedAngle(transform.up, toPlayer);
		if (angle > 0)
		{
			direction = direction + new Vector2(transform.right.x, transform.right.y) * trackingamount;
		}
		else
		{
			direction = direction - new Vector2(transform.right.x, transform.right.y) * trackingamount;
		}
		direction = direction.normalized;
		transform.up = direction;
		//update the position 60 times a second
		rb.MovePosition(new Vector2(transform.position.x + direction.x * speed * Time.deltaTime, transform.position.y + direction.y * speed * Time.deltaTime));
	}

	void GetComponents()
	{
		if (rb == null)
		{
			rb = GetComponent<Rigidbody2D>();
		}

		player = GameObject.Find("PlayerChar");
	}

	/// <summary>
	/// Handles all the collision when things are hit by the bullet
	/// </summary>
	/// <param name="other"></param>
	public void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.gameObject.tag)
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
				if (!fromPlayer)
				{
					other.gameObject.GetComponent<PlayerStats>().TakeDamage((int)damage);
					Destroy(gameObject);
				}
				break;
			case "Bullet":
				// Do nothing so bullets don't break when hitting each other
				break;
			case "Enemy":
				if (fromPlayer)
				{
					// DO damage to enemy
					other.gameObject.GetComponent<BreakableObjectScript>().GetHit(1f);
					Destroy(gameObject);
				}
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

}
