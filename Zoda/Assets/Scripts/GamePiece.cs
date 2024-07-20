using System.Collections;
using UnityEngine;

//TODO: Move the InterpolationType to a master-script. Otherwise, you need to change all prefabs manually. 

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    bool isMoving = false;

    // Select type of movement on the game pieces in piece prefab object inspector. 
    public InterpolationType interpolation = InterpolationType.SmootherStep; 

    public enum InterpolationType       //TODO NOT YET INTEGRATED
    {
        Linear,
        EaseOut,
        EaseIn,
        SmoothStep,
        SmootherStep
    };

    void Start()
    {

    }
    void Update()
    {
        // FOR DEBUG
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move((int)transform.position.x + 1, (int)transform.position.y, 0.5f);
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
        if (!isMoving)
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
                transform.position = destination;                               // Round out pos to final destination
                SetCoord((int)destination.x, (int)destination.y);               // Set cords of game piece
                break;
            }

            // While not at destination
            elapsedTime += Time.deltaTime;                                      // Track the total running time        
            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);            // Linear: value of time (0 - 1) used to calc lerp

            // These are different ways to calculate lerp time on piece. You pick one in inspector.
            switch (interpolation)
            {
                case InterpolationType.Linear:     
                    break;
                case InterpolationType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
                    break;
                case InterpolationType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
                    break;
                case InterpolationType.SmoothStep:
                    t = t * t * (3 - 2 * t);
                    break;
                case InterpolationType.SmootherStep:
                    t = t * t * t * (t * (t * 6 - 15) + 10);
                    break;
            }

            transform.position = Vector3.Lerp(startPosition, destination, t);   // Moves towards destination as t grows
            yield return null;                                                  // Wait until next frame
        }
        isMoving = false;
    }
}
