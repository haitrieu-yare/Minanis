namespace Application;

public class Pagination
{
    public Pagination()
    {
    }

    public Pagination(int pageNo, int pageSize, int count, int totalRecord, int totalPage)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        Count = count;
        TotalRecord = totalRecord;
        TotalPage = totalPage;
    }

    public int PageNo { get; init; }
    public int PageSize { get; init; }
    public int Count { get; init; }
    public int TotalRecord { get; init; }
    public int TotalPage { get; init; }
}