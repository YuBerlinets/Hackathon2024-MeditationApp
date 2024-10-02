using meditationApp.Entities;

namespace meditationApp.DTO.article;

public class ArticleResponseDTO
{
    public int Id { get; set; }
    public DateTime PublishDate { get; set; }
    
    public string Title { get; set; }
    
    public string Category { get; set; }
    
    public string Type { get; set; }
    
    public string Preview { get; set; }
    
    public string PreviewAlt { get; set; }

    public List<SectionSchemaItem> ItemsSchema { get; set; } = [];

    public List<ParagraphBlock> ParagraphItems { get; set; } = [];

    public List<ImageBlock> ImageBlocks { get; set; } = [];

    public List<UnorderedListBlock> UnorderedListBlocks { get; set; } = [];
}