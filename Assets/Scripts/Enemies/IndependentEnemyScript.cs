using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum AIState
{
	Patrol,
	PathtoAction,
	Engage,
	Wait
}

public class IndependentEnemyScript : MonoBehaviour
{
	[SerializeField] GameObject enemyTarget;
	[SerializeField] float hearingRadius = 4f;
	[SerializeField] EnemyVisionCone visionCone;
	[SerializeField] float playerSpottingEscapeTime = 2f;
	[SerializeField] float investigationTime = 4f;
	[SerializeField] LayerMask playerLayer;
	[SerializeField] LayerMask visionObstructingLayer;
	[SerializeField] PatrolPath path;
	[SerializeField] float SweepSpeedInDegreesPerSecond = 40f;
	[SerializeField] EnemyWeaponScript weapon;
	[SerializeField] float distanceToPlayerToStartShooting = 3f;
	[SerializeField] float visionAngle = 17.5f;
	Vector3 nextPatrolPoint = new Vector3();
	public float timeSinceLastSpottedPLayer = 0f;
	public float waitTimer = 0f;
	[SerializeField] float spotWaitTime = .5f;
	public float spotTimer = 0f;
	public AIState state = AIState.Patrol;

	private void Start()
	{

		if (visionCone == null)
		{
			visionCone = GetComponentInChildren<EnemyVisionCone>();
		}

		if (path == null)
		{
			Debug.LogError("enemy has no patrol path");
		}

		enemyTarget.transform.position = path.nextPoint;
		nextPatrolPoint = enemyTarget.transform.position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, hearingRadius);
		Gizmos.DrawLine(transform.position, transform.position + transform.up * 6);
	}

	private void Update()
	{
		UpdateAiState();
		weapon.UpdateShootingTimer();

		if (state == AIState.Engage)
		{
			if (AudioManager.Instance.currentState != AudioManager.AudioState.Combat && AudioManager.Instance.currentState != AudioManager.AudioState.StealthToCombat)
			{
				AudioManager.Instance.currentState = AudioManager.AudioState.StealthToCombat;
			}

			UpdateEngaged();
			return;
		}

		if (state == AIState.PathtoAction)
		{
			UpdatePathToAction();
		}

		if (state == AIState.Patrol)
		{
			UpdatePatrol();
		}

		if (state == AIState.Wait)
		{
			UpdateWait();
		}


	}

	private void UpdateAiState()
	{

		if (state != AIState.Engage)
		{

			if (SpotPlayer())//if we are not engaged already and we've spotted the player
			{
				state = AIState.Engage;
				enemyTarget.transform.position = PlayerInfo.Instance.playerPos.position;
				return;
			}
			if (DetectAction())
			{
				state = AIState.PathtoAction;
				enemyTarget.transform.position = PlayerInfo.Instance.playerPos.position;
				return;
			}

		}

	}

	private bool SpotPlayer()
	{
		if (visionCone.CanSeePlayer(playerLayer)){
			Vector3 dir = PlayerInfo.Instance.playerPos.transform.position - transform.position;
			RaycastHit2D line = Physics2D.Raycast(transform.position, dir,dir.magnitude, visionObstructingLayer);
			if (!line)
			{
				return true;
			}
		}

		return false;
	}

	private bool DetectAction()
	{
		return PlayerInfo.Instance.hasShot && (PlayerInfo.Instance.playerPos.transform.position - transform.position).magnitude < hearingRadius;
	}


	private void UpdateEngaged()
	{

		Vector3 playerToThis = PlayerInfo.Instance.playerPos.position - transform.position;


		if (SpotPlayer())
		{
			timeSinceLastSpottedPLayer = 0f;
			spotTimer += Time.deltaTime;
			enemyTarget.transform.position = PlayerInfo.Instance.playerPos.position;
			if (playerToThis.magnitude < distanceToPlayerToStartShooting)
			{
				enemyTarget.transform.position = transform.position;
				float angleBetween = Vector3.Angle(playerToThis, transform.up);
				if (Mathf.Abs(angleBetween) > 1f)
				{
					float angle = Mathf.Atan2(playerToThis.y, playerToThis.x) * Mathf.Rad2Deg - 90f;
					Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
					transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * SweepSpeedInDegreesPerSecond);
				}
				if (Mathf.Abs(angleBetween) < visionAngle && weapon.CanShoot && spotTimer > spotWaitTime)
				{
					weapon.Attack();
				}
			}


		}
		else
		{

			timeSinceLastSpottedPLayer += Time.deltaTime;
			if (timeSinceLastSpottedPLayer > playerSpottingEscapeTime)
			{
				spotTimer = 0;
				state = AIState.Wait;
			}
		}


	}

	private void UpdatePathToAction()
	{
		if (CloseToTarget())
		{
			state = AIState.Wait;
		}
	}

	private void UpdatePatrol()
	{
		if (CloseToTarget())
		{
			Vector2 nextpathPoint = path.nextPoint;
			nextPatrolPoint = nextpathPoint;
			enemyTarget.transform.position = new Vector3(nextpathPoint.x, nextpathPoint.y, enemyTarget.transform.position.z);
		}
	}

	private void UpdateWait()
	{
		float rotation = SweepSpeedInDegreesPerSecond * Time.deltaTime + transform.localEulerAngles.z;
		gameObject.transform.rotation = Quaternion.Euler(0f,0f,rotation);

		waitTimer += Time.deltaTime;
		if (waitTimer > investigationTime)
		{
			waitTimer = 0f;
			enemyTarget.transform.position = nextPatrolPoint;
			state = AIState.Patrol;
		}
	}


	private bool CloseToTarget(float distance = .01f)
	{
		return (transform.position - enemyTarget.transform.position).magnitude < distance;
	}
}
