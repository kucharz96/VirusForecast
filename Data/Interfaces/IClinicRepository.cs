using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Models;

namespace VirusForecast.Data.Interfaces
{
    public interface IClinicRepository
    {
        /// <summary>
        /// Pobranie listy dostępnych placówek.
        /// </summary>
        /// <returns>Lista placówek.</returns>
        List<Clinic> GetAll();

        /// <summary>
        /// Pobranie placówki na podstawie id.
        /// </summary>
        /// <param name="id">Identyfikator placówki.</param>
        /// <returns>WYbrana placówka.</returns>
        Clinic Get(string id);

        /// <summary>
        /// Pobranie placówki na podstawie nazwy.
        /// </summary>
        /// <param name="name">Nazwa placówki</param>
        /// <returns>Wybrana placówka.</returns>
        //Clinic Get(string name);

        /// <summary>
        /// Edytowanie wybranej placówki.
        /// </summary>
        /// <param name="id">Identyfikator placówki.</param>
        /// <param name="new_name">Nowa nazwa placówki.</param>
        /// <returns>Zmieniona placówka.</returns>
        Clinic Edit(string id, string new_name);

        /// <summary>
        /// Usuwanie placówki.
        /// </summary>
        /// <param name="id">Identyfikator placówki do usunięcia.</param>
        /// <returns>Usunięta placówka.</returns>
        Clinic Delete(string id);

        /// <summary>
        /// Dodanie placówki o zadanej nazwie.
        /// </summary>
        /// <param name="name">Nazwa placówki.</param>
        void Add(string name);

        string GetClinicName(string id);

        IEnumerable<Clinic> GetDoctorsClinics(string doctorId);
    }
}
