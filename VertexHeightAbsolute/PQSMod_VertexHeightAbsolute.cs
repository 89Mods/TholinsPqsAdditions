using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TholinsPQSAdditions.VertexHeightAbsolute
{
    class PQSMod_VertexHeightAbsolute : PQSMod
    {
        public override void OnVertexBuildHeight(PQS.VertexBuildData data)
        {
            data.vertHeight = Math.Abs(data.vertHeight);
        }
    }
}
