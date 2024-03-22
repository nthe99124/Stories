using StoriesProject.Model.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesProject.Model.DTO.Chapter
{
    public class ChapterForEditDTO
    {
        public List<string> ImgLinkContent { get; set; }
        public string StoryName { get; set; }
        public string ChapterName { get; set; }
        public Guid ChapterId  { get; set; }

    }
}
