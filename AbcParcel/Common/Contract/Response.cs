namespace AbcParcel.Common.Contract
{
    public class Response<T> where T : class//: Response
    {
        /// <summary>
        /// Indicates if the response contains a result
        /// </summary>
        public bool HasResult
        {
            get
            {
                return Result != null;
            }
        }

        /// <summary>
        /// The result of the response
        /// </summary>
        public T Result { get; set; }
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public List<string> ValidationMessages { get; set; }


        /// <summary>
        /// Indicates if the response is successful or not. Warning or success result type indicate success
        /// </summary>
        public bool Successful
        {
            get
            {
                return ResultType == ResultType.Success || ResultType == ResultType.Warning;
            }
        }
        /// <summary>
        /// Creates a successful response with a given result object
        /// </summary>
        /// <param name="result">The result object to return with the response</param>
        /// <returns>The response object</returns>
        public static Response<T> Success(T result)
        {
            var response = new Response<T> { ResultType = ResultType.Success, Result = result };

            return response;
        }
        public static Response<T> Success(T result, string message = "Successful")
        {
            var response = new Response<T> { ResultType = ResultType.Success, Result = result, Message = message };

            return response;
        }

        /// <summary>
        /// Creates a failed result. It takes no result object
        /// </summary>
        /// <param name="errorMessage">The error message returned with the response</param>
        /// <returns>The created response object</returns>
        public static Response<T> Failed(string errorMessage)
        {
            var response = new Response<T> { ResultType = ResultType.Error, Message = errorMessage };

            return response;
        }



        /// <summary>
        /// Creates a validation error response, indicating the input was invalid
        /// </summary>
        /// <param name="validationMessages">The validation message</param>
        /// <returns>The Response object</returns>
        public static Response<T> ValidationError(List<string> validationMessages)
        {
            var response = new Response<T> { ResultType = ResultType.ValidationError, Message = "Response has validation errors", ValidationMessages = validationMessages };

            return response;
        }

        /// <summary>
        /// Creates a warning result. The warning result is successful, but might have issues that should be addressed or logged
        /// </summary>
        /// <param name="warningMessage">The warning returned with the response</param>
        /// <param name="result">The result object</param>
        /// <returns>The created response object</returns>
        public static Response<T> Warning(string warningMessage, T result)
        {
            var response = new Response<T>
            {
                ResultType = ResultType.Warning,
                Message = warningMessage,
                Result = result
            };

            return response;
        }

        /// <summary>
        /// Creates an empty result. The empty result is successful, but might have issues that should be addressed or logged
        /// </summary>
        /// <returns>The created response object</returns>
        public static Response<T> Empty()
        {
            var response = new Response<T> { ResultType = ResultType.Empty };

            return response;
        }

    }

    /// <summary>
    /// A simple response object with no returned object. Just indicates successful or failed requests
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class. 
        /// </summary>
        public Response()
        {
            this.ResultType = ResultType.Success;
        }

        /// <summary>
        /// Indicates if the response contains a result
        /// </summary>
        public virtual bool HasResult
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The result of the response
        /// </summary>
        public object Result { get; protected set; }

        /// <summary>
        /// Indicates if the response is successful or not. Warning or success result type indicate success
        /// </summary>
        public bool Successful
        {
            get
            {
                return this.ResultType == ResultType.Success || this.ResultType == ResultType.Warning;
            }
        }


        /// <summary>
        /// The result type
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        /// The message returned with the response
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The validation error messages returned with the response
        /// </summary>
        public List<string> ValidationMessages { get; set; }

        /// <summary>
        /// Creates a successful response with a given result object
        /// </summary>
        /// <returns>The response object</returns>
        public static Response Success()
        {
            var response = new Response { ResultType = ResultType.Success };

            return response;
        }

        /// <summary>
        /// Creates a failed result. It takes no result object
        /// </summary>
        /// <param name="errorMessage">The error message returned with the response</param>
        /// <returns>The created response object</returns>
        public static Response Failed(string errorMessage)
        {
            var response = new Response { ResultType = ResultType.Error, Message = errorMessage };

            return response;
        }

        /// <summary>
        /// Creates a validation error response, indicating the input was invalid
        /// </summary>
        /// <param name="validationMessages">The validation message</param>
        /// <returns>The Response object</returns>
        public static Response ValidationError(List<string> validationMessages)
        {
            var response = new Response { ResultType = ResultType.ValidationError, ValidationMessages = validationMessages };

            return response;
        }

        /// <summary>
        /// Creates a warning result. The warning result is successful, but might have issues that should be addressed or logged
        /// </summary>
        /// <param name="warningMessage">The warning returned with the response</param>
        /// <returns>The created response object</returns>
        public static Response Warning(string warningMessage)
        {
            var response = new Response { ResultType = ResultType.Warning, Message = warningMessage };

            return response;
        }

        /// <summary>
        /// Creates a validation error response, indicating the input was invalid
        /// </summary>
        /// <param name="customerInformationMessage">The validation message</param>
        /// <returns>The Response object</returns>
        public static Response CustomerInformation(string customerInformationMessage)
        {
            var response = new Response
            {
                ResultType = ResultType.CustomerInformation,
                Message = customerInformationMessage
            };

            return response;
        }

        /// <summary>
        /// Creates an empty result. The empty result is successful, but might have issues that should be addressed or logged
        /// </summary>
        /// <returns>The created response object</returns>
        public static Response Empty()
        {
            var response = new Response { ResultType = ResultType.Empty };

            return response;
        }
    }
}
