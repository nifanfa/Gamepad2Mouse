using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;

namespace ConsoleApp2
{
    class Gamepad2Mouse
    {
        private const int MomentDivider = 2_000;
        private const int ScrollDivider = 10_000;
        private Controller controller;
        private IMouseSimulator mouseSimulator;
        private Timer timer;

        private bool wasADown;
        private bool wasBDown;
        private int RefreshRate = 60;

        public Gamepad2Mouse()
        {
            controller = new Controller(UserIndex.One);
            mouseSimulator = new InputSimulator().Mouse;
            timer = new Timer(obj => Update());
        }

        public void Start() 
        {
            timer.Change(0,1000/RefreshRate);
        }

        private void Update()
        {
            controller.GetState(out var state);
            Movement(state);
            Scroll(state);
            LeftButton(state);
            RightButton(state);
        }

        private void RightButton(State state)
        {
            var isBDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B);
            if (isBDown && !wasBDown) mouseSimulator.RightButtonDown();
            if (!isBDown && wasBDown) mouseSimulator.RightButtonUp();
            wasBDown = isBDown;
        }

        private void LeftButton(State state)
        {
            var isADown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A);
            if (isADown && !wasADown) mouseSimulator.LeftButtonDown();
            if (!isADown && wasADown) mouseSimulator.LeftButtonUp();
            wasADown = isADown;
        }

        private void Scroll(State state)
        {
            var x = state.Gamepad.RightThumbX / ScrollDivider;
            var y = state.Gamepad.RightThumbY / ScrollDivider;

            mouseSimulator.HorizontalScroll(x);
            mouseSimulator.VerticalScroll(y);
        }

        private void Movement(State state)
        {
            var x = state.Gamepad.LeftThumbX / MomentDivider;
            var y = state.Gamepad.LeftThumbY / MomentDivider;

            mouseSimulator.MoveMouseBy(x, -y);
        }
    }
}
