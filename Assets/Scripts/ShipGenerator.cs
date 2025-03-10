using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyShips;
    [SerializeField] private float _xOffset = 2f;
    [SerializeField] private float _yOffset = 2f;

    private Vector3 _startSpawnPoint;
    private int _rows = 5;
    private int _columns = 11;
    
    private List<GameObject> _instances;

    private void OnEnable()
    {
        _startSpawnPoint = transform.position;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                if (i < 2)
                {
                    Instantiate(_enemyShips[0], new Vector3(_startSpawnPoint.x + _xOffset * j,
                                                            _startSpawnPoint.y - _yOffset * i, 0f), _enemyShips[0].transform.rotation);
                }
                else
                {
                    Instantiate(_enemyShips[1], new Vector3(_startSpawnPoint.x + _xOffset * j,
                                                            _startSpawnPoint.y - _yOffset * i, 0f), _enemyShips[1].transform.rotation);
                }
            }
        }
    }
}
