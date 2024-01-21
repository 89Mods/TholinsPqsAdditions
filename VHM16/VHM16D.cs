using System;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;
using UnityEngine;

namespace TholinsPQSAdditions.VHM16
{
    [RequireConfigType(Kopernicus.ConfigParser.Enumerations.ConfigType.Node)]
    class VHM16D : ModLoader<PQSMod_VHM16D>
    {
        // The map texture for the planet
        [ParserTarget("mapLo")]
        public MapSOParserGreyScale<MapSO> HeightMapLo
        {
            get { return Mod.heightMap; }
            set { Mod.heightMap = value; }
        }

        [ParserTarget("mapHi")]
        public MapSOParserGreyScale<MapSO> HeightMapHi
        {
            get { return Mod.heightMap2; }
            set { Mod.heightMap2 = value; }
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
