﻿using System.Text;
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

    private static string BuildPaginationUrl(string relation, string urlWithoutPagination, Pagination pagination)
    {
        var urlWithPaginationBuilder = new StringBuilder(urlWithoutPagination)
            .Append(PageNo)
            .Append(EqualSign);

        switch (relation)
        {
            case SelfRelation:
                urlWithPaginationBuilder.Append(pagination.PageNo);
                break;
            case FirstRelation:
                urlWithPaginationBuilder.Append(FirstPage);
                break;
            case LastRelation:
                urlWithPaginationBuilder.Append(pagination.TotalPage);
                break;
            case PrevRelation:
                urlWithPaginationBuilder.Append(pagination.PageNo - 1);
                break;
            case NextRelation:
                urlWithPaginationBuilder.Append(pagination.PageNo + 1);
                break;
        }

        urlWithPaginationBuilder
            .Append(Ampersand)
            .Append(PageSize)
            .Append(EqualSign)
            .Append(pagination.PageSize);

        return urlWithPaginationBuilder.ToString();
    }

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        ReadOnlySpan<char> controllerName = Request.Path.ToString();
        controllerName = controllerName[1..];

        switch (result.IsSuccess)
        {
            case true when result.Value == null:
                return NotFound(new CustomResponse
                {
                    StatusCode = NotFoundStatusCode,
                    Message = NotFoundMessage,
                });
            case false when !string.IsNullOrEmpty(result.NotFoundMessage):
                return NotFound(new CustomResponse
                {
                    StatusCode = NotFoundStatusCode,
                    Message = result.NotFoundMessage,
                });
            case false when result.IsUnauthorized:
                return new ObjectResult(new CustomResponse
                {
                    StatusCode = ForbiddenStatusCode,
                    Message = result.UnauthorizedMessage ?? ApiConstants.Unauthorized,
                }) {StatusCode = NotFoundStatusCode};
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

                    return Created(newResourceUrl.ToString(), new CustomResponse
                    {
                        StatusCode = CreatedStatusCode,
                        Message = result.SuccessMessage ?? Success,
                        Data = result.Value,
                    });
                }

                #endregion

                #region NORMAL - 200

                if (result.Pagination is null)
                    return Ok(new CustomResponse
                    {
                        StatusCode = SuccessStatusCode,
                        Message = result.SuccessMessage ?? Success,
                        Data = result.Value
                    });

                #endregion

                #region PAGINATION - 200

                var queryStringWithoutPagination = RemoveQueryStringPagination(Request.QueryString.ToString());

                var urlWithoutPagination = new StringBuilder(Slash.ToString())
                    .Append(controllerName)
                    .Append(QuestionMark)
                    .Append(queryStringWithoutPagination).ToString();

                List<LinkData> linkData = new()
                {
                    new LinkData
                    {
                        Href = BuildPaginationUrl(SelfRelation, urlWithoutPagination, result.Pagination),
                        Rel = SelfRelation
                    },
                    new LinkData
                    {
                        Href = BuildPaginationUrl(FirstRelation, urlWithoutPagination, result.Pagination),
                        Rel = FirstRelation
                    },
                    new LinkData
                    {
                        Href = BuildPaginationUrl(LastRelation, urlWithoutPagination, result.Pagination),
                        Rel = LastRelation
                    }
                };

                if (result.Pagination.PageNo > 1 && result.Pagination.PageNo <= result.Pagination.TotalPage)
                    linkData.Add(new LinkData
                    {
                        Href = BuildPaginationUrl(PrevRelation, urlWithoutPagination, result.Pagination),
                        Rel = PrevRelation
                    });

                if (result.Pagination.PageNo >= 1 && result.Pagination.PageNo < result.Pagination.TotalPage)
                    linkData.Add(new LinkData
                    {
                        Href = BuildPaginationUrl(NextRelation, urlWithoutPagination, result.Pagination),
                        Rel = NextRelation
                    });

                var customResponse = new CustomResponse
                {
                    StatusCode = SuccessStatusCode,
                    Message = result.SuccessMessage ?? Success,
                    MetaData = new Pagination()
                    {
                        PageNo = result.Pagination.PageNo,
                        PageSize = result.Pagination.PageSize,
                        Count = result.Pagination.Count,
                        TotalRecord = result.Pagination.TotalRecord,
                        TotalPage = result.Pagination.TotalPage
                    },
                    LinkData = linkData,
                    Data = result.Value
                };

                return Ok(customResponse);

                #endregion
            }
            default:
                return BadRequest(new CustomResponse
                {
                    Message = result.ErrorMessage ?? Failed
                });
        }
    }
}