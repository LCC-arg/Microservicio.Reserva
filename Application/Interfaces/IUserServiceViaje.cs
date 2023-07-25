namespace Application.Interfaces
{
    public interface IUserServiceViaje
    {
        dynamic ObtenerViaje(int viajeId);
        public dynamic ObtenerPasajero(int pasajeroId);
        dynamic ModificarViaje(int viajeId, int asientosDisponibles);
    }
}
