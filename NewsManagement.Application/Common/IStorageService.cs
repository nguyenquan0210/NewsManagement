using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Common
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

        Task DeleteFileAsync(string fileName);

        string GetFileNewsUrl(string fileName);

        Task SaveFileNewsAsync(Stream mediaBinaryStream, string fileName);

        Task DeleteFileNewsAsync(string fileName);
    }
}
