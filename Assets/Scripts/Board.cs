using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Board : MonoBehaviour
{

    public PlacementZone[] row1, row2, row3;
    public PlacementZone[] column1, column2, column3;

    public PlacementZone[] diag1, diag2;

    public string winner = "";


    PlacementZone[] zones;

    private void Awake()
    {
        zones = FindObjectsOfType<PlacementZone>();
    }

    void Start()
    {
        print("HELLO");
    }



    public void AnalyzeBoardState()
    {
        AnalyzeZones(row1, "row1");
        AnalyzeZones(row2, "row2");
        AnalyzeZones(row3, "row3");
        AnalyzeZones(column1, "column1");
        AnalyzeZones(column2, "column2");
        AnalyzeZones(column3, "column3");
        AnalyzeZones(diag1, "diag1");
        AnalyzeZones(diag2, "diag2");

        //after checking zones, check if all squares are full for draw
        if(winner == "" && IsBoardFull()) {
            winner = "DRAW";
        }
    }

    public bool IsBoardFull(){
        //if any spot does not have a piece, we will return false
        foreach(PlacementZone spot in zones)
        {
            if(spot.piece == string.Empty) return false;
        }

        return true;
    }

    public void TogglePlacement(bool isAllowed)
    {
        foreach(PlacementZone z in zones)
        {
            z.isPlacementAllowed = isAllowed;
        }
    }


    private void AnalyzeZones(PlacementZone[] zones, string zone_name)
    {
        if(winner != "") return; // we have found a winner

        int xScore = 0;
        int oScore = 0;
        foreach(PlacementZone z in zones)
        {   
            if(z.piece == "x") xScore++;
            if(z.piece == "o") oScore++;
        }

        if(xScore == 3) {
            winner = "X WINS";
        }
        else if(oScore == 3){
            winner = "O WINS";
        }
    
        print($"checking results xScore:{xScore}   oscore:{oScore}  winner:{winner} from zone: {zone_name}");
    }

    public string GetResults(){
        return winner;
    }

    public PlacementZone GetRandomOpenPlacement()
    {
        List<PlacementZone> openZones = new List<PlacementZone>();
        foreach(PlacementZone z in zones)
        {
            if(z.piece == ""){
                openZones.Add(z);
            }
        }

        int spots = openZones.Count;
        int pick = UnityEngine.Random.Range(0, spots);
        print("will return: " + pick);

        return openZones[pick];

    }

}
