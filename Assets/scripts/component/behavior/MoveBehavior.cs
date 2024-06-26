using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MoveBehavior : Behavior
{
    private MoveComp moveComp;
    private Vector3Int from, to;

    public MoveBehavior(Thing thing, Vector3Int from, Vector3Int to)
    {
        moveComp = (MoveComp)thing.GetComp(typeof(MoveComp));
        this.from = from;
        this.to = to;
    }

    public override void InitSteps()
    {
        steps = new();
        List<Vector3Int> path;
        if (ThingSystem.Instance.PathFind(moveComp.Thing.Pos, to, out path, 20))
            for (int i = 0; i < path.Count - 1; i++)
                steps.Add(new MoveStep(moveComp, path[i], path[i + 1]));
    }
}