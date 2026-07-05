using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile file);
    Task<string> SaveImageFromUrlAsync(string imageUrl);
    Task<string> SaveImageFromBase64Async(string input);
    Task DeleteImageAsync(string name);
}
