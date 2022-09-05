using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData
{
    public string ID;
    public enum DialogTypes { Standard, Ask, Answer_1, Answer_End }
    public DialogTypes Type;
    public string ActorName;
    public int Face;
    public string Speech;
}
