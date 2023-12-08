using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.StorageAccount
{
    public interface IStorageAccountUtility
    {
        Task<Either<HttpResponseMessage, Errors>> DownloadDocument(string fileId, string fileName);
        Task<Either<HttpResponseMessage, Errors>> DownloadDocumentTemplate(string fileId, string fileName);
        string GetPublicAccountStorageDeleteFileToken(string fileId);
        string GetPublicAccountStorageDownloadFileToken(string fileId);
        string GetPublicAccountStorageUploadFileToken(string fileId);       
        Task<bool> UploadDocument(IFormFile file, string fileId);
        Task<bool> UploadDocumentTemplate(IFormFile file, string fileId);
    }
}