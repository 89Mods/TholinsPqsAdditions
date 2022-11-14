using System;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration.ModLoader;

namespace TholinsPQSAdditions.VertexHeightAbsolute
{
    [RequireConfigType(Kopernicus.ConfigParser.Enumerations.ConfigType.Node)]
    class VertexHeightAbsolute : ModLoader<PQSMod_VertexHeightAbsolute>
    {

    }
}
