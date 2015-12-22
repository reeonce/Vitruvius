using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using NLog;

namespace LightBuzz.Vitruvius
{
    class LyingOnFloorSegment : ISpaceGestureSegment
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static IDictionary<ulong, LyingOnFloorSegment> previousSegments = new Dictionary<ulong, LyingOnFloorSegment>();

        private DateTime updateTime;

        private double totalLength = 0;
        private int lengthCount = 0;

        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="spaceBody">The spaceBody.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(SpaceBody spaceBody)
        {
            Body body = spaceBody.body;

            LyingOnFloorSegment previousSegment = null;

            if (previousSegments.Keys.Contains(body.TrackingId))
            {
                previousSegment = previousSegments[body.TrackingId];
            }

            if (previousSegment != null)
            {
                if ((DateTime.Now - previousSegment.updateTime).TotalSeconds > 30)
                {
                    previousSegments[body.TrackingId] = null;
                }
                else
                {
                    totalLength = previousSegment.totalLength;
                    lengthCount = previousSegment.lengthCount;
                }
            }

            if (body.Joints[JointType.ShoulderLeft].TrackingState == TrackingState.Tracked && body.Joints[JointType.ShoulderRight].TrackingState == TrackingState.Tracked)
            {
                double shoulderWidth = body.Joints[JointType.ShoulderLeft].Position.Length(body.Joints[JointType.ShoulderRight].Position);
                totalLength += shoulderWidth * 0.7;
                lengthCount++;
            }


            if (body.Joints[JointType.AnkleLeft].TrackingState == TrackingState.Tracked && body.Joints[JointType.KneeLeft].TrackingState == TrackingState.Tracked)
            {
                double leftLegLength = body.Joints[JointType.AnkleLeft].Position.Length(body.Joints[JointType.KneeLeft].Position);
                totalLength += leftLegLength * 0.7;
                lengthCount++;
            }

            if (body.Joints[JointType.AnkleRight].TrackingState == TrackingState.Tracked && body.Joints[JointType.KneeRight].TrackingState == TrackingState.Tracked)
            {
                double rightLegLength = body.Joints[JointType.AnkleRight].Position.Length(body.Joints[JointType.KneeRight].Position);
                totalLength += rightLegLength * 0.7;
                lengthCount++;
            }

            if (body.Joints[JointType.WristLeft].TrackingState == TrackingState.Tracked && body.Joints[JointType.ElbowLeft].TrackingState == TrackingState.Tracked)
            {
                double leftHandLength = body.Joints[JointType.WristLeft].Position.Length(body.Joints[JointType.ElbowLeft].Position);
                totalLength += leftHandLength;
                lengthCount++;
            }


            if (body.Joints[JointType.WristRight].TrackingState == TrackingState.Tracked && body.Joints[JointType.ElbowRight].TrackingState == TrackingState.Tracked)
            {
                double rightHandLength = body.Joints[JointType.WristRight].Position.Length(body.Joints[JointType.ElbowRight].Position);
                totalLength += rightHandLength;
                lengthCount++;
            }

            updateTime = DateTime.Now;

            double maxBodyHeight = 0;
            if (lengthCount > 0)
            {
                maxBodyHeight = totalLength / lengthCount;
            }

            if (body.Joints[JointType.SpineShoulder].TrackingState != TrackingState.Tracked || body.Joints[JointType.SpineBase].TrackingState != TrackingState.Tracked)
            {
                return GesturePartResult.Undetermined;
            }

            CameraSpacePoint shoulderPosition = body.Joints[JointType.SpineShoulder].Position;
            CameraSpacePoint corePosition = body.Joints[JointType.SpineBase].Position;

            if (spaceBody.floorPlane.X == 0 && spaceBody.floorPlane.Y == 0 && spaceBody.floorPlane.Z == 0)
            {
                return GesturePartResult.Undetermined;
            }

            double coreHeight = spaceBody.floorPlane.Length(corePosition);
            double shoulderHeight = spaceBody.floorPlane.Length(shoulderPosition);

            if (coreHeight < maxBodyHeight && shoulderHeight < maxBodyHeight && coreHeight > -maxBodyHeight * 0.2 && shoulderHeight > -maxBodyHeight * 0.2)
            {
                previousSegments[body.TrackingId] = this;
                return GesturePartResult.Succeeded;
            }
            else if (coreHeight < maxBodyHeight * 1.5 && shoulderHeight < maxBodyHeight * 1.5)
            {
                return GesturePartResult.Undetermined;
            }

            return GesturePartResult.Failed;
        }
    }
}
