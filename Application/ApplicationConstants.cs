namespace Application;

public static class ApplicationConstants
{
    public const char Period = '.';
    public const char OpenParenthesis = '(';
    public const char CloseParenthesis = ')';
    public const char WhiteSpace = ' ';
    public const int SqlExceptionErrorCode = 2601;
    public const string CanNotInsertDuplicatedValue = "Cannot insert duplicated value into table";
    public const string TheDuplicatedValueIs = "The duplicated value is";
    
    public const string TaskCancelled = "Request was cancelled";

    #region Product

    public const string ProductDoesNotExist = "Product does not exist";
    public const string SuccessfullyGetProductById = "Successfully get product by Id";
    public const string SuccessfullyGetAllProducts = "Successfully get all products";
    public const string SuccessfullyCreateNewProduct = "Successfully create new product";
    public const string SuccessfullyUpdateProduct = "Successfully update product";
    public const string FailedToCreateNewProduct = "Failed to create new product";
    public const string FailedToUpdateProduct = "Failed to update product";

    #endregion
}