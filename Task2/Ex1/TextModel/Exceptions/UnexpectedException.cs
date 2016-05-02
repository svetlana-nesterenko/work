namespace TextModel.Exceptions
{
    #region Usings

    using System;

    #endregion

    /// <summary>
    /// Class represents any kind of exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UnexpectedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedException"/> class.
        /// </summary>
        public UnexpectedException() : base()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnexpectedException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public UnexpectedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
