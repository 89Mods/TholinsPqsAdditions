using System;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration.ModLoader;

namespace TholinsPQSAdditions.VertexHeightOffset
{
    [RequireConfigType(Kopernicus.ConfigParser.Enumerations.ConfigType.Node)]
    class VertexHeightOffset : ModLoader<PQSMod_VertexHeightOffset>
    {
        [ParserTarget("offset")]
        public NumericParser<Double> offset
        {
            get { return Mod.offset; }
            set { Mod.offset = value; }
        }

        [ParserTarget("scaleOffsetByRadius")]
        public NumericParser<Boolean> scaleOffsetByRadius
        {
            get { return Mod.scaleOffsetByRadius;  }
            set { Mod.scaleOffsetByRadius = value; }
        }
    }
}
