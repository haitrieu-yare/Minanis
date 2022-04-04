namespace Application;

public static class ApplicationUtils
{
    public static int CalculateLastPage(int totalRecord, int pageSize)
    {
        var remain = totalRecord % pageSize;
        var lastPage = (totalRecord - remain) / pageSize;

        if (remain > 0) lastPage++;

        return lastPage;
    }
}