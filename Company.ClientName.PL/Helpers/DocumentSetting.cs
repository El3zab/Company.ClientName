namespace Company.ClientName.PL.Helpers
{
    public static class DocumentSetting
    {
        // 1. Upload
        // ImageName
        public static string UploadFile(IFormFile file, string folderName)
        {
            // File Path
            // 1. Get Folder Location
            //string folderPath = "C:\\Users\\new lap\\source\\repos\\Company.ClientName\\Company.ClientName.PL\\wwwroot\\files\\" + folderName;

            //var folderPath =  Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName ;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // 2. Get File Name And Make It Unique
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        // 2. Delete

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
