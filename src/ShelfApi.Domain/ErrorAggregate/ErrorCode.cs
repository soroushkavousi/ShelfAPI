namespace ShelfApi.Domain.ErrorAggregate;

public enum ErrorCode : short
{
    InternalServerError = 1,
    AuthenticationError = 2,
    AccessDenied = 3,
    ItemNotFound = 4,
    ItemAlreadyExists = 5,
    InvalidFormat = 6,
    InvalidValue = 7,
}