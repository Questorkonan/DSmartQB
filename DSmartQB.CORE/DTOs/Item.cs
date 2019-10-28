using System.Collections.Generic;

namespace DSmartQB.CORE.DTOs
{
    public class ItemTypeDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }

    public class QuestionDto
    {
        public string Id { get; set; }
        public string Duration { get; set; }
        public string Item { get; set; }
        public ItemTypeDto Type { get; set; }
        public string level { get; set; }
        public string Ilo { get; set; }
        public string TFStatus { get; set; }
        public string UserId { get; set; }
    }

    public class ItemAddDto
    {
        public QuestionDto Question { get; set; }
        public List<AlternativesDto> Alternatives { get; set; }
    }

    public class AlternativesDto
    {
        public string Text { get; set; }
        public bool Correct { get; set; }
    }


    public class ItemPagination
    {
        public List<ItemListDto> Items { get; set; }
        public int TotalRows { get; set; }
    }

    public class ItemListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Difficulty { get; set; }
        public string Type { get; set; }
    }


    public class MannualItems
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Degree { get; set; }
        public string Difficulty { get; set; }
        public string Type { get; set; }
        public string CorrectAnswerId { get; set; }
    }

    public class ExamItemsPagination
    {
        public List<MannualItems> Items { get; set; }
        public int TotalRows { get; set; }
    }

    public class Item
    {
        public string Id { get; set; }
        public string Stem { get; set; }
        public int Duration { get; set; }
        public string Level { get; set; }
        public string ILoId { get; set; }
    }

    public class Answer
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; }
    }

    public class Planner
    {
        public string PlannerId { get; set; }
        public string CourseId { get; set; }
    }


    public class MCQ
    {
        public Planner Planner { get; set; }
        public List<Answer> Answers { get; set; }
        public Item Item { get; set; }
    }

    public class TF
    {
        public Planner Planner { get; set; }
        public Answer Answer { get; set; }
        public Item Item { get; set; }
    }

    public class Ids
    {
        public string QId { get; set; }
        public string ExamId { get; set; }
    }


    public class Question
    {
        public string Id { get; set; }
        public string Stem { get; set; }
        public string Type { get; set; }
        public int Degree { get; set; }
    }


}
