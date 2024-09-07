using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using TunApi.Data;
using TunApi.Models;

namespace TunApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoFilesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoFilesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{todoId}")]
        public async Task<IActionResult> UploadFile(int todoId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Check file size should not exceed 10MB
            if (file.Length > 10 * 1024 * 1024)
                return BadRequest("File size should not exceed 10MB.");

            // Check file is only image
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
                return BadRequest("Only image files are allowed.");

            var todo = await _context.Todo.FindAsync(todoId);
            if (todo == null)
                return NotFound("Todo item not found.");

            var uploadsFolderPath = Path.Combine("uploads", todoId.ToString());
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var filePath = Path.Combine(uploadsFolderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var todoFile = new TodoFile
            {
                FilePath = filePath,
                UploadedAt = DateTime.UtcNow,
                TodoId = todoId
            };

            _context.TodoFiles.Add(todoFile);
            await _context.SaveChangesAsync();

            return Ok(new { filePath });
        }

        [HttpGet("{todoId}/files/{fileId}")]
        public async Task<IActionResult> GetFile(int todoId, int fileId)
        {
            var todoFile = await _context.TodoFiles.FindAsync(fileId);
            if (todoFile == null || todoFile.TodoId != todoId)
                return NotFound("File not found.");

            var filePath = todoFile.FilePath;
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found on server.");

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var mimeType = "application/octet-stream"; // You can set the appropriate MIME type based on the file extension

            return new FileStreamResult(fileStream, mimeType)
            {
                FileDownloadName = Path.GetFileName(filePath)
            };
        }
    }
}