using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float walkMoveStopRadius = 0.2f;
	[SerializeField] float attackMoveStopRadius = 5f;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
	Vector3 currentClickTarget, clickPoint;
	AICharacterControl aICharacterControl;
	GameObject walkTarget = null;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
		aICharacterControl = GetComponent<AICharacterControl>();
		walkTarget = new GameObject ("walkTarget");
    }

	void ProcessMouseMovement ()
	{
		if (Input.GetMouseButton (0)) {
			//print ("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString ());
			//print("cameraRaycaster.layerHit = " + cameraRaycaster.layerHit);
			clickPoint = cameraRaycaster.hit.point;
			switch (cameraRaycaster.layerHit) {
			case Layer.Walkable:
				//currentClickTarget = cameraRaycaster.hit.point;
				//currentClickTarget = ShortDestination (clickPoint, walkMoveStopRadius);
				walkTarget.transform .position = clickPoint;
				aICharacterControl.SetTarget (walkTarget.transform);
				break;
			case Layer.Enemy:
				//currentClickTarget = ShortDestination (clickPoint, attackMoveStopRadius);
				GameObject enemy = cameraRaycaster.hit.collider.gameObject;
				aICharacterControl.SetTarget (enemy.transform);
				break;
			default:
				print ("Unexpected layer found");
				return;
			}
		}
		//WalkToDestination ();
	}

	bool isInDirectMode = false;
    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
		if (Input.GetKeyDown (KeyCode.G)) {
			isInDirectMode = !isInDirectMode;
			currentClickTarget = transform.position;
		}

		if (isInDirectMode) {
			ProcessDirectMovement();
		} else {
			ProcessMouseMovement ();			
		}

    }

	void ProcessDirectMovement(){
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

		m_Character.Move (m_Move, false, false);
	}

//	void WalkToDestination ()
//	{
//		var playerToClick = currentClickTarget - transform.position;
//		if (playerToClick.magnitude >= 0) {
//			m_Character.Move (playerToClick, false, false);
//		}
//		else {
//			m_Character.Move (Vector3.zero, false, false);
//		}
//	}

	Vector3 ShortDestination(Vector3 destination, float shortening){
		Vector3 reductionVector = (destination - transform.position).normalized * shortening;
		return destination - reductionVector;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.black;
		Gizmos.DrawLine (transform.position, currentClickTarget);
		Gizmos.DrawSphere (currentClickTarget, 0.1f);
		Gizmos.DrawSphere (clickPoint, 0.15f);

		//Draw attack sphere
		Gizmos.color = new Color(255f,0f,0,.5f);
		Gizmos.DrawWireSphere (transform.position, attackMoveStopRadius);
	}
}

