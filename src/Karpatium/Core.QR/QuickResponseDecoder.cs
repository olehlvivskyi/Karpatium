using SkiaSharp;
using ZXing;
using ZXing.SkiaSharp;

namespace Karpatium.Core.QR;

/// <summary>
/// Provides functionality for decoding QR codes from image files.
/// </summary>
public static class QuickResponseDecoder
{
    /// <summary>
    /// Decodes the content of a QR code from an image file located at the specified path.
    /// </summary>
    /// <param name="imagePath">The file path of the image containing the QR code to be decoded.</param>
    /// <returns>
    /// The decoded content of the QR code as a string, or null if decoding fails or no QR code is found in the image.
    /// </returns>
    public static string? Decode(string imagePath)
    {
        var bitmap = SKBitmap.Decode(imagePath);
        if (bitmap == null)
        {
            return null;
        }

        var reader = new BarcodeReader<SKBitmap>(source => new SKBitmapLuminanceSource(source))
        {
            AutoRotate = true,
            Options = new ZXing.Common.DecodingOptions
            {
                PossibleFormats = [BarcodeFormat.QR_CODE],
                TryHarder = true,
                TryInverted = true
            }
        };

        var result = reader.Decode(bitmap);
        return result?.Text;
    }
}