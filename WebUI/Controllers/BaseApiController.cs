using System.Text;
using Application;
using Microsoft.AspNetCore.Mvc;
using static WebUI.ApiConstants;

namespace WebUI.Controllers;

[ApiController]
[Route(BaseUrl)]
public class BaseApiController : ControllerBase
{
    private static string RemoveQueryStringPagination(string queryStringWithPagination)
    {
        var queryString = queryStringWithPagination.Split(Ampersand).ToList();
        queryString[0] = queryString[0].Remove(0, 1);

        var queryStringWithoutPagination =
            queryString.Where(t => !t.Contains(PageNo) && !t.Contains(PageSize)).ToList();
        queryStringWithoutPagination.Add(string.Empty);

        return string.Join(Ampersand, queryStringWithoutPagination.ToArray());
    }

    private static string BuildPaginationUrl(string relation, StringBuilder urlWithoutPagination, Pagination pagination)
    {
        urlWithoutPagination
            .Append(PageNo)
            .Append(EqualSign);

        switch (relation)
        {
            case SelfRelation:
                urlWithoutPagination.Append(pagination.PageNo);
                break;
            case FirstRelation:
                urlWithoutPagination.Append(FirstPage);
                break;
            case LastRelation:
                urlWithoutPagination.Append(pagination.LastPage);
                break;
            case PrevRelation:
                urlWithoutPagination.Append(pagination.PageNo - 1);
                break;
            case NextRelation:
                urlWithoutPagination.Append(pagination.PageNo + 1);
                break;
        }

        urlWithoutPagination
            .Append(Ampersand)
            .Append(PageSize)
            .Append(EqualSign)
            .Append(pagination.PageSize);

        return urlWithoutPagination.ToString();
    }

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        var controllerName = Request.Path.ToString().Split(BaseUrl).Last().Split(Slash).First();

        switch (result.IsSuccess)
        {
            case true when result.Value == null:
                return NotFound(NotFoundMessage);
            case false when !string.IsNullOrEmpty(result.NotFoundMessage):
                return NotFound(result.NotFoundMessage);
            case false when result.IsUnauthorized:
                return new ObjectResult(result.UnauthorizedMessage) {StatusCode = 403};
            case true when result.Value != null:
            {
                #region CREATED - 201

                if (!string.IsNullOrEmpty(result.NewResourceId))
                {
                    var newResourceUrl = new StringBuilder(Request.Scheme);
                    newResourceUrl.Append(Colon);
                    newResourceUrl.Append(Slash);
                    newResourceUrl.Append(Slash);
                    newResourceUrl.Append(Request.Host);
                    newResourceUrl.Append(Slash);
                    newResourceUrl.Append(BaseUrl);
                    newResourceUrl.Append(Slash);
                    newResourceUrl.Append(controllerName);
                    newResourceUrl.Append(Slash);
                    newResourceUrl.Append(result.NewResourceId);

                    return Created(newResourceUrl.ToString(),
                        new {message = result.SuccessMessage, data = result.Value});
                }

                #endregion

                #region NORMAL - 200

                if (result.Pagination is null)
                    return Ok(new {message = result.SuccessMessage, data = result.Value});

                #endregion

                #region Pagination - 200

                var queryStringWithoutPagination = RemoveQueryStringPagination(Request.QueryString.ToString());

                var urlWithoutPagination = new StringBuilder(Slash.ToString());
                urlWithoutPagination.Append(controllerName);
                urlWithoutPagination.Append(QuestionMark);
                urlWithoutPagination.Append(queryStringWithoutPagination);

                List<object> linkData = new()
                {
                    new
                    {
                        href = BuildPaginationUrl(SelfRelation, urlWithoutPagination, result.Pagination),
                        rel = SelfRelation
                    },
                    new
                    {
                        href = BuildPaginationUrl(FirstRelation, urlWithoutPagination, result.Pagination),
                        rel = FirstRelation
                    },
                    new
                    {
                        href = BuildPaginationUrl(LastRelation, urlWithoutPagination, result.Pagination),
                        rel = LastRelation
                    }
                };

                if (result.Pagination.PageNo > 1 && result.Pagination.PageNo <= result.Pagination.LastPage)
                    linkData.Add(new
                    {
                        href = BuildPaginationUrl(PrevRelation, urlWithoutPagination, result.Pagination),
                        rel = PrevRelation
                    });

                if (result.Pagination.PageNo >= 1 && result.Pagination.PageNo < result.Pagination.LastPage)
                    linkData.Add(new
                    {
                        href = BuildPaginationUrl(NextRelation, urlWithoutPagination, result.Pagination),
                        rel = NextRelation
                    });

                var response = new
                {
                    message = result.SuccessMessage,
                    metaData = new
                    {
                        page = result.Pagination.PageNo,
                        limit = result.Pagination.PageSize,
                        count = result.Pagination.Count,
                        lastPage = result.Pagination.LastPage,
                        totalRecord = result.Pagination.TotalRecord,
                        totalPage = result.Pagination.TotalPage
                    },
                    linkData,
                    data = result.Value
                };

                return Ok(response);

                #endregion
            }
            default:
                return BadRequest(result.ErrorMessage);
        }
    }
}