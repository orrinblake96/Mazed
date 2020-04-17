using UnityEngine;

public class Player : MonoBehaviour
    {
    private MazeCell _currentCell;

    public void SetLocation (MazeCell cell) {
        _currentCell = cell;
        transform.localPosition = cell.transform.localPosition + Vector3.up/2;
    }
}
