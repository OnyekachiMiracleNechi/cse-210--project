using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Creating videos
        Video video1 = new Video("Understanding Abstraction", "John Doe", 300);
        video1.AddComment(new Comment("Alice", "Great video!"));
        video1.AddComment(new Comment("Bob", "Very informative."));
        video1.AddComment(new Comment("Charlie", "Helped me a lot!"));

        Video video2 = new Video("Intro to OOP", "Jane Smith", 450);
        video2.AddComment(new Comment("David", "Loved the examples!"));
        video2.AddComment(new Comment("Eve", "Clear explanation."));
        video2.AddComment(new Comment("Frank", "Thanks for sharing!"));

        Video video3 = new Video("C# Basics", "Tom Brown", 600);
        video3.AddComment(new Comment("Grace", "This is helpful!"));
        video3.AddComment(new Comment("Heidi", "Looking forward to more!"));
        
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        // Displaying video details
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");

            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}

