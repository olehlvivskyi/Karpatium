namespace Karpatium.Core.PDF;

/// <summary>
/// Provides functionality to extract textual and graphical content from PDF files
/// using a specified strategy for processing.
/// </summary>
public class PdfExtractor(IPdfExtractorStrategy strategy)
{
    /// <summary>
    /// Extracts text content from a PDF file at the specified path and returns it as a dictionary.
    /// </summary>
    /// <param name="path">The file path to the PDF document from which text will be extracted.</param>
    /// <returns>A dictionary where the key represents the page number, and the value is the text content extracted from that page.</returns>
    public Dictionary<int, string> GetText(string path) => strategy.GetText(path);

    /// <summary>
    /// Extracts images from a PDF file at the specified path and returns them as a dictionary.
    /// </summary>
    /// <param name="path">The file path to the PDF document from which images will be extracted.</param>
    /// <returns>A dictionary where the key represents the page number, and the value is a list of byte arrays representing images extracted from that page.</returns>
    public Dictionary<int, List<byte[]>> GetImages(string path) => strategy.GetImages(path);
}