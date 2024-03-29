﻿using System;
using UnityEngine;

namespace TholinsPQSAdditions.VHM16
{
    /// <summary>
    /// A heightmap PQSMod that can parse encoded 16bpp textures
    /// </summary>
    public class PQSMod_VHM16MitchellNetravali : PQSMod_VertexHeightMap
    {
        public override void OnVertexBuildHeight(PQS.VertexBuildData data)
        {
            // Apply it
            data.vertHeight += heightMapOffset + heightMapDeformity * InterpolateHeights(data.u, data.v);
        }

        public static float SingleSample(Int32 x, Int32 y, MapSO heightMap, bool bits24)
        {
            // Get the Color, not the Float-Value from the Map
            Color32 c = heightMap.GetPixelColor32(x, y);

            // Get the height data from the terrain
            float height = 0;
            if (bits24)
            {
                height = (float)((int)c.b | ((int)c.g << 8) | ((int)c.r << 16)) / (float)0x00FFFFFF;
            }
            else
            {
                height = (float)((int)c.b | ((int)c.g << 8)) / (float)0xFFFF;
            }

            return height;
        }

        /*
         * Code snippet from https://github.com/pkmniako/Kopernicus_VertexMitchellNetravaliHeightMap/blob/0013253f88a7634e01f149c462ffbdb6a23bd113/VertexMitchellNetravaliHeightMap/PQSMod_VertexMitchellNetravaliHeightMap.cs
         */

        /// <summary> B value for the Mitchell-Netravali Filter </summary>
        public double B = 1.0f;
        /// <summary> C value for the Mitchell-Netravali Filter </summary>
        public double C = 0.0f;
        /// <summary> Have the constants been precalculated already? </summary>
        protected bool hasConstantsPrecalculated = false;

        /*
				Constants
		*/
        private double _n6BnC;
        private double _n32BnC2;
        private double _32BCn2;
        private double _6BC;
        private double _2B2C;
        private double _2BCn3;
        private double _n52Bn2C3;
        private double _n2BnC;
        private double _2BC;
        private double _6B;
        private double _n3B1;
        private double iWidth;
        private double iHeight;


        private double[] PY = new double[4];
        private double[] PX = new double[4];

        /// <summary> Precalculates constants that are used at OnVertexBuildHeight </summary>
        public void PrecalculateConstants()
        {
            _n6BnC = (-1 / 6.0) * B - C;
            _n32BnC2 = (-3 / 2.0) * B - C + 2;
            _32BCn2 = -_n32BnC2;
            _6BC = -_n6BnC;
            _2B2C = 0.5f * B + 2 * C;
            _2BCn3 = 2 * B + C - 3;
            _n52Bn2C3 = (-5 / 2.0) * B - 2 * C + 3;
            _n2BnC = -0.5f * B - C;
            _2BC = -_n2BnC;
            _6B = (1 / 6.0) * B;
            _n3B1 = (-1 / 3.0) * B + 1;

            iWidth = 1.0 / heightMap.Width;
            iHeight = 1.0 / heightMap.Height;

            hasConstantsPrecalculated = true;

            Debug.Log("[VertexMitchellNetravaliHeightMap] New Constant values for B = " + B + " and C = " + C);
            Debug.Log("[VertexMitchellNetravaliHeightMap] Map's Size: " + heightMap.Width + "x" + heightMap.Height);
        }

        public double RunMitchellNetravali(double P0, double P1, double P2, double P3, double d)
        {
            double output = (_n6BnC * P0 + _n32BnC2 * P1 + _32BCn2 * P2 + _6BC * P3) * d * d * d
                            + (_2B2C * P0 + _2BCn3 * P1 + _n52Bn2C3 * P2 - C * P3) * d * d
                            + (_n2BnC * P0 + _2BC * P2) * d
                            + _6B * P0 + _n3B1 * P1 + _6B * P2;

            return output;
        }

        protected int ClampLoop(int value, int min, int max)
        {
            int d = max - min;
            return value < min ? value + d : (value >= max ? value - d : value);
        }

        protected int Clamp(int value, int min, int max)
        {
            return value < min ? min : (value >= max ? max - 1 : value);
        }

        /*
         * Function modified to use the SingleSample method defined here instead of GetPixelFloat
         * 
         *  - Tholin
         */
        public double InterpolateHeights(double u, double v)
        {
            if (!hasConstantsPrecalculated)
            {
                PrecalculateConstants();
            }

            //Calculate necesary variables
            int x0 = (int)Math.Floor(u * heightMap.Width);
            int y0 = (int)Math.Floor(v * heightMap.Height);
            double u0 = x0 / (double)heightMap.Width;
            double v0 = y0 / (double)heightMap.Height;

            double uD = (u - u0) * heightMap.Width;
            double vD = (v - v0) * heightMap.Height;

            //Calculate height (Interpolate)

            for (int j = -1; j < 3; j++)
            {
                Int32 y = Clamp(y0 + j, 0, heightMap.Height);
                for (int i = -1; i < 3; i++)
                {
                    Int32 x = ClampLoop(x0 + i, 0, heightMap.Width);
                    PX[i + 1] = SingleSample(x, y, this.heightMap, false);
                }
                PY[j + 1] = RunMitchellNetravali(PX[0], PX[1], PX[2], PX[3], uD);
                //PY[j + 1] = PX[0];
            }

            double output = RunMitchellNetravali(PY[0], PY[1], PY[2], PY[3], vD);

            //return heightMap.GetPixelFloat(u, v);

            return output;
        }

        public override void OnSetup()
        {
            base.OnSetup();
            PrecalculateConstants();
        }

        /*
         * End code snippet
         */
    }
}
