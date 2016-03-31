using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class CState : BaseState
{
    public PlayerFsm FsmPlayer { get; protected set; }
    public CState(BaseFsm fsm) : base(fsm)
    {
        FsmPlayer = (PlayerFsm)fsm;
    }
}

