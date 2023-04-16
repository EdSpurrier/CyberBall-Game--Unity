using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class LevelPiece : MonoBehaviour
{
    public Transform levelBase;
    public Transform floor;
    public List<Transform> walls = new List<Transform>();
    public List<Transform> interactables = new List<Transform>();
}
