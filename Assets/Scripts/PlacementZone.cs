using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JetBrains.Annotations;

public class PlacementZone : MonoBehaviour
{

    public GameObject xPiece, oPiece;

    public bool isPlacementAllowed = false;
    public string piece = string.Empty;


    void Awake(){
        isPlacementAllowed = false;
        piece = string.Empty;
    }

    private void OnMouseDown()
    {
        PlacePiece();
    }

    public void PlacePiece()
    {
        if(!isPlacementAllowed || piece != "") return;

        GameObject objToPlace = xPiece;
        piece = "x";
        if(!GameController.instance.isXsTurn)
        {
            objToPlace = oPiece;
            piece = "o";
        }

        // print($"Placing piece:{piece}");
        Instantiate(objToPlace, this.transform.position, objToPlace.transform.rotation);

        GameController.instance.TurnOver();
    }


    public void ComputerPlacePiece()
    {
        // This one is used specifically from the computer player in singleplayer mode.
        // it places the piece, but is not restricted by the isPlacementAllowed, which is
        // set to false while the computer makes its move.

        GameObject objToPlace = xPiece;
        piece = "x";
        if(!GameController.instance.isXsTurn)
        {
            objToPlace = oPiece;
            piece = "o";
        }

        Instantiate(objToPlace, this.transform.position, objToPlace.transform.rotation);
        GameController.instance.TurnOver();
    }


}
