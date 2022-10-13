using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DevFest22Asyut.Helpers
{
    public static class SaveFile
    {
        public static async Task<string> Save(IFormFile file,IHostingEnvironment environment,string prefix = "img_")
        {
            string imagePath = String.Empty;
            if (file != null)
            {
                if (file.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = prefix + Guid.NewGuid() + extension;
                    var directory = Path.Combine(environment.WebRootPath, "Images");

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    using (var stream = new FileStream(Path.Combine(directory, fileName), FileMode.CreateNew,
                        FileAccess.ReadWrite))
                    {
                        await file.CopyToAsync(stream);
                    }

                    imagePath = "/Images/" + fileName;
                }
            }

            return imagePath;
        }

        public static async Task<string> SavePDF(IFormFile file, IHostingEnvironment environment, string prefix = "pdf_")
        {
            string pdfPath = "";
            if (file != null)
            {
                if (file.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var actualFileName = Path.GetFileName(file.FileName);
                    var fileName = prefix + new Random().Next() + actualFileName;
                    var directory = Path.Combine(environment.WebRootPath, "PDFs");

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    using (var stream = new FileStream(Path.Combine(directory, fileName), FileMode.CreateNew,
                        FileAccess.ReadWrite))
                    {
                        await file.CopyToAsync(stream);
                    }

                    pdfPath = "/PDFs/" + fileName;
                }
            }

            return pdfPath;
        }

        internal static Task<string> Save(IFormFile image, object environment)
        {
            throw new NotImplementedException();
        }
    }

}
