using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialog", menuName = "NPC/Dialog")]
public class NPCDialogSO : ScriptableObject
{
    [System.Serializable]
    public class NPCDialog
    {
        
        public string Name;
        public string[] Dialog; // セリフの配列
        public float HeartSize; // ハートのサイズ
    }

    public NPCDialog[] npcNum;
}
