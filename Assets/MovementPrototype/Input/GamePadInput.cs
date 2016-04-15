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

    public override void Update()
    {
        lastState = state;
        state = GamePad.GetState(id.toXInput());

        buffer.Update();

        move.horizontal = state.ThumbSticks.Left.X;
        move.vertical = state.ThumbSticks.Left.Y;

        look.horizontal = state.ThumbSticks.Right.X;
        look.vertical = state.ThumbSticks.Right.Y;

        run = state.Triggers.Left;

        var dashed = (state.Buttons.LeftShoulder == ButtonState.Pressed
               && lastState.Buttons.LeftShoulder == ButtonState.Released)
               || (state.Buttons.B == ButtonState.Pressed && lastState.Buttons.B == ButtonState.Released);

        var attacked = state.Buttons.RightShoulder == ButtonState.Pressed
                && lastState.Buttons.RightShoulder == ButtonState.Released;

        var blocked = state.Triggers.Right > triggerThreshold
              && (lastState.Triggers.Right <= triggerThreshold);

        dash |= dashed;
        attack |= attacked;
        block |= blocked;

        if (dashed)
        {
            buffer.Push(new InputEvent.Dash(move));
        }
        else if (attacked)
        {
            buffer.Push(new InputEvent.Attack(move));
        }
        else if (blocked)
        {
            buffer.Push(new InputEvent.Block());
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