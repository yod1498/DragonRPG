using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float walkMoveStopRadius = 0.2f;
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
	Vector3 currentClickTarget;


    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

	bool isInDirectMode = false;
    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
		if (Input.GetKeyDown (KeyCode.G)) {
			isInDirectMode = !isInDirectMode;
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

	void ProcessMouseMovement ()
	{
		if (Input.GetMouseButton (0)) {
			print ("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString ());
			switch (cameraRaycaster.layerHit) {
			case Layer.Walkable:
				currentClickTarget = cameraRaycaster.hit.point;
				break;
			case Layer.Enemy:
				print ("Not moving to enemy");
				break;
			default:
				print ("Unexpected layer found");
				return;
			}
		}
		var playerToClick = currentClickTarget - transform.position;
		if (playerToClick.magnitude >= walkMoveStopRadius) {
			m_Character.Move (playerToClick, false, false);
		}
		else {
			m_Character.Move (Vector3.zero, false, false);
		}
	}
}

