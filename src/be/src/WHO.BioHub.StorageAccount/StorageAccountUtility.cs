using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.StorageAccount
{
    public class StorageAccountUtility : IStorageAccountUtility
    {
        private readonly StorageConfiguration _storageConfiguration;

        public StorageAccountUtility(StorageConfiguration storageConfiguration)
        {
            _storageConfiguration = storageConfiguration;
        }

        public string GetPublicAccountStorageDownloadFileToken(string fileId)
        {
            string sas = GetAccountSASToken(AccountSasPermissions.Read);

            return $"{fileId}?{sas}";
        }

        public string GetPublicAccountStorageUploadFileToken(string fileId)
        {
            string sas = GetAccountSASToken(AccountSasPermissions.Write);
            return $"{fileId}?{sas}";
        }

        public string GetPublicAccountStorageDeleteFileToken(string fileId)
        {
            string sas = GetAccountSASToken(AccountSasPermissions.Delete);
            return $"{fileId}?{sas}";
        }

        public async Task<bool> UploadDocumentTemplate(IFormFile file, string fileId)
        {
            return await UploadFile(file, fileId, _storageConfiguration.DocumentTemplatesConnection, _storageConfiguration.DocumentTemplatesContainerReference);
        }

        public async Task<Either<HttpResponseMessage, Errors>> DownloadDocumentTemplate(string fileId, string fileName)
        {
            return await DownloadFile(fileId, fileName, _storageConfiguration.DocumentTemplatesConnection, _storageConfiguration.DocumentTemplatesContainerReference);
        }

        public async Task<bool> UploadDocument(IFormFile file, string fileId)
        {
            return await UploadFile(file, fileId, _storageConfiguration.DocumentsConnection, _storageConfiguration.DocumentsContainerReference);
        }

        public async Task<Either<HttpResponseMessage, Errors>> DownloadDocument(string fileId, string fileName)
        {
            return await DownloadFile(fileId, fileName, _storageConfiguration.DocumentsConnection, _storageConfiguration.DocumentsContainerReference);
        }

        private async Task<bool> UploadFile(IFormFile file, string fileId, string storageConnection, string containerReference)
        {
            try
            {

                BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnection);
                {

                    BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerReference);

                    if (await container.ExistsAsync())
                    {
                        BlobClient blobClient = container.GetBlobClient(fileId);

                        Stream fileStream = file.OpenReadStream();
                        await blobClient.UploadAsync(fileStream, true);
                        fileStream.Close();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task<Either<HttpResponseMessage, Errors>> DownloadFile(string fileId, string fileName, string storageConnection, string containerReference)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    var blobServiceClient = new BlobServiceClient(storageConnection);

                    BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerReference);

                    if (await container.ExistsAsync())
                    {
                        BlobClient file = container.GetBlobClient(fileId);

                        if (await file.ExistsAsync())
                        {

                            var extension = Path.GetExtension(file.Uri.AbsoluteUri);

                            await file.DownloadToAsync(ms);
                            var arrayByte = ms.ToArray();
                            var stream = new MemoryStream(arrayByte);
                            HttpResponseMessage httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                            httpResponse.Content = new StreamContent(stream);
                            httpResponse.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                            string contentType = String.Empty;
                            switch (extension)
                            {
                                case ".doc":
                                case ".docx":
                                    contentType = "application/octet-stream";
                                    break;
                                case ".pdf":
                                    contentType = "application/pdf";
                                    break;
                                case ".mp4":
                                    contentType = "video/mp4";
                                    break;
                                default:
                                    contentType = "application/octet-stream";
                                    break;
                            }

                            httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);


                            return new(httpResponse);
                        }
                        else
                        {
                            return new(new Errors(ErrorType.NotFound, "File does not exist"));

                        }
                    }
                    else
                    {
                        return new(new Errors(ErrorType.NotFound, "Container does not exist"));
                    }
                }
            }
            catch (Exception ex)
            {
                return new(new Errors(ErrorType.Internal, "Error opening storage"));
            }
        }



        private string GetAccountSASToken(AccountSasPermissions accountSasPermissions)
        {
            StorageSharedKeyCredential key = new StorageSharedKeyCredential(_storageConfiguration.PublicDocumentsAccountReference, _storageConfiguration.PublicDocumentsConnectionKey);

            // Create a SAS token that's valid for one hour.
            AccountSasBuilder sasBuilder = new AccountSasBuilder()
            {
                Services = AccountSasServices.Blobs | AccountSasServices.Files,
                ResourceTypes = AccountSasResourceTypes.Object,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(_storageConfiguration.PublicDocumentsSasExpirationMinutes),
                Protocol = SasProtocol.Https
            };

            sasBuilder.SetPermissions(accountSasPermissions);

            // Use the key to get the SAS token.
            string sasToken = sasBuilder.ToSasQueryParameters(key).ToString();

            return sasToken;
        }
    }
}