using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace LightBuzz.Vitruvius
{
    class LyingOnFloorSegment : ISpaceGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="spaceBody">The spaceBody.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(SpaceBody spaceBody)
        {
            Body body = spaceBody.body;

            CameraSpacePoint shoulderPosition = body.Joints[JointType.SpineShoulder].Position;
            CameraSpacePoint corePosition = body.Joints[JointType.SpineBase].Position;
            CameraSpacePoint headPosition = body.Joints[JointType.Head].Position;

            if (Math.Abs(corePosition.X - FallDownOnFloorSegment3.lyingCorePosition.X) > FallDownOnFloorSegment1.originBodyLength)
            {
                return GesturePartResult.Failed;
            }

            if (body.Joints[JointType.SpineShoulder].TrackingState != TrackingState.Tracked || body.Joints[JointType.SpineBase].TrackingState != TrackingState.Tracked)
            {
                return GesturePartResult.Undetermined;
            }

            if (shoulderPosition.Y < FallDownOnFloorSegment1.lyingMaxBodyHeight && corePosition.Y < FallDownOnFloorSegment1.lyingMaxBodyHeight && headPosition.Y < FallDownOnFloorSegment1.originKneeHeight)
            {
                return GesturePartResult.Succeeded;
            }
            else if (shoulderPosition.Y > FallDownOnFloorSegment1.lyingMaxBodyHeight && corePosition.Y > FallDownOnFloorSegment1.lyingMaxBodyHeight
                && (shoulderPosition.Y - FallDownOnFloorSegment1.originFooterHeight) < 0.6 * FallDownOnFloorSegment1.originBodyLength)
            {
                return GesturePartResult.Undetermined;
            }

            return GesturePartResult.Failed;
        }
    }
}
