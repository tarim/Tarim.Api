using System;
namespace Tarim.Api.Infrastructure.Model.Name
{
    public class TopName
    {
        public int Id { get; set; }
        public string NameUg { get; set; }
        public string NameLatin { get; set; }
        public int LikeCount { get; set; }
        public int LoveCount { get; set; }
        public int MyNameCount { get; set; }
    }
}
