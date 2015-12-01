using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace LightBuzz.Vitruvius.Gestures
{
    class HandCircleLeftBehindSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            // left hand left of elbow, elbow left of shoulder
            if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ElbowLeft].Position.X && body.Joints[JointType.ElbowLeft].Position.X < body.Joints[JointType.ShoulderLeft].Position.X)
            {
                // left hand behind of elbow

                CameraSpacePoint leftShoulderPosition = body.Joints[JointType.ShoulderLeft].Position;
                CameraSpacePoint rightShoulderPosition = body.Joints[JointType.ShoulderRight].Position;
                double shoulderWidth = leftShoulderPosition.lengthTo(rightShoulderPosition);
                if (body.Joints[JointType.HandLeft].Position.Z > body.Joints[JointType.ElbowLeft].Position.Z + shoulderWidth * 0.05)
                {
                    return GesturePartResult.Succeeded;
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }

    class HandCircleLeftAboveSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            // left hand left of elbow, elbow left of shoulder
            if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ElbowLeft].Position.X && body.Joints[JointType.ElbowLeft].Position.X < body.Joints[JointType.ShoulderLeft].Position.X)
            {
                // Left hand above of elbow
                CameraSpacePoint leftShoulderPosition = body.Joints[JointType.ShoulderLeft].Position;
                CameraSpacePoint rightShoulderPosition = body.Joints[JointType.ShoulderRight].Position;
                double shoulderWidth = leftShoulderPosition.lengthTo(rightShoulderPosition);
                if (body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.ElbowLeft].Position.Y + 0.05 * shoulderWidth)
                {
                    return GesturePartResult.Succeeded;
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }

    class HandCircleLeftFrontSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            // left hand left of elbow, elbow left of shoulder
            if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ElbowLeft].Position.X && body.Joints[JointType.ElbowLeft].Position.X < body.Joints[JointType.ShoulderLeft].Position.X)
            {
                // left hand front of elbow
                CameraSpacePoint leftShoulderPosition = body.Joints[JointType.ShoulderLeft].Position;
                CameraSpacePoint rightShoulderPosition = body.Joints[JointType.ShoulderRight].Position;
                double shoulderWidth = leftShoulderPosition.lengthTo(rightShoulderPosition);
                if (body.Joints[JointType.HandLeft].Position.Z < body.Joints[JointType.ElbowLeft].Position.Z - 0.05 * shoulderWidth)
                {
                    return GesturePartResult.Succeeded;
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }

    class HandCircleLeftBlowSegment : IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public GesturePartResult Update(Body body)
        {
            // left hand left of elbow, elbow left of shoulder
            if (body.Joints[JointType.HandLeft].Position.X < body.Joints[JointType.ElbowLeft].Position.X && body.Joints[JointType.ElbowLeft].Position.X < body.Joints[JointType.ShoulderLeft].Position.X)
            {
                // left hand blow of elbow
                CameraSpacePoint leftShoulderPosition = body.Joints[JointType.ShoulderLeft].Position;
                CameraSpacePoint rightShoulderPosition = body.Joints[JointType.ShoulderRight].Position;
                double shoulderWidth = leftShoulderPosition.lengthTo(rightShoulderPosition);
                if (body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.ElbowLeft].Position.Y - 0.05 * shoulderWidth)
                {
                    return GesturePartResult.Succeeded;
                }
                return GesturePartResult.Undetermined;
            }
            return GesturePartResult.Failed;
        }
    }
}
