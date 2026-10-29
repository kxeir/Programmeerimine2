namespace KooliProjekt.Application.Infrastructure.Results
{
    public class OperationResult<T> : OperationResult
    {
        public T Value { get; set; }

        public OperationResult() { }

        public OperationResult(T value) 
        { 
            Value = value;
        }        

        public static OperationResult<T> Success(T value)
        {
            return new OperationResult<T>(value);
        }

        public static OperationResult<T> NotFound(string error = "The requested resource was not found.")
        {
            return new OperationResult<T>().AddError(error);
        }

        public new OperationResult<T> AddError(string error)
        {
            base.AddError(error);

            return this;
        }

        public new OperationResult<T> AddPropertyError(string propertyName, string error)
        {
            base.AddPropertyError(propertyName, error);

            return this;
        }
    }
}