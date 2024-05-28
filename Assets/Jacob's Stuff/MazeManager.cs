using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;


public class MazeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int mazeSize = 100;
    public GameObject groundObject;
    public Vector3 mazeCentralPosition = Vector3.zero;
    public int[,] mazeArray;
    public GameObject boxPrefab;
    public int carvingIterations = 10;
    private PathGenerator pathGenerator;
    private Vector3 wallDiamentions;
    private Vector3 mazeStartingPos;
    

    void Awake()
    {

        mazeGeneratorv1();

    }

    private Vector3 getCentralPositionOfCell(Vector2 thisCell)
    {

        if (thisCell.x > mazeSize-1 || thisCell.y > mazeSize - 1)
        {
            return Vector3.zero;
        }

        Vector3 position = Vector3.zero;
        int offsetUnits = 0;

        if (mazeSize % 2  == 0)
        {
            offsetUnits = (mazeSize / 2) - 1;

        } else
        {
            offsetUnits = (mazeSize - 1) / 2;
        }

        position.x = mazeStartingPos.x + (thisCell.x * wallDiamentions.z) + (wallDiamentions.z/2);
        position.z = mazeStartingPos.z + (thisCell.y * wallDiamentions.z);
        position.y = groundObject.transform.position.y + wallDiamentions.y;

        return position;
    }



    private void drawWalls()
    {

        float mazeLength = mazeSize * wallDiamentions.z;

        mazeStartingPos = new Vector3(mazeCentralPosition.x - (mazeLength / 2) , groundObject.transform.position.y , mazeCentralPosition.z - (mazeLength / 2) + (wallDiamentions.z / 2));
        Debug.Log("Starting Pos = " + mazeStartingPos);

        // Spawn a row of wall
        for (int j = 0; j < mazeSize + 1; j++)
        {
            for (int i = 0; i < mazeSize; i++)
            {
                Instantiate(boxPrefab, new Vector3( wallDiamentions.z * j, wallDiamentions.y / 2, wallDiamentions.z * i) + mazeStartingPos, Quaternion.identity);
            }
        }

        for (int j = 0; j < mazeSize + 1; j++)
        {
            for (int i = 0; i < mazeSize; i++)
            {
                Instantiate(boxPrefab, new Vector3((wallDiamentions.z / 2) + (i * wallDiamentions.z), wallDiamentions.y / 2, (wallDiamentions.z * j) - (wallDiamentions.z / 2)) + mazeStartingPos, Quaternion.identity * Quaternion.Euler(0, 90, 0));
            }
        }

        
    }

    private void carveWall()
    {
        // Delete walls
        for (int i = 0; i < pathGenerator.mazePath.Count - 1; i++)
        {
            Debug.Log("Checking Wall at " + getCentralPositionOfCell(pathGenerator.mazePath[i]) + " , " + getCentralPositionOfCell(pathGenerator.mazePath[i + 1]));
            wallDetectionRaycast(getCentralPositionOfCell(pathGenerator.mazePath[i]), getCentralPositionOfCell(pathGenerator.mazePath[i + 1]));
        }
    }

    private void wallDetectionRaycast(Vector3 positionA, Vector3 positionB)
    {

        RaycastHit hit;
        Vector3 dir = (positionB - positionA).normalized;

        if (Physics.Linecast(positionA, positionB, out hit))
        { 
            Destroy(hit.transform.gameObject);
        }

    }




    private void mazeGeneratorv1()
    {
        wallDiamentions = boxPrefab.transform.localScale;

        

        // Generate 2D array of all 1s.
        generateMaze();
        drawWalls();

        pathGenerator = new PathGenerator(mazeSize, mazeArray);

        Debug.Log("Cell " + new Vector2(mazeSize / 2, 0) + " at " + getCentralPositionOfCell(new Vector2(mazeSize / 2, 0 )));

        // Starting Cell
        mazeArray = pathGenerator.traverse(mazeSize/ 2, 0);
        carveWall();

        // Ending Cell
        mazeArray = pathGenerator.traverse(mazeSize/ 2, mazeSize - 1);
        carveWall();

        for (int i  = 0; i < carvingIterations; i++) {

            mazeArray = pathGenerator.traverse(i * (mazeSize / (carvingIterations + 1)), 0);
            carveWall();
        }

        for (int i = 0; i < carvingIterations; i++)
        {
            mazeArray = pathGenerator.traverse(0, i * (mazeSize / (carvingIterations + 1)));
            carveWall();
        }
    }


    private void generateMaze()
    {
    
        mazeArray = new int[mazeSize, mazeSize];
        for (int i = 0; i < mazeSize; i++)
        {
            for (int j = 0; j < mazeSize; j++)
            {
                mazeArray[i,j] = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
