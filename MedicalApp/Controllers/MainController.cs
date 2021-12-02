using MedicalApp.Data;
using MedicalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : Controller
    {
        private NpgsqlDbContext _ctx;

        public MainController(NpgsqlDbContext ctx)
        {
            _ctx = ctx;
        }

   
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPatients()
        {
            var cards = _ctx.Patients.ToList();

            return Ok(cards);
        }

     
        [HttpGet("[action]/{id}")]
        public ActionResult<IEnumerable<Visit>> GetMedicalCardById(int id)
        {
            var visit = _ctx.Visits.Where(x => x.MC_ID == id).Select(t => new Visit
            {
                MC_ID = t.MC_ID,
                VisitDate = t.VisitDate,
                Diagnosis = t.Diagnosis,
                Сomplaints = t.Сomplaints
            }).ToList();

            return visit;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddPatient([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }

            var newPatient = new Patient
            {
                FIO = patient.FIO,
                IIN = patient.IIN,
                Address = patient.Address,
                Phone = patient.Phone
            };

            await _ctx.Patients.AddAsync(newPatient);
            await _ctx.SaveChangesAsync();

            var newMedicalCard = new MedicalCard
            {
                Patient = newPatient,
                PatientId = newPatient.PatientId
            };

            await _ctx.MedicalCards.AddAsync(newMedicalCard);
            await _ctx.SaveChangesAsync();


            return Ok(new JsonResult("Добавлен новый пациент и его медицинская карта!!!"));

        }


        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdatePatientInfo(int id, [FromBody] Patient patient)
        {
            var findPatient = _ctx.Patients.FirstOrDefault(p => p.PatientId == id);

            if (findPatient == null)
            {
                return NotFound();
            }

            findPatient.FIO = patient.FIO;
            findPatient.IIN = patient.IIN;
            findPatient.Address = patient.Address;
            findPatient.Phone = patient.Phone;

            _ctx.Entry(findPatient).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult("Пациент с id " + id + " обновил свои данные"));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddPatientVisit([FromBody] Visit visit)
        {
            if (visit == null)
            {
                return BadRequest();
            }

            var newVisit = new Visit
            {
                VisitDate = visit.VisitDate,
                MC_ID = visit.MC_ID,
                DocFIO = visit.DocFIO,
                DocSpec = visit.DocSpec,
                Diagnosis = visit.Diagnosis,
                Сomplaints = visit.Сomplaints
            };

            await _ctx.Visits.AddAsync(newVisit);
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult("Добавлено новое посещение у врача!!!"));
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateVisitInfo(int id, [FromBody] Visit visit)
        {
            var findVisit = _ctx.Visits.FirstOrDefault(v => v.VisitId == id);

            if (findVisit == null)
            {
                return NotFound();
            }

            findVisit.DocFIO = visit.DocFIO;
            findVisit.DocSpec = visit.DocSpec;
            findVisit.VisitDate = visit.VisitDate;
            findVisit.Сomplaints = visit.Сomplaints;
            findVisit.Diagnosis = visit.Diagnosis;

            _ctx.Entry(findVisit).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult("Пациент с id " + id + " обновил свои данные"));
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var findPatient = _ctx.Patients.FirstOrDefault(p => p.PatientId == id);

            if (findPatient == null)
            {
                return NotFound();
            }

            _ctx.Patients.Remove(findPatient);
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult("Удален пациент пациент и его медицинская карта!!!"));
        }

        
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteVisit(int visitId)
        {
            var findVisit = _ctx.Visits.FirstOrDefault(v => v.VisitId == visitId);

            if (findVisit == null)
            {
                return NotFound();
            }

            _ctx.Visits.Remove(findVisit);
            await _ctx.SaveChangesAsync();

            return Ok(new JsonResult("Удален визит!!!"));
        }
    }
}
