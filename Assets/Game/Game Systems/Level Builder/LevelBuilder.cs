#if (UNITY_EDITOR)

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;



public class LevelBuilder : MonoBehaviour
{
    [BoxGroup("Placement")]


    [HorizontalGroup("Placement/Row-A", (2f / 3f))]
    [Button("Place (+)")]
    public void PlacePiece()
    {
        PlaceBase();

        UpdateBuilder();
    }


    [HorizontalGroup("Placement/Row-A", (1f / 3f))]
    [Button("Delete (-)")]
    public void RemovePieceButton()
    {
        RemovePiece();
        UpdateBuilder();
    }


    [HorizontalGroup("Placement/Row-C", (1f / 3f))]
    [Button("Copy")]
    public void CopyPiece()
    {
        if(currentPiece)
        {
            clipboard = currentPiece;
        };

        UpdateBuilder();
    }

    [HorizontalGroup("Placement/Row-C", (1f / 3f))]
    [Button("Paste")]
    public void Paste()
    {
        PastePiece();

        UpdateBuilder();
    }

    [HorizontalGroup("Placement/Row-C", (1f / 3f))]
    [Button("Toggle Floor")]
    public void ToggleFloorButton()
    {
        ToggleFloor();

        UpdateBuilder();
    }



    bool paintActive = false;

    [HorizontalGroup("Placement/Row-L", (1f / 2f))]
    [ShowIf("@this.paintActive == false")]
    [Button("Paint")]
    public void ActivatePaint()
    {
        paintActive = true;
        eraserActive = false;

        if (!clipboard)
        {
            clipboard = currentPiece;
        };

        UpdateBuilder();
    }

    [HorizontalGroup("Placement/Row-L", (1f / 2f))]
    [ShowIf("@this.paintActive == true")]
    [Button("Painting")]
    public void DeactivatePaint()
    {
        paintActive = false;
        UpdateBuilder();
    }



    bool eraserActive = false;

    [HorizontalGroup("Placement/Row-G", (1f / 2f))]
    [ShowIf("@this.eraserActive == false")]
    [Button("Eraser")]
    public void ActivateEraser()
    {
        eraserActive = true;
        paintActive = false;

        UpdateBuilder();
    }

    [HorizontalGroup("Placement/Row-G", (1f / 2f))]
    [ShowIf("@this.eraserActive == true")]
    [Button("Erasering")]
    public void DeactivateEraser()
    {
        eraserActive = false;
        UpdateBuilder();
    }




    [HorizontalGroup("Placement/Row-B", (1f / 3f))]
    [Button("Show/Hide")]
    public void ShowHideMarker()
    {
        builderMarker.gameObject.SetActive(!builderMarker.gameObject.activeSelf);
        UpdateBuilder();
    }

    [HorizontalGroup("Placement/Row-B", (1f / 3f))]
    [Button("Randomize")]
    public void Null_A()
    {
        RandomizePiece();

        UpdateBuilder();
    }

    /*[HorizontalGroup("Placement/Row-B", (1f / 3f))]
    [Button("Reset Floors")]
    public void ResetFloors()
    {
        levelManager.ResetFloors();
    }*/


    void MoveUpdate()
    {
        UpdateBuilder();

        if (paintActive)
        {
            PastePiece();
        }
        else if (eraserActive)
        {
            RemovePiece();
        };

        UpdateBuilder();
    }



    [BoxGroup("Controls")]


    

    [HorizontalGroup("Controls/Row-1", (1f / 3f))]
    [Button("Rotate")]
    public void RotateNegative()
    {
        RotateBase(new Vector3(0, -90, 0));
        UpdateBuilder();
    }

    [HorizontalGroup("Controls/Row-1", (1f / 3f))]
    [Button("Up")]
    public void MoveUp()
    {
        currentPosition.z += movementAmount;

        MoveUpdate();
    }

    [HorizontalGroup("Controls/Row-1", (1f / 3f))]
    [Button("Rotate")]
    public void RotatePositive()
    {
        RotateBase(new Vector3(0, +90, 0));
        UpdateBuilder();
    }




    [HorizontalGroup("Controls/Row-2", (1f/3f))]
    [Button("Left")]
    public void MoveLeft()
    {
        currentPosition.x -= movementAmount;
        MoveUpdate();
    }

    [HorizontalGroup("Controls/Row-2", (1f / 3f))]
    [Button("Zero")]
    public void MoveZero()
    {
        currentPosition = Vector3.zero;
        UpdateBuilder();
    }

    [HorizontalGroup("Controls/Row-2", (1f / 3f))]
    [Button("Right")]
    public void MoveRight()
    {
        currentPosition.x += movementAmount;
        MoveUpdate();
    }






    [HorizontalGroup("Controls/Row-3", (1f / 3f))]
    [Button(" ")]
    public void Null_3()
    {
        UpdateBuilder();
    }

    [HorizontalGroup("Controls/Row-3", (1f / 3f))]
    [Button("Down")]
    public void MoveDown()
    {
        currentPosition.z -= movementAmount;
        MoveUpdate();
    }

    [HorizontalGroup("Controls/Row-3", (1f / 3f))]
    [Button(" ")]
    public void Null_4()
    {
        UpdateBuilder();
    }









    [BoxGroup("Walls")]

    [BoxGroup("Walls/Placement")]

    [HorizontalGroup("Walls/Placement/Row-A", (1f / 3f))]
    [Button("Rotate (-)")]
    public void RotateWallLeft()
    {
        RotateWall(new Vector3(0, -90, 0));

        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Placement/Row-A", (1f / 3f))]
    [Button("Clear")]
    public void ClearWalls()
    {
        RemoveWalls();

        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Placement/Row-A", (1f / 3f))]
    [Button("Rotate (+)")]
    public void RotateWallRight()
    {
        RotateWall(new Vector3(0, 90, 0));

        UpdateBuilder();
    }

    [BoxGroup("Walls/Place Type")]


    [HorizontalGroup("Walls/Place Type/Row-A", (1f / 4f))]
    [Button("Single Wall")]
    public void SingleWall()
    {
        AddWallType("Single Wall");
        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Place Type/Row-A", (1f / 4f))]
    [Button("Hallway")]
    public void DoubleWall()
    {
        AddWallType("Hallway");
        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Place Type/Row-A", (1f / 4f))]
    [Button("Corner")]
    public void CornerWall()
    {
        AddWallType("Corner");
        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Place Type/Row-X", (1f / 4f))]
    [Button("U")]
    public void UWall()
    {
        AddWallType("U");
        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Place Type/Row-X", (1f / 4f))]
    [Button("T")]
    public void TWall()
    {
        AddWallType("T");
        UpdateBuilder();
    }



    [HorizontalGroup("Walls/Place Type/Row-Z", (1f / 2f))]
    [Button("Single w Edge Oppsite")]
    public void SingleEdgeOppWall()
    {
        AddWallType("Single w Edge Oppsite");
        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Place Type/Row-Z", (1f / 2f))]
    [Button("Single w Edge")]
    public void SingleEdgeWall()
    {
        AddWallType("Single w Edge");
        UpdateBuilder();
    }


    [HorizontalGroup("Walls/Place Type/Row-T", (1f / 2f))]
    [Button("Corner w Edge")]
    public void CornerEdgeWall()
    {
        AddWallType("Corner With Edge");
        UpdateBuilder();
    }



    [HorizontalGroup("Walls/Place Type/Row-B", (1f / 2f))]
    [Button("Edge (x1)")]
    public void EdgeWall()
    {
        AddWallType("Edge (x1)");
        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Place Type/Row-B", (1f / 2f))]
    [Button("Edge (x2) Opposite")]
    public void EdgeWallOpp2()
    {
        AddWallType("Edge (x2) Opposite");
        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Place Type/Row-C", (1f / 3f))]
    [Button("Edge (x2)")]
    public void EdgeWall2()
    {
        AddWallType("Edge (x2)");
        UpdateBuilder();
    }

    

    [HorizontalGroup("Walls/Place Type/Row-C", (1f / 3f))]
    [Button("Edge (x3)")]
    public void EdgeWall3()
    {
        AddWallType("Edge (x3)");
        UpdateBuilder();
    }

    [HorizontalGroup("Walls/Place Type/Row-C", (1f / 3f))]
    [Button("Edge (x4)")]
    public void EdgeWall4()
    {
        AddWallType("Edge (x4)");
        UpdateBuilder();
    }


    [PropertySpace(20)]


    [BoxGroup("Interactables")]

    [BoxGroup("Interactables/Actions")]

    

    [HorizontalGroup("Interactables/Actions/Row-X", (1f / 3f))]
    [Button("Delete (-)")]
    public void DeleteInteractable ()
    {
        RemoveInteractable();

        UpdateBuilder();
    }


    [HorizontalGroup("Interactables/Row-Z", (1f / 3f))]
    [Button("Rotate (-)")]
    public void RotateInt_Left()
    {
        RotateInteractable(new Vector3(0, 90f, 0));

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Row-Z", (1f / 3f))]
    [Button(" ")]
    public void Int_Null_1()
    {

    }

    [HorizontalGroup("Interactables/Row-Z", (1f / 3f))]
    [Button("Rotate (+)")]
    public void RotateInt_Right()
    {
        RotateInteractable(new Vector3(0, 90f, 0));

        UpdateBuilder();
    }


    [BoxGroup("Interactables/Fans")]

    [HorizontalGroup("Interactables/Fans/Row-A", (1f / 3f))]
    [Button("Fan (Dual Direction)")]
    public void Fan()
    {
        AddInteractableType("Fan (Dual)");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Fans/Row-A", (1f / 3f))]
    [Button("Fan (Single Direction)")]
    public void FanSingle()
    {
        AddInteractableType("Fan (Single)");

        UpdateBuilder();
    }

    [BoxGroup("Interactables/Diodes")]

    [HorizontalGroup("Interactables/Diodes/Row-A", (1f / 3f))]
    [Button("Diodes (x2)")]
    public void Diodes2()
    {
        AddInteractableType("Diodes (x2)");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Diodes/Row-A", (1f / 3f))]
    [Button("Diodes (x4)")]
    public void Diodes4()
    {
        AddInteractableType("Diodes (x4)");

        UpdateBuilder();
    }


    [BoxGroup("Interactables/Other")]

    [HorizontalGroup("Interactables/Other/Row-A", (1f / 3f))]
    [Button("Battery")]
    public void Battery()
    {
        AddInteractableType("Battery");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Other/Row-A", (1f / 3f))]
    [Button("CPU")]
    public void CPU()
    {
        AddInteractableType("CPU");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Other/Row-A", (1f / 3f))]
    [Button("Computer")]
    public void Computer()
    {
        AddInteractableType("Computer");

        UpdateBuilder();
    }


    [BoxGroup("Interactables/Sunken Holes")]

    [HorizontalGroup("Interactables/Sunken Holes/Row-A", (1f / 3f))]
    [Button("Sunken Hole Computer")]
    public void SunkenHoleComputer()
    {
        AddInteractableType("Sunken Hole Computer");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Sunken Holes/Row-A", (1f / 3f))]
    [Button("Sunken Hole")]
    public void SunkenHole()
    {
        AddInteractableType("Sunken Hole");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Sunken Holes/Row-A", (1f / 3f))]
    [Button("End Level Hole")]
    public void EndLevelHole()
    {
        AddInteractableType("End Level Hole");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Sunken Holes/Row-B", (1f / 3f))]
    [Button("Fan - Sunken Hole")]
    public void FanSunkenHole()
    {
        AddInteractableType("Fan - Sunken Hole");

        UpdateBuilder();
    }

    [BoxGroup("Interactables/Chips")]

    [HorizontalGroup("Interactables/Chips/Row-A", (1f / 3f))]
    [Button("Chip (Dual)")]
    public void ChipDual()
    {
        AddInteractableType("Chip (Dual)");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Chips/Row-A", (1f / 3f))]
    [Button("Chip (Straight)")]
    public void ChipStraight()
    {
        AddInteractableType("Chip (Straight)");

        UpdateBuilder();
    }


    [BoxGroup("Interactables/Planks")]

    [HorizontalGroup("Interactables/Planks/Row-A", (1f / 3f))]
    [Button("Plank 1")]
    public void Plank1()
    {
        AddInteractableType("Plank 1");

        UpdateBuilder();
    }

    [HorizontalGroup("Interactables/Planks/Row-A", (1f / 3f))]
    [Button("Plank 2")]
    public void Plank2()
    {
        AddInteractableType("Plank 2");

        UpdateBuilder();
    }


    public void AddInteractable(string partName)
    {


        GameObject newPart = SpawnObject(GetPrefab(interactablePrefabs, partName));

        newPart.transform.parent = currentPiece.transform;
        newPart.transform.localPosition = Vector3.zero;
        newPart.transform.localRotation = Quaternion.identity;

        currentPiece.interactables.Add(newPart.transform);



        Debug.Log("Builder >> Added Interactable");
    }



    public void RemoveInteractable()
    {
        if (!currentPiece)
        {
            Debug.Log("Builder >> No Piece Selected");
            return;
        }
        else if (currentPiece.interactables.Count == 0)
        {
            Debug.Log("Builder >> No Interactable To Remove");
            return;
        };

        Transform interactable = currentPiece.interactables[currentPiece.interactables.Count - 1];

        currentPiece.interactables.Remove(interactable);
        DestroyImmediate(interactable.gameObject);

        Debug.Log("Builder >> Removed Interactable");
    }




    public void AddInteractableType(string partName)
    {
        if (!currentPiece)
        {
            Debug.Log("Builder >> No Piece Selected");
            currentPiece = PlaceBase().GetComponent<LevelPiece>();
        }
        else if (currentPiece.interactables.Count > 0)
        {
            RemoveInteractable();
        };

        AddInteractable(partName);

        
    }


    public void RotateInteractable(Vector3 direction)
    {
        currentPiece.interactables.ForEach(part =>
        {
            part.Rotate(direction);
        });
    }




    [Title("Settings")]
    public float movementAmount = 1f;



















    [Title("Parts")]
    public Transform levelRoot;
    public Transform builderMarker;
    public Transform builderMarkerActive;
    public Transform builderMarkerInactive;
    public LevelManager levelManager;



    [Title("System")]
    public Vector3 currentPosition;
    public LevelPiece currentPiece;
    public LevelPiece clipboard;


    [System.Serializable]
    public class PrefabPart
    {
        public string name;
        public Transform prefab;
        public bool floorActive = true;
    }

    [FoldoutGroup("Prefabs")]
    public List<PrefabPart> basePrefabs;

    [FoldoutGroup("Prefabs")]
    public List<PrefabPart> wallPrefabs;
    
    [FoldoutGroup("Prefabs")]
    public List<PrefabPart> interactablePrefabs;


    public void ShowHideFloor(bool state)
    {
        if (currentPiece.floor)
        {
            currentPiece.floor.gameObject.SetActive(state);
        };
    }

    public void ToggleFloor()
    {
        if (currentPiece.floor)
        {
            currentPiece.floor.gameObject.SetActive(!currentPiece.floor.gameObject.activeSelf);
        };
    }

    public void PastePiece()
    {
        if (currentPiece)
        {
            Debug.Log("Builder >> Already Piece Here");
            return;
        } else if (!clipboard)
        {
            Debug.Log("Builder >> Nothing Copied");
            return;
        }


        currentPiece = PlaceBase().GetComponent<LevelPiece>();

        


        clipboard.walls.ForEach(part => {
            GameObject newPart = SpawnObject(GetPrefab(wallPrefabs, part));

            Vector3 rotation = Vector3.zero;

            newPart.transform.rotation = part.transform.rotation;
            newPart.transform.parent = currentPiece.levelBase;
            newPart.transform.position = currentPiece.levelBase.position;

            currentPiece.walls.Add(newPart.transform);

        });

        clipboard.interactables.ForEach(part => {
            GameObject newPart = SpawnObject(GetPrefab(interactablePrefabs, part));

            Vector3 rotation = Vector3.zero;

            newPart.transform.rotation = part.transform.rotation;
            newPart.transform.parent = currentPiece.levelBase;
            newPart.transform.position = currentPiece.levelBase.position;

            currentPiece.interactables.Add(newPart.transform);

        });

    }






    public Transform GetPrefab(List<PrefabPart> prefabs, Transform transform)
    {
        Transform prefabFound = null;

        prefabs.ForEach(prefab => {
            if (prefab.prefab.name == transform.name)
            {
                prefabFound = prefab.prefab;
            };
        });

        return prefabFound;
    }

    public Transform GetPrefab(List<PrefabPart> prefabs, string name)
    {
        Transform prefabFound = prefabs[0].prefab;

        prefabs.ForEach(prefab => {
            if (prefab.name == name)
            {
                prefabFound = prefab.prefab;
            };
        });

        return prefabFound;
    }



    public Transform GetRandomPrefab(List<PrefabPart> prefabs)
    {
        RandomSeedGenerator.RandomizeSeed();

        int id = Random.Range(0, (prefabs.Count));

        Debug.Log("Random Prefab Id:" + id);

        return prefabs[id].prefab;
    }



    public void RemovePiece()
    {
        if (!currentPiece)
        {
            Debug.Log("Builder >> No Piece To Delete");
            return;
        }

        Undo.RecordObject(currentPiece.gameObject, "Removed Object");
        DestroyImmediate(currentPiece.gameObject);
    }




    public void RandomizePiece()
    {
        RemovePiece();
        PlaceBase();
    }



    public void AddWall(string wallName)
    {
        

        GameObject newWall = SpawnObject(GetPrefab(wallPrefabs, wallName));

        newWall.transform.parent = currentPiece.transform;
        newWall.transform.localPosition = Vector3.zero;
        newWall.transform.localRotation = Quaternion.identity;

        currentPiece.walls.Add(newWall.transform);

        Debug.Log("Builder >> Added Wall");
    }



    public void RemoveWalls()
    {
        if (!currentPiece)
        {
            Debug.Log("Builder >> No Piece Selected");
            return;
        }
        else if (currentPiece.walls.Count == 0)
        {
            Debug.Log("Builder >> No Walls To Remove");
            return;
        };

        Transform wall = currentPiece.walls[currentPiece.walls.Count - 1];

        currentPiece.walls.Remove(wall);
        DestroyImmediate(wall.gameObject);

        Debug.Log("Builder >> Removed Walls");
    }




    public void AddWallType(string wallType)
    {
        if (!currentPiece)
        {
            Debug.Log("Builder >> No Piece Selected");
            currentPiece = PlaceBase().GetComponent<LevelPiece>();
        } else if (currentPiece.walls.Count > 0)
        {
            RemoveWalls();
        };

        AddWall(wallType);
    }


    public void RotateWall(Vector3 direction)
    {
        currentPiece.walls.ForEach(wall =>
        {
            wall.Rotate(direction);
        });
    }



    public void AddToPiece()
    {
        
    }





    public void RotateBase(Vector3 direction)
    {
        currentPiece.transform.Rotate(direction);
    }

    public Transform PlaceBase()
    {
        if (currentPiece)
        {
            Debug.Log("Builder >> Already Piece Here");
            return null;
        }

        GameObject newBase = SpawnObject(GetRandomPrefab(basePrefabs));

        Vector3 rotation = Vector3.zero;
        rotation.y = Random.Range(0, 4) * 90;
        newBase.transform.eulerAngles = rotation;
        newBase.transform.parent = levelRoot;
        
        LevelPiece levelPiece = newBase.AddComponent<LevelPiece>();
        levelPiece.levelBase = newBase.transform;

        return newBase.transform;
    }


    public GameObject SpawnObject(Transform obj)
    {
        return SpawnObject(obj, currentPosition, Vector3.zero);
    }




    public GameObject SpawnObject(Transform obj, Vector3 position, Vector3 rotation)
    {

        LevelBuilderWindow levelBuilderWindow = (LevelBuilderWindow)EditorWindow.GetWindow(typeof(LevelBuilderWindow));

        Debug.Log("levelBuilderWindow: " + (levelBuilderWindow != null));
        Debug.Log("Spawning Object: " + obj);

        Transform newObj = levelBuilderWindow.SpawnPrefab(obj);

        Debug.Log("Spawned Object: " + newObj);
        //Transform newObj = Instantiate(obj, Vector3.zero, Quaternion.identity) as Transform;

        Undo.RecordObject(newObj.gameObject, "Created Object");

        newObj.parent = null;

        newObj.position = position;
        newObj.eulerAngles = rotation;

        newObj.name = obj.name;

        return newObj.gameObject;
    }





    public void UpdateBuilder()
    {
        Undo.RecordObject(gameObject, "Updated Builder");

        levelManager.Refresh();
        currentPiece = levelManager.GetPieceAt(currentPosition);

        UpdateMarker();
    }



    public void UpdateMarker()
    {

        Undo.RecordObject(builderMarker, "Moved Marker");

        builderMarkerActive.gameObject.SetActive(currentPiece);
        builderMarkerInactive.gameObject.SetActive(!currentPiece);

        builderMarker.position = currentPosition;

        EditorUtility.SetDirty(levelRoot);
    }
}

#endif