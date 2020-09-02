using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Vector2Int _boardSize;

    [SerializeField] private GameBoard gameBoard;


    private void Awake()
    {
        gameBoard.Initialize(_boardSize);
        gameBoard.InitializeTiles();
        gameBoard.FindPaths();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        if (_boardSize.x < 2)
            _boardSize.x = 2;

        if (_boardSize.y < 2)
            _boardSize.y = 2;


    }
}
