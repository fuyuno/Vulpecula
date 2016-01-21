namespace Vulpecula.Scripting.Objects
{
    public class DataSource
    {
        public SourceType SourceType { get; }

        public string Tag { get; }

        public DataSource(SourceType type, object tag)
        {
            SourceType = type;
            Tag = tag.ToString();
        }
    }
}