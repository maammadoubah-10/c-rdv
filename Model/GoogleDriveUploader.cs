using System;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;

public class GoogleDriveUploader
{
    static string[] Scopes = { DriveService.Scope.DriveFile };
    static string ApplicationName = "appRendez-vous";

    public static string UploadFileToGoogleDrive(string filePath)
    {
        try
        {
            // Charger les identifiants OAuth depuis credentials.json
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Créer le service Google Drive
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Créer un fichier Google Drive
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
                MimeType = "application/pdf"
            };

            // Télécharger le fichier
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "application/pdf");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;

            // Rendre le fichier public
            var permission = new Google.Apis.Drive.v3.Data.Permission()
            {
                Role = "reader",
                Type = "anyone"
            };
            service.Permissions.Create(permission, file.Id).Execute();

            // Retourner l'URL publique du fichier
            return $"https://drive.google.com/uc?id={file.Id}";
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : " + ex.Message);
            return null;
        }
    }
}
