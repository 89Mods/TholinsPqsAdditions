using System;

namespace TholinsPQSAdditions.VertexHeightOffset
{
    class PQSMod_VertexHeightOffset : PQSMod
    {
        public Double offset;
        public Boolean scaleOffsetByRadius;

        public override void OnSetup()
        {
            base.OnSetup();
            if(scaleOffsetByRadius)
            {
                offset *= sphere.radius;
            }
        }

        public override void OnVertexBuildHeight(PQS.VertexBuildData data)
        {
            data.vertHeight += offset;
        }
    }
}
