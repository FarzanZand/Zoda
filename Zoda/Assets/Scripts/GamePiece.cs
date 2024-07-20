using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SetCoord(int _x, int _y)        // Set the location of the gameobject on the board. 
    {
        xIndex = _x;
        yIndex = _y;
    }
}
