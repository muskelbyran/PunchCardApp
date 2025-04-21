using PunchCardApp.Entities;
using PunchCardApp.Models;
using PunchCardApp.Repositories;

public class CourseService
{
    private readonly CourseRepository _courseRepository;
    private readonly ILogger<CourseService> _logger;

    public CourseService(CourseRepository courseRepository, ILogger<CourseService> logger)
    {
        _courseRepository = courseRepository;
        _logger = logger;
    }

    public async Task CreateCourseAsync(CreateCourseModel model)
    {
        try
        {
            string? imagePath = null;

            if (model.ImageFile != null)
            {
                Console.WriteLine("Image file detected.");

                _logger.LogInformation("Uploading image: {ImageFileName}", model.ImageFile.FileName);

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/courses");
                Console.WriteLine($"Uploads folder path: {uploadsFolder}");

                Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists
                Console.WriteLine("Created or verified uploads folder.");

                var fileExtension = Path.GetExtension(model.ImageFile.FileName).ToLower();
                Console.WriteLine($"File extension: {fileExtension}");

                var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
                if (!validExtensions.Contains(fileExtension))
                {
                    Console.WriteLine("Invalid file type.");
                    _logger.LogError("Invalid file type.");
                    throw new InvalidOperationException("Invalid file type.");
                }

                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName); // Full path to save the image
                Console.WriteLine($"Generated unique file name: {uniqueFileName}");
                Console.WriteLine($"Full file path: {filePath}");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Console.WriteLine("Saving image file to the server...");
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = $"images/courses/{uniqueFileName}";
                Console.WriteLine($"Image saved at: {imagePath}");

                _logger.LogInformation("Image saved at: {ImagePath}", imagePath);
            }
            else
            {
                Console.WriteLine("No image file provided.");
            }

            var courseEntity = new CourseEntity
            {
                Title = model.Title,
                Tagline = model.Tagline,
                Category = model.Category,
                Price = model.Price,
                StartDate = model.StartDate,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                Times = model.Times,
                Location = model.Location
            };

            Console.WriteLine("Created course entity. Now saving to the repository...");

            await _courseRepository.CreateCourseAsync(courseEntity);

            Console.WriteLine("Course saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating course: {ex.Message}");
            _logger.LogError(ex, "Error creating course");
            throw;
        }
    }

    public async Task<CourseEntity?> GetCourseByImageAsync(string imageFileName)
    {
        var allCourses = await _courseRepository.GetAllCoursesAsync();
        return allCourses.FirstOrDefault(c => c.ImageUrl != null && c.ImageUrl.Contains(imageFileName));
    }


    public async Task<List<CourseEntity>> GetAllCoursesAsync()
    {
        return await _courseRepository.GetAllCoursesAsync();
    }

    public async Task DeleteUnusedImagesAsync()
    {
        // Hämta alla bilder som är länkade till kurser
        var allCourses = await _courseRepository.GetAllCoursesAsync();
        var usedImages = allCourses
            .Where(course => !string.IsNullOrEmpty(course.ImageUrl))
            .Select(course => course.ImageUrl)
            .ToList();

        var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "courses");

        var allImageFiles = Directory.GetFiles(imagesFolder);

        foreach (var imageFile in allImageFiles)
        {
            var imageName = Path.GetFileName(imageFile);

            // Om bilden inte längre används av någon kurs, ta bort den
            if (!usedImages.Contains($"images/courses/{imageName}"))
            {
                try
                {
                    File.Delete(imageFile);
                    _logger.LogInformation($"Deleted unused image: {imageName}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to delete image: {imageName}");
                }
            }
        }
    }

    public async Task DeleteCourseAsync(int courseId)
    {
        try
        {
            await _courseRepository.DeleteCourseAsync(courseId);
            _logger.LogInformation("Course with ID {CourseId} deleted successfully.", courseId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete course with ID {CourseId}", courseId);
            throw;
        }
    }


}