using Microsoft.Extensions.FileProviders;
using OfficeOpenXml;
using System.IO;
using System.Reflection;

namespace DATN.Client.Services
{
    public class ExcelService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ExcelService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<Stream> ExportExcel<T>(string fileName, Dictionary<int, string> titles, IEnumerable<T> records) where T : class
        {
            try
            {
                using (var package = new ExcelPackage())
                {

                    // add a new worksheet to the Excel package
                    var worksheet = package.Workbook.Worksheets.Add(fileName);

                    foreach (KeyValuePair<int, string> kvp in titles)
                    {
                        worksheet.Cells[1, kvp.Key].Value = kvp.Value;
                    }

                    // set data rows
                    int rowIndex = 2;
                    foreach (T record in records)
                    {
                        // Use reflection to get the properties of the record type
                        PropertyInfo[] properties = typeof(T).GetProperties();

                        // Start from the first column
                        int columnIndex = 1;
                        foreach (PropertyInfo property in properties)
                        {
                            // Get the value of the property for the current record
                            object value = property.GetValue(record);

                            // Set the value in the corresponding cell
                            worksheet.Cells[rowIndex, columnIndex].Value = value;

                            columnIndex++;
                        }

                        rowIndex++;
                    }

                    // save Excel package to a file
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string filePath = Path.Combine(webRootPath, fileName);
                    FileInfo file = new FileInfo(filePath);
                    await package.SaveAsAsync(file);

                    // return file download
                    PhysicalFileProvider fileProvider = new PhysicalFileProvider(webRootPath);
                    IFileInfo fileInfo = fileProvider.GetFileInfo(fileName);
                    Stream fileStream = fileInfo.CreateReadStream();

                    return fileStream;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<List<T>> ReadExcel<T>(IFormFile file) where T : class
        {
            List<T> records = new List<T>();
            string tempFileName = "";
            string tempFilePath = "";

            try
            {

                // Generate a unique temporary file name
                tempFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                // Combine the temporary directory path with the file name
                tempFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "TempFiles", tempFileName);

                // Create the directory if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath));

                // Save the uploaded Excel file to the temporary location
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Now you can read the Excel file using EPPlus
                using (var package = new ExcelPackage(new FileInfo(tempFilePath)))
                {
                    PropertyInfo[] properties = typeof(T).GetProperties();

                    var worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Skip the header is in the first row
                    {
                        // Create an instance of the object 'T'
                        T item = Activator.CreateInstance<T>();
                        try
                        {

                            // Iterate through each property of 'T'
                            for (int col = 1; col <= properties.Length; col++)
                            {
                                // Access the cell value using the 'Cells' property
                                var cellValue = worksheet.Cells[row, col].Text.Trim();

                                // Get the corresponding property of 'T'
                                PropertyInfo property = properties[col - 1];

                                // Use reflection to set the property value based on the cell value
                                if (property.PropertyType == typeof(string))
                                {
                                    property.SetValue(item, cellValue);
                                }
                                else if (property.PropertyType == typeof(int))
                                {
                                    int intValue;
                                    if (int.TryParse(cellValue, out intValue))
                                    {
                                        property.SetValue(item, intValue);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        records.Add(item);
                    }

                    // delete the temporary file when done
                    System.IO.File.Delete(tempFilePath);
                }

                return records;
            }
            catch (Exception)
            {
                // delete the temporary file when error
                System.IO.File.Delete(tempFilePath);
                return records;
            }

        }
    }
}
