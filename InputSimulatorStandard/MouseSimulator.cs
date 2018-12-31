﻿namespace InputSimulatorStandard
{
    using System;
    using System.Threading;

    using Native;

    /// <inheritdoc />
    /// <summary>
    /// Implements the <see cref="IMouseSimulator" /> interface by calling the an <see cref="IInputMessageDispatcher" /> to simulate Mouse gestures.
    /// </summary>
    public class MouseSimulator : IMouseSimulator
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the amount of mouse wheel scrolling per click. The default value for this property is 120 and different values may cause some applications to interpret the scrolling differently than expected.
        /// </summary>
        public int MouseWheelClickSize { get; set; } = 120;

        private readonly IInputSimulator inputSimulator;

        /// <summary>
        /// The instance of the <see cref="IInputMessageDispatcher"/> to use for dispatching <see cref="Input"/> messages.
        /// </summary>
        private readonly IInputMessageDispatcher messageDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseSimulator"/> class using an instance of a <see cref="WindowsInputMessageDispatcher"/> for dispatching <see cref="Input"/> messages.
        /// </summary>
        /// <param name="inputSimulator">The <see cref="IInputSimulator"/> that owns this instance.</param>
        public MouseSimulator(IInputSimulator inputSimulator)
        {
            this.inputSimulator = inputSimulator ?? throw new ArgumentNullException(nameof(inputSimulator));
            this.messageDispatcher = new WindowsInputMessageDispatcher();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseSimulator"/> class using the specified <see cref="IInputMessageDispatcher"/> for dispatching <see cref="Input"/> messages.
        /// </summary>
        /// <param name="inputSimulator">The <see cref="IInputSimulator"/> that owns this instance.</param>
        /// <param name="messageDispatcher">The <see cref="IInputMessageDispatcher"/> to use for dispatching <see cref="Input"/> messages.</param>
        /// <exception cref="InvalidOperationException">If null is passed as the <paramref name="messageDispatcher"/>.</exception>
        internal MouseSimulator(IInputSimulator inputSimulator, IInputMessageDispatcher messageDispatcher)
        {
            this.inputSimulator = inputSimulator ?? throw new ArgumentNullException(nameof(inputSimulator));
            this.messageDispatcher = messageDispatcher ?? throw new InvalidOperationException(
                                         string.Format("The {0} cannot operate with a null {1}. Please provide a valid {1} instance to use for dispatching {2} messages.",
                                             typeof(MouseSimulator).Name, typeof(IInputMessageDispatcher).Name, typeof(Input).Name));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the <see cref="IKeyboardSimulator" /> instance for simulating Keyboard input.
        /// </summary>
        /// <value>The <see cref="IKeyboardSimulator" /> instance.</value>
        public IKeyboardSimulator Keyboard => this.inputSimulator.Keyboard;

        /// <summary>
        /// Sends the list of <see cref="Input"/> messages using the <see cref="IInputMessageDispatcher"/> instance.
        /// </summary>
        /// <param name="inputList">The <see cref="System.Array"/> of <see cref="Input"/> messages to send.</param>
        private void SendSimulatedInput(Input[] inputList)
        {
            this.messageDispatcher.DispatchInput(inputList);
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates mouse movement by the specified distance measured as a delta from the current mouse location in pixels.
        /// </summary>
        /// <param name="pixelDeltaX">The distance in pixels to move the mouse horizontally.</param>
        /// <param name="pixelDeltaY">The distance in pixels to move the mouse vertically.</param>
        public IMouseSimulator MoveMouseBy(int pixelDeltaX, int pixelDeltaY)
        {
            var inputList = new InputBuilder().AddRelativeMouseMovement(pixelDeltaX, pixelDeltaY).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates mouse movement to the specified location on the primary display device.
        /// </summary>
        /// <param name="absoluteX">The destination's absolute X-coordinate on the primary display device where 0 is the extreme left hand side of the display device and 65535 is the extreme right hand side of the display device.</param>
        /// <param name="absoluteY">The destination's absolute Y-coordinate on the primary display device where 0 is the top of the display device and 65535 is the bottom of the display device.</param>
        public IMouseSimulator MoveMouseTo(double absoluteX, double absoluteY)
        {
            var inputList = new InputBuilder().AddAbsoluteMouseMovement((int)Math.Truncate(absoluteX), (int)Math.Truncate(absoluteY)).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates mouse movement to the specified location on the Virtual Desktop which includes all active displays.
        /// </summary>
        /// <param name="absoluteX">The destination's absolute X-coordinate on the virtual desktop where 0 is the left hand side of the virtual desktop and 65535 is the extreme right hand side of the virtual desktop.</param>
        /// <param name="absoluteY">The destination's absolute Y-coordinate on the virtual desktop where 0 is the top of the virtual desktop and 65535 is the bottom of the virtual desktop.</param>
        public IMouseSimulator MoveMouseToPositionOnVirtualDesktop(double absoluteX, double absoluteY)
        {
            var inputList = new InputBuilder().AddAbsoluteMouseMovementOnVirtualDesktop((int)Math.Truncate(absoluteX), (int)Math.Truncate(absoluteY)).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse left button down gesture.
        /// </summary>
        public IMouseSimulator LeftButtonDown()
        {
            var inputList = new InputBuilder().AddMouseButtonDown(MouseButton.LeftButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse left button up gesture.
        /// </summary>
        public IMouseSimulator LeftButtonUp()
        {
            var inputList = new InputBuilder().AddMouseButtonUp(MouseButton.LeftButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse left-click gesture.
        /// </summary>
        public IMouseSimulator LeftButtonClick()
        {
            var inputList = new InputBuilder().AddMouseButtonClick(MouseButton.LeftButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse left button double-click gesture.
        /// </summary>
        public IMouseSimulator LeftButtonDoubleClick()
        {
            var inputList = new InputBuilder().AddMouseButtonDoubleClick(MouseButton.LeftButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse middle button down gesture.
        /// </summary>
        public IMouseSimulator MiddleButtonDown()
        {
            var inputList = new InputBuilder().AddMouseButtonDown(MouseButton.MiddleButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse middle button up gesture.
        /// </summary>
        public IMouseSimulator MiddleButtonUp()
        {
            var inputList = new InputBuilder().AddMouseButtonUp(MouseButton.MiddleButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse middle button click gesture.
        /// </summary>
        public IMouseSimulator MiddleButtonClick()
        {
            var inputList = new InputBuilder().AddMouseButtonClick(MouseButton.MiddleButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse middle button double click gesture.
        /// </summary>
        public IMouseSimulator MiddleButtonDoubleClick()
        {
            var inputList = new InputBuilder().AddMouseButtonDoubleClick(MouseButton.MiddleButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse right button down gesture.
        /// </summary>
        public IMouseSimulator RightButtonDown()
        {
            var inputList = new InputBuilder().AddMouseButtonDown(MouseButton.RightButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse right button up gesture.
        /// </summary>
        public IMouseSimulator RightButtonUp()
        {
            var inputList = new InputBuilder().AddMouseButtonUp(MouseButton.RightButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse right button click gesture.
        /// </summary>
        public IMouseSimulator RightButtonClick()
        {
            var inputList = new InputBuilder().AddMouseButtonClick(MouseButton.RightButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse right button double-click gesture.
        /// </summary>
        public IMouseSimulator RightButtonDoubleClick()
        {
            var inputList = new InputBuilder().AddMouseButtonDoubleClick(MouseButton.RightButton).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse X button down gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public IMouseSimulator XButtonDown(int buttonId)
        {
            var inputList = new InputBuilder().AddMouseXButtonDown(buttonId).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse X button up gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public IMouseSimulator XButtonUp(int buttonId)
        {
            var inputList = new InputBuilder().AddMouseXButtonUp(buttonId).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse X button click gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public IMouseSimulator XButtonClick(int buttonId)
        {
            var inputList = new InputBuilder().AddMouseXButtonClick(buttonId).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse X button double-click gesture.
        /// </summary>
        /// <param name="buttonId">The button id.</param>
        public IMouseSimulator XButtonDoubleClick(int buttonId)
        {
            var inputList = new InputBuilder().AddMouseXButtonDoubleClick(buttonId).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates mouse vertical wheel scroll gesture.
        /// </summary>
        /// <param name="scrollAmountInClicks">The amount to scroll in clicks. A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user.</param>
        public IMouseSimulator VerticalScroll(int scrollAmountInClicks)
        {
            var inputList = new InputBuilder().AddMouseVerticalWheelScroll(scrollAmountInClicks * this.MouseWheelClickSize).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Simulates a mouse horizontal wheel scroll gesture. Supported by Windows Vista and later.
        /// </summary>
        /// <param name="scrollAmountInClicks">The amount to scroll in clicks. A positive value indicates that the wheel was rotated to the right; a negative value indicates that the wheel was rotated to the left.</param>
        public IMouseSimulator HorizontalScroll(int scrollAmountInClicks)
        {
            var inputList = new InputBuilder().AddMouseHorizontalWheelScroll(scrollAmountInClicks * this.MouseWheelClickSize).ToArray();
            this.SendSimulatedInput(inputList);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait.</param>
        public IMouseSimulator Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sleeps the executing thread to create a pause between simulated inputs.
        /// </summary>
        /// <param name="timeout">The time to wait.</param>
        public IMouseSimulator Sleep(TimeSpan timeout)
        {
            Thread.Sleep(timeout);
            return this;
        }
    }
}
