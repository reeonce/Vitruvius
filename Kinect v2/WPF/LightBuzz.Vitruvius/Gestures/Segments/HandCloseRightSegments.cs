using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace LightBuzz.Vitruvius.Gestures
{
    class HandCloseRightSegment : IGestureSegment
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
                    double elbowAngle = body.Joints[JointType.ElbowRight].Position.Angle(body.Joints[JointType.HandRight].Position, body.Joints[JointType.ShoulderRight].Position);
                    double shoulderAngle = body.Joints[JointType.ShoulderRight].Position.Angle(body.Joints[JointType.ElbowRight].Position, body.Joints[JointType.ShoulderLeft].Position);
                    if (elbowAngle < 95 && shoulderAngle > 145 && body.Joints[JointType.HandRight].Position.Z > body.Joints[JointType.ShoulderRight].Position.Z - bodyHeight * 0.3)
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
