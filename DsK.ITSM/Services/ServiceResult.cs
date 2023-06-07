namespace DsK.ITSM.Services;
public class ServiceResult<T> where T : class
{
    public ServiceResult()
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

