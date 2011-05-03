using System;
using System.Collections.Generic;
using System.Linq;
using Evil.Common;

namespace Evil.Lairs
{
    public class Lair : Entity
    {
        private List<Section> _sections;

        public virtual Position Location { get; set; }
        public virtual string Name { get; set; }

        public virtual int EmptySectionsCount()
        {
            return MaxSections - Sections.Count();
        }

        protected virtual int MaxSections { get { return 1; } }

        public virtual DateTime? UpgradeFinished { get; private set; }

        public virtual bool IsUpgrading()
        {
            return UpgradeFinished != null && UpgradeFinished > DateTime.Now;
        }

        public virtual bool CanUpgrade()
        {
            return !IsUpgrading();
        }

        public virtual IEnumerable<Section> Sections
        {
            get { return _sections; }
        }

        public virtual int CurrentLevel { get; set; }

        public virtual int GetMaxLevel()
        {
            return 20;
        }

        public Lair()
        {
            _sections = new List<Section>();
            CurrentLevel = 1;
        }

        public virtual void AddSection(Section section)
        {
            _sections.Add(section);
        }

        public virtual TimeSpan BeginUpgrade()
        {
            if(CurrentLevel >= GetMaxLevel())
                throw new InvalidOperationException("Already at max level.");
            var upgradeTime =  new TimeSpan(1,0,0);
            UpgradeFinished = DateTime.Now.Add(upgradeTime);
            return upgradeTime;
        }

        public virtual void CompleteUpgrade()
        {
            UpgradeFinished = null;
            CurrentLevel++;
        }
    }
}