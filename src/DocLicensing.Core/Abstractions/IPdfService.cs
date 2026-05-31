using DocLicensing.Core.Models;

namespace DocLicensing.Core.Abstractions;

public interface IPdfService
{
    byte[] GenerateLicensedPdf(Document document, string licensedTo);
}
