using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiScrapingData.Domain.Body
{
    public class Research
    {
        public List<TypePokR>? TypePok { get; set; }
        public List<TypeLearnR>? TypeLearn { get; set; }
        public List<TalentPokR>? TalentPok { get; set; }
        public bool Hidden { get; set; }
        public Interval? Interval { get; set; }
        public List<Height>? Height { get; set; }
        public List<Weight>? Weight { get; set; }
    }

    public class TypeLearnR
    {
        public string Code { get; set; }

        public TypeLearnR(string code)
        {
            Code = code;
        }
    }

    public class TalentPokR
    {
        public string Code { get; set; }

        public TalentPokR(string code)
        {
            Code = code;
        }
    }

    public class TypePokR
    {
        public string Code { get; set; }
        public string CodeType { get; set; }

        public TypePokR(string code, string codeType)
        {
            Code = code;
            CodeType = codeType;
        }
    }

    public class Interval
    {
        public int First { get; set; }
        public int Last { get; set; }
        
        public Interval(int first, int last)
        {
            First = first;
            Last = last;
        }
    }

    public class Height
    {
        // Small From 0 to 0.9m (2'11")

        // Normal From 1.0m to 1.8m (from 3'03" to 5'11")

        // Big 1.9m (6'03")
        public string Code { get; set; }

        public Height(string code)
        {
            Code = code;
        }
    }

    public class Weight : Code
    {
        // Small From 0 to 24,9 kg (from 0 to 54.9 lbs)

        // Normal From 25 to 99,9 kg (from 55.1 to 220.2 lbs)

        // Big 100 kg (220.5 lbs)

        public Weight(string code)
        {
            id = code;
        }
    }

    public class Code
    {
        public string? id { get; set; }
    }
}
