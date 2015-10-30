using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBuzz.Vitruvius
{
    public static class CameraSpacePointExtension
    {
        public static double lengthTo(this CameraSpacePoint fromPoint, CameraSpacePoint toPoint)
        {
            return Math.Sqrt((fromPoint.X - toPoint.X) * (fromPoint.X - toPoint.X) + (fromPoint.Y - toPoint.Y) * (fromPoint.Y - toPoint.Y) + (fromPoint.Z - toPoint.Z) * (fromPoint.Z - toPoint.Z));
        }
    }
}
