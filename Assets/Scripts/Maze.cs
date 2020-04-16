using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public float generationStepDelay;
    public IntVector2 size;
    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;
    public MazeCell cellPrefab;

    private MazeCell[ , ] _cells;

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return _cells[coordinates.x, coordinates.z];
    }
    public IEnumerator Generate () 
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        _cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0) 
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    private void DoFirstGenerationStep (List<MazeCell> activeCells) 
    {
        activeCells.Add(CreateCells(RandomCoordinates));
    }

    private void DoNextGenerationStep (List<MazeCell> activeCells) 
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbour = GetCell(coordinates);
            if (neighbour == null)
            {
                neighbour = CreateCells(coordinates);
                CreatePassage(currentCell, neighbour, direction);
                activeCells.Add(neighbour);
            }
            else
            {
                CreateWall(currentCell, neighbour, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private MazeCell CreateCells (IntVector2 coordinates) 
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        _cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition =
            new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }
    
    private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null) {
            wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }
    
    private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }
    
    public IntVector2 RandomCoordinates => new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));

    public bool ContainsCoordinates(IntVector2 coordinate) =>
        coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
}