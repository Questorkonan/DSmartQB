using System;
using System.Collections.Generic;

namespace DSmartQB.CORE.DTOs
{

    public class PreviewForSelect
    {
        public int Id { get; set; }
        public string ExamId { get; set; }
    }

    public class ExamHeader
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public int Duration { get; set; }
        public int CurrentMark { get; set; }
        public int Mark { get; set; }
        public bool Published { get; set; }
    }

    public class Preview
    {
        public ExamHeader Header { get; set; }
        public List<ExamBody> ExamBody { get; set; }
    }

    public class ExamBody
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class ExamBinder
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        public string Type { get; set; }
        public int NoOfQuestions { get; set; }
        public string Group { get; set; }
        public string GroupId { get; set; }
        public bool Published { get; set; }
    }
    public class ExamPost
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        public string Type { get; set; }
        public string CourseId { get; set; }
        public string GroupId { get; set; }
        public int Duration { get; set; }
        public string Supervisor { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class SpecifedExam
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GroupId { get; set; }
        public int Duration { get; set; }
        public int Mark { get; set; }
        public string StartDate { get; set; }
    }

    public class ExamPagination
    {
        public List<ExamBinder> Exams { get; set; }
        public int TotalRows { get; set; }
    }

    public class MannualExam
    {
        public string ExamId { get; set; }
        public string GroupId { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public string CorrectAnswerId { get; set; }
        public int Degree { get; set; }
    }


    public class BluePrintArchieve
    {
        public string Id { get; set; }
        public int Degree { get; set; }
    }
    public class BluePrintParams
    {
        public string ExamId { get; set; }
        public int Degree { get; set; }
        public int NoQuestions { get; set; }
        public int Mild { get; set; }
        public int Normal { get; set; }
        public int Hard { get; set; }
    }

    public class ArchieveItems
    {
        public string Id { get; set; }
        public string ExamId { get; set; }
        public int Degree { get; set; }
    }

    public class PreviewQuestion
    {
        public string Id { get; set; }
        public string ExamId { get; set; }
        public string Stem { get; set; }
        public string Type { get; set; }
    }

    public class PreviewAnswers
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; }
    }


    public class PreviewObj
    {
        public PreviewQuestion Question { get; set; }
        public List<PreviewAnswers> Answers { get; set; }
    }

    public class QuestionBinder
    {
        public string Id { get; set; }
        public int Degree { get; set; }
        public string Type { get; set; }
    }

    public class Publish
    {
        public List<QuestionBinder> Questions { get; set; }
        public string ExamId { get; set; }
    }

    public class OnlineStudentsGrid
    {
        public bool Status { get; set; }
        public string Color { get; set; }
        public string Student { get; set; }
        public string JoiningDate { get; set; }
    }

}
