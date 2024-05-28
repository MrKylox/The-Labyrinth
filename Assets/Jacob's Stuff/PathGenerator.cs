using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathGenerator
{
    private int mazeSize;
    private int[,] mazeArray;
    public List<Vector2> mazePath = new List<Vector2>();


    public PathGenerator(int mazeSize, int[,] mazeArray)
    {
        this.mazeSize = mazeSize;
        this.mazeArray = mazeArray;

    }

    public int[,] traverse(int StartX, int StartY)
    {
        mazePath = new List<Vector2>();
        

        int currentX = StartX;
        int currentY = StartY;

        mazePath.Add(new Vector2(StartX, StartY));

        //               1 = top middle, 
        // 2 =     left,                 3 =     right
        //               4 = bot middle

        int init_direction = 0;
        int direction = 0;

        // Starting direction of the path based on the initial starting position
        // Traverse Up
        if (currentY == 0 && (currentX > 0 && currentX < this.mazeSize))
        {
            init_direction = 1;
        }
        // Traverse Down 
        else if (currentY == this.mazeSize - 1 && (currentX > 0 && currentX < this.mazeSize))
        {
            init_direction = 4;
        }
        // Traverse Right
        else if (currentX == 0 && (currentY > 0 && currentY < this.mazeSize))
        {
            init_direction = 3;
        }
        // Traverse Left
        else if (currentX == this.mazeSize - 1 && (currentY > 0 && currentY < this.mazeSize))
        {
            init_direction = 2;
        }

        // Loop Through each element of array till we reach the end of path
        for (int i = 0; i < this.mazeSize * 5; i++)
        {

            // Calculate the random traverse direction based on the initial direction
            if (init_direction == 1)
            {
                direction = Random.Range(1, 3 + 1);

            }
            else if (init_direction == 4)
            {
                direction = Random.Range(2, 4 + 1);

            }
            else if (init_direction == 3)
            {
                direction = Random.Range(1, 4 + 1);
                if (direction == 2)
                {
                    direction = 3;
                }
            }
            else if (init_direction == 2)
            {
                direction = Random.Range(1, 4 + 1);
                if (direction == 3)
                {
                    direction = 2;
                }
            }

            // Change value of current x and y array indexing
            switch (direction)
            {
                case 1:
                    // 1 = top
                    currentY += 1;
                    break;

                case 2:
                    // 2 = Left
                    currentX -= 1;
                    break;

                case 3:
                    // 3 = Right
                    currentX += 1;
                    break;

                case 4:
                    // 4 = Bottom
                    currentY -= 1;
                    break;
            }

            // Exit the loop if we reach the end.
            if (currentX > this.mazeSize - 1 || currentX < 0)
            {
                break;
            }

            if (currentY > this.mazeSize - 1 || currentY < 0)
            {
                break;
            }

            // Modify the array.
            this.mazeArray[currentX, currentY] = 0;

            mazePath.Add(new Vector2(currentX, currentY));
        }

        return this.mazeArray;
    }


    

    


}
