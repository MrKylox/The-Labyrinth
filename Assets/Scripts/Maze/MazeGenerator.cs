using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    public PowerUpSpawner _powerUpSpawner;

    [SerializeField]
    public EnemyManager _enemyManager;

    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    [SerializeField]
    private float _cellScale = 1.0f; //Setting the scale

    private MazeCell[,] _mazeGrid;



    public float CellScale // Property to access and modify cell scale
    {
        get {  return _cellScale; } 
        set 
        { 
            _cellScale = value;
            // If scale changes, regenerate the maze with new scale
            RegenerateMaze();
        }
    }
    

    void Start()
    {
        RegenerateMaze(); // Generate maze initially   
        _enemyManager.SpawnEnemies();
        _powerUpSpawner.SpawnPowerUps();
    }

    private void RegenerateMaze()
    {
        if (_mazeGrid != null)
        {
            foreach (var cell in _mazeGrid)
            {
                Destroy(cell.gameObject); // Destroy existing maze cells
            }
        }

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                Vector3 cellPosition = new Vector3(x * _cellScale, 0, z * _cellScale); // Adjust position based on scale
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, cellPosition, Quaternion.identity);
                _mazeGrid[x, z].transform.localScale = new Vector3(_cellScale, _cellScale, _cellScale); // Set the scale
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = Mathf.RoundToInt(currentCell.transform.position.x / _cellScale); // Round to nearest index
        int z = Mathf.RoundToInt(currentCell.transform.position.z / _cellScale); // Round to nearest index

        // Check if x and z are within array bounds
        if (x >= 0 && x < _mazeWidth && z >= 0 && z < _mazeDepth)
        {
            if (x + 1 < _mazeWidth)
            {
                var cellToRight = _mazeGrid[x + 1, z];

                if (cellToRight.IsVisited == false)
                {
                    yield return cellToRight;
                }
            }

            if (x - 1 >= 0)
            {
                var cellToLeft = _mazeGrid[x - 1, z];

                if (cellToLeft.IsVisited == false)
                {
                    yield return cellToLeft;
                }
            }

            if (z + 1 < _mazeDepth)
            {
                var cellToFront = _mazeGrid[x, z + 1];

                if (cellToFront.IsVisited == false)
                {
                    yield return cellToFront;
                }
            }

            if (z - 1 >= 0)
            {
                var cellToBack = _mazeGrid[x, z - 1];

                if (cellToBack.IsVisited == false)
                {
                    yield return cellToBack;
                }
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

}
