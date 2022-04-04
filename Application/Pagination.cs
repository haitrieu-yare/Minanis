namespace Application;

public class Pagination
{
    public Pagination(int pageNo, int pageSize, int count, int lastPage, int totalRecord, int totalPage)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        Count = count;
        LastPage = lastPage;
        TotalRecord = totalRecord;
        TotalPage = totalPage;
    }

    public int PageNo { get; }
    public int PageSize { get; }
    public int Count { get; }
    public int LastPage { get; }
    public int TotalRecord { get; }
    public int TotalPage { get; }
}