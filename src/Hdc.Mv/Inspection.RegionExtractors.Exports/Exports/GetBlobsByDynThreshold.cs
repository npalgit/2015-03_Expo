//
//  File generated by HDevelop for HALCON/DOTNET (C#) Version 12.0
//

using HalconDotNet;

public partial class HDevelopExport
{
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
#endif

  // Procedures 
  public void GetBlobsByDynThreshold (HObject ho_Image, out HObject ho_FoundRegion, 
      HTuple hv_MeanMaskWidth, HTuple hv_MeanMaskHeight, HTuple hv_DynOffset, HTuple hv_DynLightDark)
  {




    // Local iconic variables 

    HObject ho_ImageMean, ho_RegionDynThresh2;
    HObject ho_ConnectedRegions;
    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_FoundRegion);
    HOperatorSet.GenEmptyObj(out ho_ImageMean);
    HOperatorSet.GenEmptyObj(out ho_RegionDynThresh2);
    HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
    ho_ImageMean.Dispose();
    HOperatorSet.MeanImage(ho_Image, out ho_ImageMean, hv_MeanMaskHeight, hv_MeanMaskWidth);
    ho_RegionDynThresh2.Dispose();
    HOperatorSet.DynThreshold(ho_Image, ho_ImageMean, out ho_RegionDynThresh2, hv_DynOffset, 
        hv_DynLightDark);

    ho_ConnectedRegions.Dispose();
    HOperatorSet.Connection(ho_RegionDynThresh2, out ho_ConnectedRegions);
    ho_FoundRegion.Dispose();
    HOperatorSet.MoveRegion(ho_ConnectedRegions, out ho_FoundRegion, 0, 0);
    ho_ImageMean.Dispose();
    ho_RegionDynThresh2.Dispose();
    ho_ConnectedRegions.Dispose();

    return;
  }


}
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
public class HDevelopExportApp
{
  static void Main(string[] args)
  {
    new HDevelopExport();
  }
}
#endif
