using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class BinaryThresholdRect2RegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            if(!(LightDark==LightDark.Light || LightDark==LightDark.Dark))
                throw new InvalidOperationException("BinaryThresholdRegionExtractor.LightDark must be Light or Dark. Now is " + LightDark);

            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionByBinaryThresholdRect2(image,
                out foundRegionObject,
                MeanMaskWidth,
                MeanMaskHeight,
                ScaleAdd,
                LightDark.ToHalconString(),
                AreaMin,
                AreaMax,
                OpeningWidth,
                OpeningHeight,
                ClosingWidth,
                ClosingHeight,
                ErosionWidth,
                ErosionHeight,
                DilationWidth,
                DilationHeight);

            var hRegion = new HRegion(foundRegionObject);
            return hRegion;
        }

        public int MeanMaskWidth { get; set; }
        public int MeanMaskHeight { get; set; }
        public double ScaleAdd { get; set; }
        public LightDark LightDark { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
        public int OpeningWidth { get; set; }
        public int OpeningHeight { get; set; }
        public int ClosingWidth { get; set; }
        public int ClosingHeight { get; set; }
        public int ErosionWidth { get; set; }
        public int ErosionHeight { get; set; }
        public int DilationWidth { get; set; }
        public int DilationHeight { get; set; }
    }
}