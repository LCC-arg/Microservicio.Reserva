namespace Application.Interfaces
{
    public interface IUserServiceViaje
    {
        dynamic ObtenerViaje(int viajeId);
        dynamic ModificarViaje(int viajeId, int asientosDisponibles);
    }
}
