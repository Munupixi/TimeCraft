using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;

namespace TimeCraft.ViewModels
{
    internal class ParticipantViewModel : INotifyPropertyChanged
    {
        private Participant _participant;

        public event PropertyChangedEventHandler PropertyChanged;

        public ParticipantViewModel(Participant participant)
        {
            _participant = participant;
        }

        public static void DeleteAllByEventId(int eventId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                db.Participant.RemoveRange(
                    db.Participant.Where(p => p.IdEvent == eventId));
                db.SaveChanges();
            }
        }

        public void Add()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                db.Participant.Add(_participant);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                try
                {
                    db.Participant.Update(_participant);
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
            using (DataBaseContent db = new DataBaseContent())
            {
                return (db.Participant.Max(u => (int?)u.ParticipantId) ?? 0) + 1;
            }
        }

        public static List<Participant> GetAllParticipantByIdEvent(int idEvent)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Participant.Where(p => p.IdEvent == idEvent).ToList();
            }
        }
    }
}