namespace DsK.ITSM.Shared.APIService;
public class APIResult<T>
{
    public APIResult()
    {
        HasError = false;
        Message = "";
        Exception = null;
        Paging = new PagingResponse();
    }
    public T? Result { get; set; }
    public string Message { get; set; }
    public bool HasError { get; set; }
    public PagingResponse Paging { get; set; }
    public Exception? Exception { get; set; }
}