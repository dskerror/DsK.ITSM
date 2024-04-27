﻿namespace DsK.ITSM.Shared.APIService;
public class PagingRequest
{
    private string? _orderBy = null;

    public PagingRequest()
    {
        _orderBy = "Id";
    }
    public string? SearchString { get; set; } = null;
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? OrderBy
    {
        get { return _orderBy; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                string[] OrderBy = value.Split(',');
                _orderBy = string.Join(",", OrderBy);
            }
            else
                _orderBy = "Id";
        }
    }
}