﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TimeCraft.ViewModels
{
    internal class EventViewModel : Activity, INotifyPropertyChanged
    {
        private Event _event;
        private DataBaseContent _content;

        public event PropertyChangedEventHandler PropertyChanged;

        public static event EventHandler EventsUpdated;

        public EventViewModel(Event _event)
        {
            this._event = _event;
            _content = new DataBaseContent();
        }

        public void Delete()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                if (db.Event.Find(_event.EventId) != null)
                {
                    db.Event.Remove(_event);
                    db.SaveChanges();
                    EventsUpdated?.Invoke(this, EventArgs.Empty);
                    return;
                }
                throw new Exception("Возникли проблемы с удалением мероприятия");
            }
        }

        public static void Delete(int eventId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                Event _event = db.Event.Find(eventId);
                if (_event == null)
                {
                    return;
                }
                db.Event.Remove(_event);
                db.SaveChanges();
                EventsUpdated?.Invoke(null, EventArgs.Empty);
            }
        }

        public void Add()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                db.Event.Add(_event);
                db.SaveChanges();
                EventsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Update()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                try
                {
                    db.Event.Update(_event);
                    db.SaveChanges();
                    EventsUpdated?.Invoke(this, EventArgs.Empty);
                }
                catch
                {
                    throw new Exception("Неудалось сохранить изменения");
                }
            }
        }

        public static int GetNewId()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return (db.Event.Max(u => (int?)u.EventId) ?? 0) + 1;
            }
        }

        public static Event Get(int eventId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.FirstOrDefault(e => e.EventId == eventId);
            }
        }

        public static Event Get(string title)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.FirstOrDefault(e => e.Title == title);
            }
        }

        public static Event Get(DateTime? startDate, TimeSpan? startTime, DateTime? endDate, TimeSpan? endTime)
        {
            if (startDate == null || endDate == null || startTime == null || endTime == null)
            {
                return null;
            }

            DateTime? startDateTime = startDate + startTime;
            DateTime? endDateTime = endDate + endTime;
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.FirstOrDefault(e =>
                    (e.StartDate < endDateTime && e.EndDate > startDateTime) || // Если начало события в пределах диапазона или конец события в пределах диапазона
                    (e.StartDate >= startDateTime && e.EndDate <= endDateTime) || // Если событие полностью внутри указанного диапазона
                    (e.StartDate <= startDateTime && e.EndDate >= endDateTime));  // Если указанный диапазон полностью внутри события
            }
        }

        public static List<Event> GetAllMine(int userId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.Where(_event => _event.UserId == userId).ToList();
            }
        }
        public static List<Event> GetAllInvitedEvents(int userId)
        {
            List<Event> invitedEvents = new List<Event>();
            List<Event> mineEvents = GetAllMine(userId);

            List<Event> events;
            Dictionary<int, List<int>> participantsByEventId = new Dictionary<int, List<int>>();
            using (DataBaseContent db = new DataBaseContent())
            {
                events = db.Event.ToList();
                foreach (Event e in events)
                {
                    participantsByEventId.Add(
                        e.EventId, db.Participant
                            .Where(p => p.IdEvent == e.EventId)
                            .Select(p => p.IdUser)
                            .ToList());
                }
            }
            foreach (Event e in events)
            {
                if (participantsByEventId[e.EventId].Contains(userId) &&
                    !mineEvents.Any(ev => ev.EventId == e.EventId))
                {
                    invitedEvents.Add(e);
                }
            }

            return invitedEvents;
        }



        public static List<Event> GetAllMineAndInvited(int userId)
        {
            List<Event> mineAndInvitedEvents = new List<Event>();
            mineAndInvitedEvents.AddRange(GetAllMine(userId));
            mineAndInvitedEvents.AddRange(GetAllInvitedEvents(userId));
            return mineAndInvitedEvents;
        }

        public static List<Event> GetFilterEventsBySearch(List<Event> events, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return events;
            }
            return events.Where(e => e.Title.Contains(search)).ToList();
        }

        public static Dictionary<DayOfWeek, List<Event>> GetFilterEventsBySearch(Dictionary<DayOfWeek, List<Event>> eventsForWeek, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return eventsForWeek;
            }
            var filteredEventsForWeek = new Dictionary<DayOfWeek, List<Event>>();

            foreach (var kvp in eventsForWeek)
            {
                var filteredEvents = kvp.Value.Where(e => e.Title.Contains(search)).ToList();
                filteredEventsForWeek.Add(kvp.Key, filteredEvents);
            }

            return filteredEventsForWeek;
        }

        public static Dictionary<int, List<Event>> GetFilterEventsBySearch(Dictionary<int, List<Event>> eventsForMonth, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return eventsForMonth;
            }
            var filteredEventsForMonth = new Dictionary<int, List<Event>>();

            foreach (var kvp in eventsForMonth)
            {
                var filteredEvents = kvp.Value.Where(e => e.Title.Contains(search)).ToList();
                filteredEventsForMonth.Add(kvp.Key, filteredEvents);
            }

            return filteredEventsForMonth;
        }

        public static List<Event> GetAllMineAndInvitedByDateForDay(int userId, DateTime date)
        {
            return GetAllMineAndInvited(userId)
                .Where(e => e.StartDate <= date && e.EndDate >= date)
                .ToList();
        }

        public static Dictionary<DayOfWeek, List<Event>> GetAllMineAndInvitedByDateForWeek(int userId, DateTime firstDayOfWeek)
        {
            Dictionary<DayOfWeek, List<Event>> eventsForWeek = new Dictionary<DayOfWeek, List<Event>>();
            foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                eventsForWeek[dayOfWeek] = GetAllMineAndInvitedByDateForDay(userId, firstDayOfWeek);
                firstDayOfWeek = firstDayOfWeek.AddDays(1);
            }
            return eventsForWeek;
        }

        public static Dictionary<int, List<Event>> GetAllMineAndInvitedByDateForMonth(int userId, DateTime firstDayOfMonth)
        {
            Dictionary<int, List<Event>> eventsForMonth = new Dictionary<int, List<Event>>();

            for (int day = 1; day <= DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month); day++)
            {
                List<Event> eventsForDay = GetAllMineAndInvitedByDateForDay(userId, new DateTime(firstDayOfMonth.Year, firstDayOfMonth.Month, day));
                eventsForMonth[day] = eventsForDay;
            }

            return eventsForMonth;
        }


        public bool IsTitleUnique()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return !db.Event.Any(e => e.Title == _event.Title);
            }
        }

        public bool IsParticipant(int userId)
        {
            return ParticipantViewModel.GetAllParticipantByIdEvent(_event.EventId)
                .Any(participant => participant.ParticipantId == userId);
        }

        public bool IsEventExists(int eventId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.Any(e => e.EventId == eventId);
            }
        }
    }
}