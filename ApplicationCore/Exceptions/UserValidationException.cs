﻿namespace ApplicationCore.Exceptions;
public class UserValidationException : Exception
{
    public UserValidationException(string message) : base(message)
    {
    }
}
