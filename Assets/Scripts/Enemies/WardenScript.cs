using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WardenScript : MonoBehaviour
{
	[SerializeField] GameObject enemyTarget;
	[SerializeField] float hearingRadius = 4f;
	[SerializeField] EnemyVisionCone visionCone;
	[SerializeField] float playerSpottingEscapeTime = 2f;
	[SerializeField] float investigationTime = 4f;
	[SerializeField] LayerMask playerLayer;
	[SerializeField] LayerMask visionObstructingLayer;
	[SerializeField] float SweepSpeedInDegreesPerSecond = 40f;
	[SerializeField] WardenWeaponScript weapon;
	[SerializeField] float distanceToPlayerToStartShooting = 3f;
	[SerializeField][Range(.5f,3f)] float UpdatePlayerTarget = 1f;
	Vector3 nextPatrolPoint = new Vector3();
	public float timeSinceLastSpottedPLayer = 0f;
	public float updateLocationTimer = 0f;
	public AIState state = AIState.Patrol;
	[SerializeField]float minPatrolDistance = 0f, maxPatrolDistance = 3f;

	private void Start()
	{

		if (visionCone == null)
		{
			visionCone = GetComponentInChildren<EnemyVisionCone>();
		}

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
		if (visionCone.CanSeePlayer(playerLayer))
		{
			Vector3 dir = PlayerInfo.Instance.playerPos.transform.position - transform.position;
			RaycastHit2D line = Physics2D.Raycast(transform.position, dir, dir.magnitude, visionObstructingLayer);
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
			}
			else
			{
				enemyTarget.transform.position = PlayerInfo.Instance.playerPos.position;
			}

			if (weapon.CanShoot)
			{
				weapon.Attack();
			}
		}
		else
		{
			enemyTarget.transform.position = PlayerInfo.Instance.playerPos.position;
			timeSinceLastSpottedPLayer += Time.deltaTime;
			if (timeSinceLastSpottedPLayer > playerSpottingEscapeTime)
			{
				state = AIState.Wait;
			}
		}
	}

	private void UpdatePathToAction()
	{
		if (CloseToTarget())
		{
			state = AIState.Patrol;
		}
	}

	private void UpdatePatrol()
	{
		updateLocationTimer -= Time.deltaTime;
		if (CloseToTarget() || updateLocationTimer < 0)
		{
			GetNewPatrolTarget();
		}
	}

	private void GetNewPatrolTarget()
	{
		enemyTarget.transform.position = PlayerInfo.Instance.playerPos.position + new Vector3(Random.Range(1, 2), Random.Range(1, 2)).normalized * Random.Range(minPatrolDistance, maxPatrolDistance);
	}

	private bool CloseToTarget(float distance = .01f)
	{
		return (transform.position - enemyTarget.transform.position).magnitude < distance;
	}
}
