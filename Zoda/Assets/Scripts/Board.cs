using UnityEngine;

// Holds the core logic for the game board. Added to the Board-Gameobject. 
public class Board : MonoBehaviour              
{
    public int width;                           // Width of tile board. 1 width = 1 unit (or 1 tile)
    public int height;                          // Height of tile board. 
    public int borderSize;                      // The spacing to edge of camera. Calculated in units

    public GameObject[] gamePiecePrefabs;       // Holds all prefabs of the game pieces. Set size and add in inspector. 
    public GameObject TilePrefab;               // Will hold the object for one square on the board

    private Tile[,] allTiles;                   // The , tells C# that we want 2D-array. 
    private GamePiece[,] allGamePieces;         // Location of game pieces on board. 

    void Start()
    {
        allTiles = new Tile[width, height];             // Set upp an empty array.
        allGamePieces = new GamePiece[width, height];   // Sets size of array as size of board. 

        SetupTiles();                           // Setup the tiles in the array. 
        SetupCamera();                          // Setup camera size depending on tilesize
        FillRandom();                           // Randomly fill board at start
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
                allTiles[i, j] = tile.GetComponent<Tile>();

                tile.transform.parent = transform; // Set the Board gameobject as parent. 
                allTiles[i, j].Init(i, j, this);   // Set the data in the Tile.cs script for the created tile prefab
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

    GameObject GetRandomGamePiece()             
    {
        int randomIndex = Random.Range(0, gamePiecePrefabs.Length);

        if (gamePiecePrefabs[randomIndex] == null)
            Debug.LogWarning("BOARD " + randomIndex + " does not contain a gamepiece-prefab.");

        return gamePiecePrefabs[randomIndex];   // Return the prefab piece at the array location.
    }

    void PlaceGamePiece(GamePiece _gamePiece, int _x, int _y)
    {
        if (_gamePiece == null)
        {
            Debug.LogWarning("BOARD: Invalid GamePiece.");
            return;
        }

        _gamePiece.transform.position = new Vector3(_x, _y, 0);
        _gamePiece.transform.rotation = Quaternion.identity;
        _gamePiece.SetCoord(_x, _y);            // sets the gamepiece at defined coords. 
    }

    void FillRandom()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject randomPiece = Instantiate(GetRandomGamePiece(), Vector3.zero, Quaternion.identity) as GameObject;

                if (randomPiece != null)    // Get the script component of the gameobject, and place at the location. 
                    PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j); 

            }
        }
    }
}
