using Domain.Entities.BaseEntities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        private PlanLevel _level;

        public PlanLevel Level
        {
            get => _level;
            set
            {
                _level = value;
                SetValuesByLevel();
            }
        }

        public decimal Value { get; private set; }
        public int ProfessionalNumberLimit { get; private set; }
        public IList<Enterprise> Enterprises { get; set; } = new List<Enterprise>();

        private void SetValuesByLevel()
        {
            switch (_level)
            {
                case PlanLevel.Basic:
                    Value = 79.90m;
                    ProfessionalNumberLimit = 5;
                    break;

                case PlanLevel.Intermediary:
                    Value = 99.90m;
                    ProfessionalNumberLimit = 30;
                    break;

                case PlanLevel.Advanced:
                    Value = 119.90m;
                    ProfessionalNumberLimit = 200;
                    break;

                default:
                    break;
            }
        }
    }
}
