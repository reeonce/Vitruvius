﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using NLog;

namespace LightBuzz.Vitruvius
{
    /// <summary>
    /// Represents the space gesture controller.
    /// </summary>
    public class SpaceGestureController: BaseController<SpaceBody>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Members

        /// <summary>
        /// A list of all the gestures the controller is searching for.
        /// </summary>
        protected List<SpaceGesture> _gestures = new List<SpaceGesture>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="GestureController"/> with all of the available gesture types.
        /// </summary>
        public SpaceGestureController()
        {
            foreach (SpaceGestureType t in Enum.GetValues(typeof(SpaceGestureType)))
            {
                AddGesture(t);
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GestureController"/> with the specified gesture type.
        /// </summary>
        /// <param name="type">The gesture type to recognize.</param>
        public SpaceGestureController(SpaceGestureType type)
        {
            AddGesture(type);
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a gesture is recognized.
        /// </summary>
        public event EventHandler<SpaceGestureEventArgs> GestureRecognized;

        #endregion

        #region Methods

        /// <summary>
        /// Updates all gestures.
        /// </summary>
        /// <param name="body">The body data to search for gestures.</param>
        public override void Update(SpaceBody body)
        {
            base.Update(body);

            foreach (SpaceGesture gesture in _gestures)
            {
                gesture.Update(body);
            }
        }

        /// <summary>
        /// Adds the specified gesture for recognition.
        /// </summary>
        /// <param name="type">The predefined <see cref="GestureType" />.</param>
        public void AddGesture(SpaceGestureType type)
        {
            // Check whether the gesure is already added.
            if (_gestures.Where(g => g.GestureType == type).Count() > 0) return;

            ISpaceGestureSegment[] segments = null;
            SpaceGesture gesture = null;

            // DEVELOPERS: If you add a new predefined gesture with a new GestureType,
            // simply add the proper segments to the switch statement here.
            switch (type)
            {
                case SpaceGestureType.FallDownOnGround:
                    segments = new ISpaceGestureSegment[3];

                    FallDownOnFloorSegment1 segment1 = new FallDownOnFloorSegment1();
                    FallDownOnFloorSegment2 segment2 = new FallDownOnFloorSegment2();
                    FallDownOnFloorSegment3 segment3 = new FallDownOnFloorSegment3();

                    segments[0] = segment1;
                    segments[1] = segment2;
                    segments[2] = segment3;

                    gesture = new SpaceGesture(type, segments);
                    gesture.WINDOW_SIZE = 100;
                    break;
                case SpaceGestureType.LyingOnGround:
                    segments = new ISpaceGestureSegment[100];

                    LyingOnFloorSegment segment = new LyingOnFloorSegment();
                    for (int i = 0; i < 100; i++)
                    {
                        segments[i] = segment;
                    }

                    gesture = new SpaceGesture(type, segments);
                    gesture.WINDOW_SIZE = 40;
                    break;
                default:
                    break;
            }

            if (gesture != null)
            {
                gesture.GestureRecognized += OnGestureRecognized;
                _gestures.Add(gesture);
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Handles the GestureRecognized event of the g control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GestureEventArgs"/> instance containing the event data.</param>
        protected void OnGestureRecognized(object sender, SpaceGestureEventArgs e)
        {
            if (GestureRecognized != null)
            {
                GestureRecognized(this, e);
            }

            foreach (SpaceGesture gesture in _gestures)
            {
                gesture.Reset();
            }
        }

        #endregion
    }
}
