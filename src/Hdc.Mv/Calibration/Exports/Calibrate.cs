//
//  File generated by HDevelop for HALCON/DOTNET (C#) Version 11.0
//

using HalconDotNet;

public partial class HDevelopExport
{
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
#endif

  // Procedures 
  public void Calibrate (HObject ho_OriginalImage, out HObject ho_CalibratedImage, 
      HTuple hv_CameraParamsFileName, HTuple hv_CameraPoseFileName, string interpolation, out HTuple hv_LengthPerPixelX, 
      out HTuple hv_LengthPerPixelY)
  {



    // Local control variables 

    HTuple hv_CameraParams = null, hv_CameraPose = null;
    HTuple hv_Width = null, hv_Height = null, hv_OriginX = null;
    HTuple hv_OriginY = null, hv_XOffsetX = null, hv_XOffsetY = null;
    HTuple hv_YOffsetX = null, hv_YOffsetY = null, hv_OriginXActual = null;
    HTuple hv_OriginYActual = null, hv_XOffsetXActual = null;
    HTuple hv_XOffsetYActual = null, hv_YOffsetXActual = null;
    HTuple hv_YOffsetYActual = null, hv_OffsetX = null, hv_OffsetY = null;
    HTuple hv_cut = null, hv_CameraPoseR = null, hv_PoseNewOrigin = null;

    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_CalibratedImage);

    HOperatorSet.ReadCamPar(hv_CameraParamsFileName, out hv_CameraParams);

    HOperatorSet.ReadPose(hv_CameraPoseFileName, out hv_CameraPose);

    HOperatorSet.GetImageSize(ho_OriginalImage, out hv_Width, out hv_Height);

    hv_OriginX = 100;
    hv_OriginY = 100;
    hv_XOffsetX = 101;
    hv_XOffsetY = 100;
    hv_YOffsetX = 100;
    hv_YOffsetY = 101;

    hv_OriginXActual = 0;
    hv_OriginYActual = 0;
    hv_XOffsetXActual = 0;
    hv_XOffsetYActual = 0;
    hv_YOffsetXActual = 0;
    hv_YOffsetYActual = 0;

    HOperatorSet.ImagePointsToWorldPlane(hv_CameraParams, hv_CameraPose, hv_OriginY, 
        hv_OriginX, 1, out hv_OriginXActual, out hv_OriginYActual);
    HOperatorSet.ImagePointsToWorldPlane(hv_CameraParams, hv_CameraPose, hv_XOffsetY, 
        hv_XOffsetX, 1, out hv_XOffsetXActual, out hv_XOffsetYActual);
    HOperatorSet.ImagePointsToWorldPlane(hv_CameraParams, hv_CameraPose, hv_YOffsetY, 
        hv_YOffsetX, 1, out hv_YOffsetXActual, out hv_YOffsetYActual);
    HOperatorSet.DistancePp(hv_OriginYActual, hv_OriginXActual, hv_XOffsetYActual, 
        hv_XOffsetXActual, out hv_LengthPerPixelX);
    HOperatorSet.DistancePp(hv_OriginYActual, hv_OriginXActual, hv_YOffsetYActual, 
        hv_YOffsetXActual, out hv_LengthPerPixelY);

    hv_OffsetX = (hv_Width / 2.0 - 150.0) * hv_LengthPerPixelX;
    hv_OffsetY = (hv_Height/2.0 -40.0)*hv_LengthPerPixelY;

    hv_cut = ((hv_CameraPose.TupleSelect(5))).TupleInt();
    hv_cut = 90;
    hv_CameraPoseR = hv_CameraPose.Clone();
    if (hv_CameraPoseR == null)
      hv_CameraPoseR = new HTuple();
    hv_CameraPoseR[5] = (hv_CameraPose.TupleSelect(5))-hv_cut;

    HOperatorSet.SetOriginPose(hv_CameraPoseR, -hv_OffsetX, -hv_OffsetY, 0, out hv_PoseNewOrigin);

    //set_origin_pose (PoseNewOrigin, 0, 0, 0, PoseNewOrigin1)

    ho_CalibratedImage.Dispose();
    HOperatorSet.ImageToWorldPlane(ho_OriginalImage, out ho_CalibratedImage, hv_CameraParams,
        hv_PoseNewOrigin, hv_Width, hv_Height, hv_LengthPerPixelY, interpolation); //bilinear, nearest_neighbor


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
