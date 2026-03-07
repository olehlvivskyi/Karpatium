using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace Karpatium.Core.PDF;

/// <summary>
/// Provides an implementation of the <see cref="IPdfExtractorStrategy"/> interface for extracting
/// text and images from PDF files using the PdfPig library.
/// </summary>
public class PdfPigExtractorStrategy : IPdfExtractorStrategy
{
    /// <summary>
    /// Extracts image data from a PDF file at the specified file path.
    /// </summary>
    /// <param name="path">The file path to the PDF document from which images will be extracted.</param>
    /// <returns>A dictionary where the key represents the page number, and the value is a list of byte arrays, each representing raw image data extracted from that page.</returns>
    public Dictionary<int, List<byte[]>> GetImages(string path)
    {
        var result = new Dictionary<int, List<byte[]>>();

        using var document = PdfDocument.Open(path);
        foreach (Page page in document.GetPages())
        {
            var images = page.GetImages()
                .Select(img => img.RawBytes.ToArray())
                .ToList();

            if (images.Count > 0)
            {
                result[page.Number] = images;
            }
        }

        return result;
    }

    /// <summary>
    /// Extracts text content from a PDF file at the specified file path.
    /// </summary>
    /// <param name="path">The file path to the PDF document from which text will be extracted.</param>
    /// <returns>A dictionary where the key represents the page number, and the value is the text content extracted from that page.</returns>
    public Dictionary<int, string> GetText(string path)
    {
        var result = new Dictionary<int, string>();

        using var document = PdfDocument.Open(path);
        foreach (Page page in document.GetPages())
        {
            result[page.Number] = ContentOrderTextExtractor.GetText(page);
        }

        return result;
    }
}