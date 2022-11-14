using System;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;
using UnityEngine;

namespace TholinsPQSAdditions.VHM16
{
    [RequireConfigType(Kopernicus.ConfigParser.Enumerations.ConfigType.Node)]
    public class VHM16MitchellNetravali : ModLoader<PQSMod_VHM16MitchellNetravali>
    {
        // The map texture for the planet
        [ParserTarget("map")]
        public MapSOParserRGB<MapSO> HeightMap
        {
            get { return Mod.heightMap; }
            set { Mod.heightMap = value; }
        }

        // Height map offset
        [ParserTarget("offset")]
        public NumericParser<Double> HeightMapOffset
        {
            get { return Mod.heightMapOffset; }
            set { Mod.heightMapOffset = value; }
        }

        // Height map deformity
        [ParserTarget("deformity")]
        public NumericParser<Double> HeightMapDeformity
        {
            get { return Mod.heightMapDeformity; }
            set { Mod.heightMapDeformity = value; }
        }

        [ParserTarget("scaleDeformityByRadius")]
        public NumericParser<Boolean> ScaleDeformityByRadius
        {
            get { return Mod.scaleDeformityByRadius; }
            set { Mod.scaleDeformityByRadius = value; }
        }

        /*
         * Code snippet taken from https://github.com/pkmniako/Kopernicus_VertexMitchellNetravaliHeightMap/blob/0013253f88a7634e01f149c462ffbdb6a23bd113/VertexMitchellNetravaliHeightMap/VertexMitchellNetravaliHeightMap.cs
         */

        [ParserTarget("B")]
        public NumericParser<Double> B
        {
            get { return Mod.B; }
            set { Mod.B = value; }
        }

        [ParserTarget("C")]
        public NumericParser<Double> C
        {
            get { return Mod.C; }
            set { Mod.C = value; }
        }

        /*
         * End code snippet
         */
    }
}
