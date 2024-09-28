using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "TutorialDialogueData", menuName = "ScriptableObject/TutorialDialogueData", order = 3)]
public class TutorialDialogueData : ScriptableObject
{
    public List<string> data;
}

