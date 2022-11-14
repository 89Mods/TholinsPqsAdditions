using System;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;
using UnityEngine;

namespace TholinsPQSAdditions.VHM16
{
    [RequireConfigType(Kopernicus.ConfigParser.Enumerations.ConfigType.Node)]
    class VHM16 : ModLoader<PQSMod_VHM16>
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
    }
}
