using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace LightBuzz.Vitruvius.Gestures
{
    class ForwardRightSegment1 : IGestureSegment
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
                    CameraSpacePoint spineShoulderPosition = body.Joints[JointType.SpineShoulder].Position;
                    CameraSpacePoint spineBasePosition = body.Joints[JointType.SpineBase].Position;
                    double bodyHeight = spineShoulderPosition.lengthTo(spineBasePosition);
                    if (body.Joints[JointType.HandRight].Position.Z > body.Joints[JointType.ShoulderRight].Position.Z - bodyHeight * 0.5)
                    {
                        return GesturePartResult.Succeeded;
                    }
                }
            }
            return GesturePartResult.Failed;
        }
    }

    class ForwardRightSegment2 : IGestureSegment
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
                if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ShoulderRight].Position.X)
                {
                    CameraSpacePoint spineShoulderPosition = body.Joints[JointType.SpineShoulder].Position;
                    CameraSpacePoint spineBasePosition = body.Joints[JointType.SpineBase].Position;
                    double bodyHeight = spineShoulderPosition.lengthTo(spineBasePosition);
                    if (body.Joints[JointType.HandRight].Position.Z < body.Joints[JointType.ShoulderRight].Position.Z - bodyHeight * 0.8)
                    {
                        return GesturePartResult.Succeeded;
                    }
                    return GesturePartResult.Undetermined;
                }
            }
            return GesturePartResult.Failed;
        }
    }
}
