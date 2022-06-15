namespace WebUI;

public static class ApiConstants
{
    public const int SuccessStatusCode = 200;
    public const int CreatedStatusCode = 201;
    public const int BadRequestStatusCode = 400;
    public const int ForbiddenStatusCode = 403;
    public const int NotFoundStatusCode = 404;
    
    public const string BaseUrl = "api/minanis/v1";

    public const string PageNo = "pageNo";
    public const string PageSize = "pageSize";
    public const char FirstPage = '1';

    public const string SelfRelation = "self";
    public const string FirstRelation = "first";
    public const string LastRelation = "last";
    public const string PrevRelation = "prev";
    public const string NextRelation = "next";

    public const string Success = "Task run successfully";
    public const string Failed = "Task run failed";
    public const string NotFoundMessage = "No records found";
    public const string NoMessage = "No Message";
    public const string Unauthorized = "You are unauthorized";
}
