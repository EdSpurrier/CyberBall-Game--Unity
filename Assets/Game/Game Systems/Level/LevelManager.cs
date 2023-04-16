using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public List<LevelPiece> levelPieces;

    public void ResetFloors()
    {
        levelPieces.ForEach(levelPiece => {

            levelPiece.floor = null;

        });
    }

    public void Refresh()
    {
        levelPieces.Clear();

        LevelPiece[] levelPiecesFound = FindObjectsOfType(typeof(LevelPiece)) as LevelPiece[];

        Array.ForEach(levelPiecesFound, levelPiece => levelPieces.Add(levelPiece));


        levelPieces.ForEach(levelPiece => {

            if (!levelPiece.floor)
            {
                Transform[] allChildren = levelPiece.gameObject.GetComponentsInChildren<Transform>();

                foreach (Transform child in allChildren)
                {
                    if (child.parent == levelPiece.transform)
                    {
                        if (!levelPiece.walls.Contains(child) && !levelPiece.interactables.Contains(child))
                        {
                            levelPiece.floor = child;
                            continue;
                        };
                    };
                    
                };
            };

        });
    }

    public LevelPiece GetPieceAt(Vector3 position)
    {
        LevelPiece found = null;

        levelPieces.ForEach(lp => {
            if (0.1f > Vector3.Distance(lp.transform.position, position)) 
            {
                found = lp;
            };
        });

        return found;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
