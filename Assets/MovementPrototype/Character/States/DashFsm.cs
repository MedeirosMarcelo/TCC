using UnityEngine;
using System.Collections;
using CDash = Assets.MovementPrototype.Character.States.CDash;


public class DashFsm : BaseFsm
{
    public class DashState : BaseState
    {
        public new DashFsm Fsm { get; private set; }
        public DashState(DashFsm fsm) : base(fsm)
        {
            Fsm = fsm;
        }
        public override void PreUpdate()
        {
            // NOTE: Dificil acessar os membors protegidos
            // Fsm.dash.Character;
        }
        public override void Update()
        {
        }
        public override void Enter(StateTransitionArgs args)
        {
        }
        public override void Exit(StateTransitionArgs args)
        {
        }
    }

    public class Accel : DashState
    {
        public Accel(DashFsm fsm) : base(fsm)
        {
            Name = "ACCEL";
        }
    }
    public class Plateau : DashState
    {
        public Plateau(DashFsm fsm) : base(fsm)
        {
            Name = "PLATEAU";
        }
    }
    public class Deccel : DashState
    {
        public Deccel(DashFsm fsm) : base(fsm)
        {
            Name = "DECCEL";
        }
    }
    public class Ended : DashState
    {
        public Ended(DashFsm fsm) : base(fsm)
        {
            Name = "ENDED";
        }
        // NOTE: mudar o estado da maquina mestre esta OK
        public override void PreUpdate()
        {
            Fsm.ChangeState("IDLE");
        }
    }

    CDash dash;
    // NOTE: Para simular um "Enter" estava recontruindo inutilmente a FSM
    public DashFsm(CDash dash) : base("DashStates")
    {
        this.dash = dash;
        var loader = new StateLoader<DashFsm, BaseState>();
        // NOTE: Carregar estados aninhados parece util
        loader.LoadNestedStates(this);
        current = dict["ACCEL"];
    }

    // NOTE: Dificil passar PreUpdate do estado para a sub Fsm
    public void PreUpdate()
    {
        current.PreUpdate();
    }
    public override void Update()
    {
        current.Update();
    }
}
