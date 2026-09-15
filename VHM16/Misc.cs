using System;
using UnityEngine;
using Kopernicus.Components;

namespace TholinsPQSAdditions.VHM16
{
	public static class Misc
	{
		public static Color32 kopernicus_changes_workaround(Int32 x, Int32 y, MapSO map)
		{
			if(map == null) return new Color32(0,0,0,0);
			if(map is KopernicusMapSO)
			{
				KopernicusMapSO mapK = (KopernicusMapSO)map;
				if(mapK.Texture == null) return new Color32(0,0,0,0);
				return mapK.Texture.GetPixel32(x, y);
			}
			return map.GetPixelColor32(x, y);
		}
	}
}
