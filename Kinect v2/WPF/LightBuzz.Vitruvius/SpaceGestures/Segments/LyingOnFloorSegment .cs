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

            // Console.WriteLine("X: {0}, Y: {1}， Z: {2}, W: {3}", spaceBody.floorPlane.X, spaceBody.floorPlane.Y, spaceBody.floorPlane.Z, spaceBody.floorPlane.W);

            if (spaceBody.floorPlane.W != 0)
            {
                return GesturePartResult.Undetermined;
            }

            Body body = spaceBody.body;

            CameraSpacePoint headPosition = body.Joints[JointType.Head].Position;
            CameraSpacePoint corePosition = body.Joints[JointType.SpineBase].Position;
            CameraSpacePoint lFooterPosition = body.Joints[JointType.FootLeft].Position;

            // Console.WriteLine("headPosition: {0} {1} {2}", headPosition.X, headPosition.Y, headPosition.Z);
            double headHeight = spaceBody.floorPlane.Length(headPosition);
            double coreHeight = spaceBody.floorPlane.Length(corePosition);
            double lFooterHeight = spaceBody.floorPlane.Length(lFooterPosition);

            double bodyHeight = headPosition.lengthTo(lFooterPosition);

            if (bodyHeight == 0)
            {
                return GesturePartResult.Failed;
            }

            //Console.WriteLine("body height: " + bodyHeight.ToString());
            //Console.WriteLine("head height: " + headHeight.ToString());
            //Console.WriteLine("core height: " + coreHeight.ToString());
            //Console.WriteLine("left footer height: " + lFooterHeight.ToString());

            double successHeight = 0.3 * bodyHeight;
            double undeterminedHeight = 0.5 * bodyHeight;
            if (headHeight < successHeight && coreHeight < successHeight && lFooterHeight < successHeight)
            {
                return GesturePartResult.Succeeded;
            }
            else if (headHeight < undeterminedHeight && coreHeight < undeterminedHeight && lFooterHeight < undeterminedHeight)
            {
                return GesturePartResult.Undetermined;
            }

            return GesturePartResult.Failed;
        }
    }
}