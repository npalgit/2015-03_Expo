﻿using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class AntennaAroundOfS1423ARegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            HObject regionHObject;
            HDevelopExport.Singletone.GetRegionOfAntennaAroundOfS1423A(image, out regionHObject,
                LightDark.ToHalconString(), WidthMin, WidthMax, HeightMin, HeightMax, DilationWidth, DilationHeight);
            return new HRegion(regionHObject);
        }

        public LightDark LightDark { get; set; }
        public double WidthMin { get; set; }
        public double WidthMax { get; set; }
        public double HeightMin { get; set; }
        public double HeightMax { get; set; }
        public int DilationWidth { get; set; }
        public int DilationHeight { get; set; }
    }
}