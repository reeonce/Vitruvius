using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace LightBuzz.Vitruvius
{
    public class FallDownOnFloorSegment1 : ISpaceGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="spaceBody">The spaceBody.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(SpaceBody spaceBody)
        {
            // Console.WriteLine("X: {0}, Y: {1}， Z: {2}, W: {3}", spaceBody.floorPlane.X, spaceBody.floorPlane.Y, spaceBody.floorPlane.Z, spaceBody.floorPlane.W);

            //if (spaceBody.floorPlane.W != 0)
            //{
            //    return GesturePartResult.Undetermined;
            //}

            Body body = spaceBody.body;

            if ((body.Joints[JointType.FootRight].TrackingState != TrackingState.Tracked && body.Joints[JointType.KneeRight].TrackingState != TrackingState.Tracked)
                || (body.Joints[JointType.FootLeft].TrackingState != TrackingState.Tracked && body.Joints[JointType.KneeLeft].TrackingState == TrackingState.Tracked))
            {
                return GesturePartResult.Failed;
            }

            CameraSpacePoint shoulderPosition = body.Joints[JointType.SpineShoulder].Position;
            CameraSpacePoint corePosition = body.Joints[JointType.SpineBase].Position;
            CameraSpacePoint footerPosition;
            CameraSpacePoint kneePosition;

            if (body.Joints[JointType.FootRight].TrackingState == TrackingState.Tracked && body.Joints[JointType.FootRight].Position.Y < body.Joints[JointType.KneeRight].Position.Y)
            {
                footerPosition = body.Joints[JointType.FootRight].Position;
            }
            else
            {
                footerPosition = body.Joints[JointType.FootLeft].Position;
            }

            if (body.Joints[JointType.KneeLeft].TrackingState == TrackingState.Tracked && body.Joints[JointType.FootLeft].Position.Y < body.Joints[JointType.KneeLeft].Position.Y)
            {
                kneePosition = body.Joints[JointType.KneeLeft].Position;
            }
            else
            {
                kneePosition = body.Joints[JointType.KneeRight].Position;
            }

            double bodyHeight = shoulderPosition.lengthTo(footerPosition);
            double bodyAngle = corePosition.Angle(shoulderPosition, footerPosition);

            if (bodyAngle > 145 && (shoulderPosition.Y - footerPosition.Y) > 0.8 * bodyHeight)
            {
                originBodyLength = bodyHeight;
                originKneeHeight = kneePosition.Y;
                originFooterHeight = footerPosition.Y;
                lyingMaxBodyHeight = (originKneeHeight + originFooterHeight) / 2;
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }

        public static double originBodyLength = 0;
        public static double originKneeHeight = 0;
        public static double originFooterHeight = 0;
        public static double lyingMaxBodyHeight = 0;
    }

    class FallDownOnFloorSegment2 : ISpaceGestureSegment
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

            if (body.Joints[JointType.SpineShoulder].TrackingState != TrackingState.Tracked || body.Joints[JointType.SpineBase].TrackingState != TrackingState.Tracked)
            {
                return GesturePartResult.Undetermined;
            }

            if ((shoulderPosition.Y - FallDownOnFloorSegment1.originFooterHeight) < 0.6 * FallDownOnFloorSegment1.originBodyLength && shoulderPosition.Y > FallDownOnFloorSegment1.originKneeHeight)
            {
                return GesturePartResult.Succeeded;
            }
            else if ((shoulderPosition.Y - FallDownOnFloorSegment1.originFooterHeight) >= 0.6 * FallDownOnFloorSegment1.originBodyLength)
            {
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }

    class FallDownOnFloorSegment3 : ISpaceGestureSegment
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

            if (body.Joints[JointType.SpineShoulder].TrackingState != TrackingState.Tracked || body.Joints[JointType.SpineBase].TrackingState != TrackingState.Tracked)
            {
                return GesturePartResult.Undetermined;
            }

            if (shoulderPosition.Y < FallDownOnFloorSegment1.lyingMaxBodyHeight && corePosition.Y < FallDownOnFloorSegment1.lyingMaxBodyHeight)
            {
                lyingCorePosition = corePosition;
                return GesturePartResult.Succeeded;
            }
            else if (shoulderPosition.Y > FallDownOnFloorSegment1.lyingMaxBodyHeight && (shoulderPosition.Y - FallDownOnFloorSegment1.originFooterHeight) < 0.6 * FallDownOnFloorSegment1.originBodyLength)
            {
                return GesturePartResult.Undetermined;
            }

            return GesturePartResult.Failed;
        }

        public static CameraSpacePoint lyingCorePosition = new CameraSpacePoint();
    }
}