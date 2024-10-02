using System.ComponentModel.DataAnnotations;
using meditationApp.Entities.enums;

namespace meditationApp.Entities;

public class Article
{
    public int Id { get; set; }
    public DateTime PublishDate { get; set; } = DateTime.UtcNow;
    public string Title { get; set; }
    public string Category { get; set; }
    public string Type { get; set; }
    public string Preview { get; set; }
    public string PreviewAlt { get; set; }

    public List<SectionSchemaItem> ItemsSchema { get; set; } = [];

    public List<ParagraphBlock> ParagraphItems { get; set; } = [];

    public List<ImageBlock> ImageBlocks { get; set; } = [];

    public List<UnorderedListBlock> UnorderedListBlocks { get; set; } = [];
    public virtual ICollection<User> Users { get; set; }

}

public class SectionSchemaItem
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Title { get; set; }
    public List<SchemaItem> SectionItems { get; set; } = [];
}

public class SchemaItem
{
    public int Id { get; set; }
    public string Key { get; set; }
    public int Order { get; set; }
}

public class ParagraphBlock
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

public class ImageBlock
{
    public int Id { get; set; }
    public string Key { get; set; }
    public ImageItem ImageItem { get; set; }
}

public class ImageItem
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string ImageUrl { get; set; }
    public string DarkImageUrl { get; set; }
    public string ImageAlt { get; set; }
}

public class UnorderedListBlock
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Title { get; set; }
    public List<UnorderedListItem> Items { get; set; }
}

public class UnorderedListItem
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}