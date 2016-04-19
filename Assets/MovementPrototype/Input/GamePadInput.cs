using UnityEngine;
using XInputDotNetPure;

internal static class Extension
{
    public static XInputDotNetPure.PlayerIndex toXInput(this PlayerIndex id)
    {
        switch (id)
        {
            default:
            case PlayerIndex.One:
                return XInputDotNetPure.PlayerIndex.One;
            case PlayerIndex.Two:
                return XInputDotNetPure.PlayerIndex.Two;
        }
    }
}

public class GamePadInput : BaseInput
{
    GamePadState state;
    GamePadState lastState;

    public PlayerIndex id { get; private set; }

    public GamePadInput(PlayerIndex id)
    {
        this.id = id;
        name = "GamePad" + id;
    }

    static readonly float triggerThreshold = 0.2f;

    bool PressedB
    {
        get { return state.Buttons.B == ButtonState.Pressed && lastState.Buttons.B == ButtonState.Released; }
    }
    bool PressedX
    {
        get { return state.Buttons.X == ButtonState.Pressed && lastState.Buttons.X == ButtonState.Released; }
    }
    bool PressedA
    {
        get { return state.Buttons.A == ButtonState.Pressed && lastState.Buttons.A == ButtonState.Released; }
    }
    bool PressedY
    {
        get { return state.Buttons.Y == ButtonState.Pressed && lastState.Buttons.Y == ButtonState.Released; }
    }
    bool PressedRS
    {
        get { return state.Buttons.RightShoulder == ButtonState.Pressed && lastState.Buttons.RightShoulder == ButtonState.Released; }
    }
    bool PressedRT
    {
        get { return state.Triggers.Right > triggerThreshold && (lastState.Triggers.Right <= triggerThreshold); }
    }

    public override void Update()
    {
        lastState = state;
        state = GamePad.GetState(id.toXInput());

        buffer.Update();

        move.horizontal = state.ThumbSticks.Left.X;
        move.vertical = state.ThumbSticks.Left.Y;

        look.horizontal = state.ThumbSticks.Right.X;
        look.vertical = state.ThumbSticks.Right.Y;

        // modifiers
        run = state.Triggers.Left;
        var high = state.Buttons.LeftShoulder == ButtonState.Pressed;

        var light = PressedRS;
        var heavy = PressedRT;
        var blocked = PressedY;
        var dashed = PressedB;

        dash |= dashed;
        attack |= light;
        heavyAttack |= heavy;
        block |= blocked;


        if (dashed)
        {
            if (high)
            {
                buffer.Push(new InputEvent.Lunge(move));
            }
            else
            {
                buffer.Push(new InputEvent.Dash(move));
            }
        }
        else if (blocked)
        {
            buffer.Push(new InputEvent.Block(isHigh: high));
        }
        else if (light)
        {
            buffer.Push(new InputEvent.Attack(isHigh: high));
        }
        else if (heavy)
        {
            buffer.Push(new InputEvent.Attack(isHigh: high, isHeavy: true));
        }
    }

    public override void FixedUpdate()
    {
        dash = false;
        attack = false;
        block = false;
    }

    public override string Debug
    {
        get
        {
            string text = "";
            text += string.Format("IsConnected {0}\n", state.IsConnected);
            text += string.Format("Stick Left  {0,4:0.0} {1,4:0.0}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
            text += string.Format("Stick Right {0,4:0.0} {1,4:0.0}\n", state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
            text += string.Format("Shoulders {0,8} {1,8}\n", state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
            text += string.Format("Triggers  {0,4:0.0} {1,4:0.0}\n", state.Triggers.Left, state.Triggers.Right);
            return text;
        }
    }

}