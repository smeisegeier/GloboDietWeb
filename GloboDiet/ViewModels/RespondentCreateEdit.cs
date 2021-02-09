using GloboDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.ViewModels
{
    public class RespondentCreateEdit : _ViewModelBase
    {
        private Respondent _respondent { get; set; }

        public RespondentCreateEdit(Respondent respondent, NavigationBar navigationBar, Globals.ProcessMilestone currentProcessMilestone)
        {
            _respondent = respondent;
            NavigationBar = navigationBar;
            CurrentProcessMilestone = currentProcessMilestone;
        }

        public RespondentCreateEdit() {}

        public int Id { get => _respondent.Id; }

        public string Label { get => _respondent.Label; }

        public int InterviewId
        {
            get => _respondent.InterviewId;
            set { _respondent.InterviewId = value; }
        }

        public string Name
        {
            get => _respondent.Name;
            set { _respondent.Name = value; }
        }
        public string Code
        {
            get => _respondent.Code;
            set { _respondent.Code = value; }
        }

        public string GivenName
        {
            get => _respondent.GivenName;
            set { _respondent.GivenName = value; }
        }

        public Gender Gender
        {
            get => _respondent.Gender;
            set { _respondent.Gender= value; }
        }

        public DateTime DateOfBirth
        {
            get => _respondent.DateOfBirth;
            set { _respondent.DateOfBirth = value; }
        }

        public double Age
        {
            get => _respondent.Age;
        }

        public int Height
        {
            get => _respondent.Height;
            set { _respondent.Height = value; }
        }
        public int Weight
        {
            get => _respondent.Weight;
            set { _respondent.Weight = value; }
        }
    }
}
