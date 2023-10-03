namespace API.Helpers;

public class Pager<T> where T : class
{
    public string Search { get; set; }
    public int PageIndex { get; set;}
    public int PageSize { get; set;}
    public int Total { get; set;}
    public IEnumerable<T> Registers { get; private set; }

    public Pager(){}

    public Pager(IEnumerable<T> registers, int total, int pageSize, int pageIndex, string search)
    {
        Registers = registers;
        Total = total;
        PageSize = pageSize;
        PageIndex = pageIndex;
        Search = search;
    }

    public int TotalPages
    {
        get 
        {
            return (int)Math.Ceiling(Total / (double)PageSize);
        }
    }

    public bool HasPreviousPage
    {
        get 
        {
            return (PageIndex > 1);
        }
    }

    public bool HasNextPage
    {
        get 
        {
            return (PageIndex < TotalPages);
        }
    }
}