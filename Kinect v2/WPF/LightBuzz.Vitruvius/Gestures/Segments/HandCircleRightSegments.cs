using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace LightBuzz.Vitruvius.Gestures
{
    class HandCircleRightBehindSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            // right hand right of elbow, elbow right of shoulder
            if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ElbowRight].Position.X && body.Joints[JointType.ElbowRight].Position.X > body.Joints[JointType.ShoulderRight].Position.X)
            {
                // right hand behind of elbow

                CameraSpacePoint leftShoulderPosition = body.Joints[JointType.ShoulderLeft].Position;
                CameraSpacePoint rightShoulderPosition = body.Joints[JointType.ShoulderRight].Position;
                double shoulderWidth = leftShoulderPosition.lengthTo(rightShoulderPosition);
                if (body.Joints[JointType.HandRight].Position.Z > body.Joints[JointType.ElbowRight].Position.Z + shoulderWidth * 0.05)
                {
                    return GesturePartResult.Succeeded;
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }

    class HandCircleRightAboveSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            // right hand right of elbow, elbow right of shoulder
            if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ElbowRight].Position.X && body.Joints[JointType.ElbowRight].Position.X > body.Joints[JointType.ShoulderRight].Position.X)
            {
                // right hand above of elbow
                CameraSpacePoint leftShoulderPosition = body.Joints[JointType.ShoulderLeft].Position;
                CameraSpacePoint rightShoulderPosition = body.Joints[JointType.ShoulderRight].Position;
                double shoulderWidth = leftShoulderPosition.lengthTo(rightShoulderPosition);
                if (body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.ElbowRight].Position.Y + 0.05 * shoulderWidth)
                {
                    return GesturePartResult.Succeeded;
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }

    class HandCircleRightFrontSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            // right hand right of elbow, elbow right of shoulder
            if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ElbowRight].Position.X && body.Joints[JointType.ElbowRight].Position.X > body.Joints[JointType.ShoulderRight].Position.X)
            {
                // right hand front of elbow
                CameraSpacePoint leftShoulderPosition = body.Joints[JointType.ShoulderLeft].Position;
                CameraSpacePoint rightShoulderPosition = body.Joints[JointType.ShoulderRight].Position;
                double shoulderWidth = leftShoulderPosition.lengthTo(rightShoulderPosition);
                if (body.Joints[JointType.HandRight].Position.Z < body.Joints[JointType.ElbowRight].Position.Z - 0.05 * shoulderWidth)
                {
                    return GesturePartResult.Succeeded;
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }

    class HandCircleRightBlowSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            // right hand right of elbow, elbow right of shoulder
            if (body.Joints[JointType.HandRight].Position.X > body.Joints[JointType.ElbowRight].Position.X && body.Joints[JointType.ElbowRight].Position.X > body.Joints[JointType.ShoulderRight].Position.X)
            {
                // right hand blow of elbow
                CameraSpacePoint leftShoulderPosition = body.Joints[JointType.ShoulderLeft].Position;
                CameraSpacePoint rightShoulderPosition = body.Joints[JointType.ShoulderRight].Position;
                double shoulderWidth = leftShoulderPosition.lengthTo(rightShoulderPosition);
                if (body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.ElbowRight].Position.Y - 0.05 * shoulderWidth)
                {
                    return GesturePartResult.Succeeded;
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }
}
