using Helpers.ObjectsUtils.Extension;

namespace Helpers.Commons.Exceptions
{
    public class BusinessException : Exception
    {
        /// <summary>
        ///   Inicializa una nueva instancia de <see cref="BusinessException"/>.
        /// </summary>
        /// <param name="tipoException">el tipo exception.</param>
        public BusinessException(TypeBusinessException tipoException) : base(tipoException.GetDescription())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="tipoException">The tipo exception.</param>
        /// <param name="data">The data.</param>
        public BusinessException(TypeBusinessException tipoException, params object[] data) : base(string.Format(tipoException.GetDescription(), data))
        {
        }
    }
}