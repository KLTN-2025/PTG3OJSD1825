using System.Collections;
using UnityEngine;

public class ElevatorBox : MonoBehaviour
{
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public Transform startFloor; 
    private bool isMoving = false;
    private Vector3 initialPosition; 

    private void Start()
    {
        initialPosition = startFloor.position; 
        StartCoroutine(DoorControlLoop()); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            Vector3 endPosition = new Vector3(startFloor.position.x, startFloor.position.y + 4, startFloor.position.z);
            StartCoroutine(MoveElevator(endPosition)); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveElevator(initialPosition));
        }
    }

    IEnumerator MoveElevator(Vector3 targetPosition)
    {
        isMoving = true;
        float elapsedTime = 0;
        float journeyTime = 5f; 
        Vector3 startPosition = transform.position;

        while (elapsedTime < journeyTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / journeyTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    IEnumerator DoorControlLoop()
    {
        while (true)
        {
            while (!isMoving)
            {
                yield return new WaitForSeconds(5);
                StartCoroutine(OpenDoors());
                yield return new WaitForSeconds(5);
                StartCoroutine(CloseDoors());
            }
            yield return null; 
        }
    }

    IEnumerator OpenDoors()
    {
        if (isMoving) yield break;

        Vector3 leftDoorTarget = LeftDoor.transform.position + new Vector3(0, 0, -1.0f); 
        Vector3 rightDoorTarget = RightDoor.transform.position + new Vector3(0, 0, 1.0f); 
        float elapsedTime = 0;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            LeftDoor.transform.position = Vector3.Lerp(LeftDoor.transform.position, leftDoorTarget, elapsedTime / duration);
            RightDoor.transform.position = Vector3.Lerp(RightDoor.transform.position, rightDoorTarget, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CloseDoors()
    {
        if (isMoving) yield break;

        Vector3 leftDoorStart = LeftDoor.transform.position + new Vector3(0, 0, 1.0f); 
        Vector3 rightDoorStart = RightDoor.transform.position + new Vector3(0, 0, -1.0f); 
        float elapsedTime = 0;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            LeftDoor.transform.position = Vector3.Lerp(LeftDoor.transform.position, leftDoorStart, elapsedTime / duration);
            RightDoor.transform.position = Vector3.Lerp(RightDoor.transform.position, rightDoorStart, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}