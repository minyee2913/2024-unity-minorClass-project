using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComp : ThingComp
{
    public int Speed { get; private set; }

    public bool IsMoving { get; private set; }
    private Vector3Int from, to;
    int movingTick = 0;
    int endTick = 0;
    float movingTime = 0;

    public MoveComp(Thing thing, int speed) : base(thing)
    {
        Speed = speed;
    }

    public override void Update()
    {
        if (!IsMoving)
            return;

        movingTime += Time.deltaTime;
        Thing.transform.position = new Vector3(
            Mathf.Lerp(from.x, to.x, movingTime * 60f / endTick),
            0,
            Mathf.Lerp(from.z, to.z, movingTime * 60f / endTick));
    }

    public override void Tick()
    {
        if (!IsMoving)
            return;

        movingTick++;
        if (movingTick >= endTick)
        {
            IsMoving = false;
            Thing.transform.position = new Vector3(to.x, to.y, to.z);
        }
    }

    public void Move(Vector3Int from, Vector3Int to)
    {
        if (from == to || Thing.Pos != from || ThingSystem.Instance.FindThing(to) != null)
            return;

        IsMoving = true;
        this.from = from;
        this.to = to;
        movingTick = 0;
        movingTime = 0;
        endTick = (int)(Vector3Int.Distance(from, to) * Speed);

        Thing.Pos = to;
        Thing.transform.position = new Vector3(from.x, from.y, from.z);

        Thing.transform.LookAt(to);
    }
}