using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace LightBuzz.Vitruvius.Gestures
{
    class HandCloseLeftSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            if (body.HandLeftState == HandState.Closed)
            {
                // left hand on top left of shoulder
                if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ShoulderLeft].Position.X && body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.ElbowLeft].Position.Y)
                {
                    if (body.Joints[JointType.HandLeft].Position.X > body.Joints[JointType.ElbowLeft].Position.X)
                    {
                        CameraSpacePoint spineShoulderPosition = body.Joints[JointType.SpineShoulder].Position;
                        CameraSpacePoint spineBasePosition = body.Joints[JointType.SpineBase].Position;
                        double bodyHeight = spineShoulderPosition.lengthTo(spineBasePosition);
                        if (body.Joints[JointType.HandLeft].Position.Z > body.Joints[JointType.ShoulderLeft].Position.Z - bodyHeight * 0.3)
                        {
                            return GesturePartResult.Succeeded;
                        }
                        return GesturePartResult.Undetermined;
                    }
                }
            }
            return GesturePartResult.Failed;
        }
    }
}
