using System.Collections;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    bool isMoving = false; 

    void Start()
    {
        
    }
    void Update()
    {
        // FOR DEBUG
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move((int)transform.position.x +1, (int)transform.position.y, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move((int)transform.position.x - 1, (int)transform.position.y, 0.5f);
        }
    }

    public void SetCoord(int x, int y)        // Set the location of the gameobject on the board. 
    {
        xIndex = x;
        yIndex = y;
    }

    public void Move(int destX, int destY, float timeToMove)
    {
        if(!isMoving)
            StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), timeToMove));
    }

    IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = transform.position;
        bool reachedDestination = false;
        float elapsedTime = 0f;
        isMoving = true; 

        while (!reachedDestination)
        {
            if (Vector3.Distance(transform.position, destination) < 0.01f)      // If at destination
            {
                reachedDestination = true;
                transform.position = destination;
                SetCoord((int)destination.x, (int)destination.y);
                break;
            }

            // While not at destination
            elapsedTime += Time.deltaTime;          
            float t = elapsedTime / timeToMove;                                 // value of time (0 - 1) used to calc lerp
            transform.position = Vector3.Lerp(startPosition, destination, t);   // Moves towards destination as t grows

            yield return null;
        }
        isMoving = false;
    }
}
