﻿namespace ApplicationCore.Models.Quiz.View;
public class ViewQuizModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public IList<ViewQuizQuestionModel> Questions { get; set; }
}
