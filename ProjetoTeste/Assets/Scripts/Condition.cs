using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Condition
{
    public string StatusName { get; set; }
    public string Description { get; set; }
    public string StartMessage { get; set; }
    public Color StatusColor { get; set; }

    public Action<Pokemon> OnAfterTurn { get; set; }

    public Func<Pokemon, bool> OnBeforeMove { get; set; }

    public Action<Pokemon> OnStart { get; set; }
}
