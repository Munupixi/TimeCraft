using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    internal class Participant : IEntity
    {
        public int ParticipantId { get; set; }
        public int IdEvent { get; set; }
        public int IdUser { get; set; }
        public bool IsAccepted { get; set; }
        public string Role { get; set; }

        public Participant(int participantId, int idEvent, int idUser,
            bool isAccepted = false, string role = "Нет")
        {
            ParticipantId = participantId;
            IsAccepted = isAccepted;
            Role = role;
            IdEvent = idEvent;
            IdUser = idUser;
        }
        public void Delete()
        {
            return;
        }
        public void Delete(int eventId)
        {
            return;
        }
        public static void DeleteAllByEventId(int eventId)
        {
            using (AppDBContent db = new AppDBContent())
            {
                db.Participant.RemoveRange(
                    db.Participant.Where(p => p.IdEvent == eventId));
                db.SaveChanges();
            }
        }
        public void Add()
        {
            using (AppDBContent db = new AppDBContent())
            {
                db.Participant.Add(this);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (AppDBContent db = new AppDBContent())
            {
                try
                {
                    db.Participant.Update(this);
                }
                catch
                {
                    throw new Exception("Неудалось сохранить изменения");
                }
                db.SaveChanges();
            }
        }
        public static int GetNewId()
        {
            using (AppDBContent db = new AppDBContent())
            {
                return (db.Participant.Max(u => (int?)u.ParticipantId) ?? 0) + 1;
            }
        }
        public static List<Participant> GetAllParticipantByIdEvent(int idEvent)
        {
            using (AppDBContent db = new AppDBContent())
            {
                return db.Participant.Where(p => p.IdEvent == idEvent).ToList();
            }
        }
    }
}