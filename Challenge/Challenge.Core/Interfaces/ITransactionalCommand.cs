namespace Challenge.Core.Interfaces;

/// <summary>
/// Marker interface para indicar que un comando requiere transacción de base de datos.
/// Los comandos que implementen esta interfaz serán ejecutados dentro de una transacción automáticamente
/// por el TransactionBehavior de MediatR.
/// </summary>
public interface ITransactionalCommand
{
}
