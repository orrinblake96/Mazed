﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public float generationStepDelay;
    public IntVector2 size;
    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;
    public MazeCell cellPrefab;
    public MazeDoor doorPrefab;
    public MazeRoomSettings[] roomSettings;

    [Range(0f, 1f)] public float doorProbability;

    private MazeCell[ , ] _cells;
    private List<MazeRoom> _rooms = new List<MazeRoom>();

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return _cells[coordinates.x, coordinates.z];
    }
    public void Generate () 
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        _cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0) 
        {
//            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    private void DoFirstGenerationStep (List<MazeCell> activeCells) 
    {
        MazeCell newCell = CreateCells(RandomCoordinates);
        newCell.Initialize(CreateRoom(-1));
        activeCells.Add(newCell);
    }
    
    private void CreatePassageInSameRoom (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        if (cell.room != otherCell.room) {
            MazeRoom roomToAssimilate = otherCell.room;
            cell.room.Assimilate(roomToAssimilate);
            _rooms.Remove(roomToAssimilate);
            Destroy(roomToAssimilate);
        }
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
            else if (currentCell.room.settingsIndex == neighbour.room.settingsIndex) 
            {
                CreatePassageInSameRoom(currentCell, neighbour, direction);
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
            new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0.5f, coordinates.z - size.z * 0.5f + 0.5f);
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
    
    private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as MazePassage;
        if (passage is MazeDoor) {
            otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
        }
        else {
            otherCell.Initialize(cell.room);
        }
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }
    
    private MazeRoom CreateRoom (int indexToExclude) {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = Random.Range(0, roomSettings.Length);
        if (newRoom.settingsIndex == indexToExclude) {
            newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
        }
        newRoom.settings = roomSettings[newRoom.settingsIndex];
        _rooms.Add(newRoom);
        return newRoom;
    }
    
    public IntVector2 RandomCoordinates => new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));

    public bool ContainsCoordinates(IntVector2 coordinate) =>
        coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
}