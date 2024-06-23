using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    public bool isSinglePlayer = false;
    public bool isComputersTurn = false;
    public bool isXsTurn = true;

    public bool isWaiting = false;
    public bool isGameOver = false;


    GameMessages gameMessages;
    Board gameBoard;
    MenuCanvas menuCanvas;


    private void Awake()
    {
        instance = this;
        gameMessages = FindObjectOfType<GameMessages>();
        gameBoard = FindObjectOfType<Board>();
        menuCanvas = FindObjectOfType<MenuCanvas>();
        
    }


    public void StartGameSession(){
        StartCoroutine(GameSessionSequence());
    }


    IEnumerator GameSessionSequence()
    {
        isGameOver = false;
        gameBoard.TogglePlacement(false);

        if(isSinglePlayer){
            gameMessages.Say("Single Player Game");
        }
        else {
            gameMessages.Say("Multiplayer Game");
        }
        yield return new WaitForSeconds(2f);

        if(isSinglePlayer){
            //determine if player or computer goes first
            float random = UnityEngine.Random.value;
            if(random > 0.5f)
            {
                 gameMessages.Say("Computer wins flip and goes first.");
                 isXsTurn = false;
                 isComputersTurn = true;
            }
            else {
                gameMessages.Say("Player wins flip and goes first.");
                isComputersTurn = false;
            }
            yield return new WaitForSeconds(2f);
        }
        

        while (!isGameOver)
        {
            isWaiting = true;
            yield return new WaitForSeconds(1f);

            //let players place.
            if(isXsTurn){
                gameMessages.Say("X's turn.");    
            }
            else {
                 gameMessages.Say("O's turn.");
            }

            if(isSinglePlayer && isComputersTurn)
            {
                print("computer's turn");
                LazyGameAI();
            }
            else {
                //anable piece placement for player
                gameBoard.TogglePlacement(true);
            }
            
            //wait for them to place
            while (isWaiting){
                yield return new WaitForSeconds(0.1f);
            }

            //after placement, disable placement until we're ready
            gameBoard.TogglePlacement(false);

            //check board state after placement
            gameBoard.AnalyzeBoardState();

            //see results and check win condition
            string result = gameBoard.GetResults();
            if(result != ""){
                isGameOver = true;
                gameMessages.ClearQueue();
                gameMessages.Say("Game Ended!");    
                yield return new WaitForSeconds(0.1f);
                menuCanvas.ShowGameOverScreen($"{result}");
            }
        }
    }


    public void TurnOver()
    {
        isWaiting = false;
        isXsTurn = !isXsTurn;
        isComputersTurn = !isComputersTurn;
    }


    public void LazyGameAI()
    {
        PlacementZone openSpace = gameBoard.GetRandomOpenPlacement();
        openSpace.ComputerPlacePiece();
    }

}
