namespace DsK.ITSM.MinimalAPI.Data.Dtos;
public class AddFieldDefinitionDto
{
    public string FieldName { get; set; } = string.Empty;
    public string DataType { get; set; } = "string";
    public Guid CreatedByUserId { get; set; }
}