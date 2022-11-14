using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TholinsPQSAdditions.VHM16
{
    /// <summary>
    /// A heightmap PQSMod that can parse encoded 16bpp textures
    /// </summary>
    class PQSMod_VHM16 : PQSMod_VertexHeightMap
    {
        public override void OnVertexBuildHeight(PQS.VertexBuildData data)
        {
            // Apply it
            data.vertHeight += heightMapOffset + heightMapDeformity * SampleHeightmap16(data.u, data.v, heightMap, false);
        }

        public static float SampleHeightmap16(Double u, Double v, MapSO heightMap, bool bits24)
        {
            if (heightMap == null || !heightMap.IsCompiled) return 0;
            BilinearCoords coords = ConstructBilinearCoords(u, v, heightMap);
            return Mathf.Lerp(
                Mathf.Lerp(
                    PQSMod_VHM16MitchellNetravali.SingleSample(coords.xFloor, coords.yFloor, heightMap, bits24),
                    PQSMod_VHM16MitchellNetravali.SingleSample(coords.xCeiling, coords.yFloor, heightMap, bits24),
                    coords.u),
                Mathf.Lerp(
                    PQSMod_VHM16MitchellNetravali.SingleSample(coords.xFloor, coords.yCeiling, heightMap, bits24),
                    PQSMod_VHM16MitchellNetravali.SingleSample(coords.xCeiling, coords.yCeiling, heightMap, bits24),
                    coords.u),
                coords.v);
        }

        // Function taken from https://github.com/Kopernicus/pqsmods-standalone/blob/master/KSP/MapSO.cs L340
        public static BilinearCoords ConstructBilinearCoords(Double x, Double y, MapSO heightMap)
        {
            // Create the struct
            BilinearCoords coords = new BilinearCoords();

            // Floor
            x = x - Math.Truncate(x);
            y = y - Math.Truncate(y);
            if (x < 0) x = 1.0 + x;
            if (y < 0) y = -y;

            // X to U
            coords.x = x * heightMap.Width;
            if (coords.x >= heightMap.Width) coords.x -= heightMap.Width;
            coords.xFloor = (Int32)Math.Floor(coords.x);
            coords.xCeiling = (Int32)Math.Ceiling(coords.x);
            coords.u = (Single)(coords.x - Math.Truncate(coords.x));
            if (coords.xCeiling >= heightMap.Width) coords.xCeiling -= heightMap.Width;

            // Y to V
            coords.y = y * heightMap.Height;
            if (coords.y >= heightMap.Height) coords.y = heightMap.Height - 1 - (coords.y - heightMap.Height);
            coords.yFloor = (Int32)Math.Floor(coords.y);
            coords.yCeiling = (Int32)Math.Ceiling(coords.y);
            coords.v = (Single)(coords.y - Math.Truncate(coords.y));
            if (coords.yCeiling >= heightMap.Height) coords.yCeiling = heightMap.Height - 1 - (coords.yCeiling - heightMap.Height);

            // We're done
            return coords;
        }

        public struct BilinearCoords
        {
            public Double x, y;
            public Int32 xCeiling, xFloor, yCeiling, yFloor;
            public Single u, v;
        }
    }
}
