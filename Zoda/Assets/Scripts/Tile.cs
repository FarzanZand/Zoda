using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    private Board board; 


    void Start()
    {
        
    }

    public void Init( int _x, int _y, Board _board)
    {
        xIndex = _x;
        yIndex = _y;
        board = _board;
    }
}
