using DevExtreme.AspNet.Data;

public class DataSourceLoadOptions : DataSourceLoadOptionsBase
{
    public string[]? Sort { get; set; }
    public string[]? Group { get; set; }
    public string[]? Filter { get; set; }
}
