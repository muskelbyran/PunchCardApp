using PunchCardApp.Entities;
using PunchCardApp.Factories;
using PunchCardApp.Models;
using PunchCardApp.Repositories;

public class FeedbackService
{
    private readonly FeedbackRepository _feedbackRepository;

    public FeedbackService(FeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public async Task<ResponseResult> CreateFeedbackAsync(FeedbackModel feedback)
    {
        try
        {
            // Validate that the Message is not null or empty
            if (string.IsNullOrWhiteSpace(feedback.Message))
            {
                return ResponseFactory.Error("Message cannot be empty.");
            }

            // Map the FeedbackModel to FeedbackEntity
            var feedbackEntity = new FeedbackEntity
            {
                Message = feedback.Message,
                FromName = feedback.FromName,
                SubmittedAt = feedback.SubmittedAt,
                IsTypo = feedback.IsTypo,
                IsBug = feedback.IsBug,
                IsIdea = feedback.IsIdea,
                IsPraise = feedback.IsPraise
            };

            // Insert the feedback into the repository
            return await _feedbackRepository.CreateOneAsync(feedbackEntity);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"Error creating feedback: {ex.Message}");
        }
    }


    public async Task<ResponseResult> GetAllFeedbackAsync()
    {
        try
        {
            return await _feedbackRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"Error fetching feedback: {ex.Message}");
        }
    }

    public async Task<ResponseResult> DeleteFeedbackAsync(int feedbackId)
    {
        try
        {
            return await _feedbackRepository.DeleteOneAsync(f => f.Id == feedbackId);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"Error deleting feedback: {ex.Message}");
        }
    }
}