﻿namespace Application.Request
{
    public class ReservaGetRequest
    {
        public int NumeroAsiento { get; set; }
        public string Clase { get; set; }
        public int Precio { get; set; }
        public int ViajeId { get; set; }
        public int PasajeroId { get; set; }
    }
}
