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
    class PQSMod_VHM16D : PQSMod_VertexHeightMap
    {
        public MapSO heightMap2;

        public override void OnVertexBuildHeight(PQS.VertexBuildData data)
        {
            // Apply it
            data.vertHeight += heightMapOffset + heightMapDeformity * SampleHeightmap16(data.u, data.v, heightMap);
        }

        private float SingleSample(Int32 x, Int32 y)
        {
            int a = heightMap.GetPixelColor32(x, y).r;
            a |= heightMap2.GetPixelColor32(x, y).r << 8;
            return (float)a / 65535.0f;
        }

        public float SampleHeightmap16(Double u, Double v, MapSO heightMap)
        {
            if (heightMap == null || !heightMap.IsCompiled) return 0;
            BilinearCoords coords = PQSMod_VHM16.ConstructBilinearCoords(u, v, heightMap);
            return Mathf.Lerp(
                Mathf.Lerp(
                    SingleSample(coords.xFloor, coords.yFloor),
                    SingleSample(coords.xCeiling, coords.yFloor),
                    coords.u),
                Mathf.Lerp(
                    SingleSample(coords.xFloor, coords.yCeiling),
                    SingleSample(coords.xCeiling, coords.yCeiling),
                    coords.u),
                coords.v);
        }
    }
}
