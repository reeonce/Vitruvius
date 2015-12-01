using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace LightBuzz.Vitruvius.Gestures
{
    class HandUpRightSegment1 : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            if (body.HandRightState == HandState.Closed)
            {
                // right hand on top right of shoulder
                if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ShoulderRight].Position.X && body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.ElbowRight].Position.Y)
                {
                    if (body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.ShoulderRight].Position.Y && body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.Head].Position.Y)
                    {
                        return GesturePartResult.Succeeded;
                    }
                }
            }
            return GesturePartResult.Failed;
        }
    }

    class HandUpRightSegment2 : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ShoulderRight].Position.X)
            {
                // right hand on top right of shoulder
                if (body.HandRightState == HandState.Closed)
                {
                    CameraSpacePoint spineShoulderPosition = body.Joints[JointType.SpineShoulder].Position;
                    CameraSpacePoint spineBasePosition = body.Joints[JointType.SpineBase].Position;
                    double bodyHeight = spineShoulderPosition.Y - spineBasePosition.Y;
                    if (body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.ShoulderRight].Position.Y + bodyHeight * 0.8)
                    {
                        return GesturePartResult.Succeeded;
                    }
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }
}
