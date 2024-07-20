using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;                           // Width of tile board. 1 width = 1 unit (or 1 tile)
    public int height;                          // Height of tile board. 
    public int borderSize;                      // The spacing to edge of camera. Calculated in units

    public GameObject TilePrefab;               // Will hold the object for one square on the board
    private Tile[,] allTiles;                   // The , tells C# that we want 2D-array. 

    void Start()
    {
        allTiles = new Tile[width, height];     // Set upp an empty array.
        SetupTiles();                           // Setup the tiles in the array. 
        SetupCamera();                          // Setup camera size depending on tilesize
    }

     void SetupTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {   
                // For every iteration, instantiate a tileprefab at location x,y. 
                GameObject tile = Instantiate(TilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                tile.name = "Tile (" + i + "," + j + ")";

                // Store the tile in the 2D array
                allTiles[i,j] = tile.GetComponent<Tile>();
                tile.transform.parent = transform; // Set the Board gameobject as parent. 
            }
        }
    }

    void SetupCamera()      // Adapt camera size to board size, keeping board centered and with spacing on the sides
    {
        Camera.main.transform.position = new Vector3((float)(width - 1) / 2f, (float)(height -1) / 2f, -10f);
        float aspectRatio = (float) Screen.width / Screen.height;
        float verticalSize = (float)height / 2f + (float)borderSize;
        float horizontalSize = ((float) width / 2f + (float)borderSize) / aspectRatio;

        if (verticalSize > horizontalSize)
            Camera.main.orthographicSize = verticalSize;
        else
            Camera.main.orthographicSize = horizontalSize;

    }
}
