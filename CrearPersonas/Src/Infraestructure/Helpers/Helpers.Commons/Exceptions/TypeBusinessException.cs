using System.ComponentModel;

namespace Helpers.Commons.Exceptions
{
    /// <summary>
    /// ResponseError
    /// </summary>
    public enum TypeBusinessException
    {
        [Description("Excepción de negocio no controlada")]
        ExceptionNoControlada = 001,

        [Description("No se pudo descargar el archivo o hubo error de conexión")]
        FallaObtenerArchivo = 002,

        [Description("No se pudo cargar el archivo al servidor")]
        FallaCargarArchivo = 004,

        [Description("La variable {0} no puede estar vacía, es necesaria para resolver el nivel de logs")]
        MinimumLevelEmpty = 005,

        [Description("No fue posible establecer una conexión con el servidor, se realizaron {0} intentos")]
        ErrorServerConection = 007,

        [Description("No se pudo consultar la información en la base de datos")]
        ErrorConsulta = 009
    }
}