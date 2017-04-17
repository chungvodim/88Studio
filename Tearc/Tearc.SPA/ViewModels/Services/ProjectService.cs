using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WebApplication.Core.React;

namespace ViewModels
{
   public class ProjectRecord
    {
      public int Id { get; set; }
      public string Type { get; set; }
      public string Category { get; set; }
      public bool Recommended { get; set; }
      public string Title { get; set; }
      public string Author { get; set; }
      public float Rating { get; set; }
      public string ImageUrl { get; set; }
      public string[] ImageUrls { get; set; }
      public string ItemUrl { get; set; }
      public string UrlSafeTitle => ToUrlSafe(Title);

      public static string ToUrlSafe(string title) => title.ToLower().Replace("\'", "").Replace(".", "dot").Replace("#", "sharp").Replace(' ', '-');
   }

   public class ProjectService
    {
      public IEnumerable<ProjectRecord> GetAllRecords() => JsonConvert.DeserializeObject<List<ProjectRecord>>(this.GetEmbeddedResource("projects.json"));

      public IEnumerable<ProjectRecord> GetAllProjects() => GetAllRecords().Where(i => i.Type == "Project");

      public ProjectRecord GetProjectByTitle( string title ) => GetAllProjects().FirstOrDefault(i => i.UrlSafeTitle == title);
   }
}
