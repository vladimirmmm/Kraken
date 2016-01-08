using LogicalModel.Base;
using Model.DefinitionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBRLProcessor.Model.Base;
using XBRLProcessor.Model.StringEnums;

namespace XBRLProcessor.Model.DefinitionModel.Filter
{
    public class DimensionFilter:Filter 
    {
        public DimensionQName Dimension { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(" >> ");
            sb.Append(this.Dimension.QName.Content);
            return sb.ToString();
        }


        public override Func<string, bool> GetFunc(FactBaseQuery fbq)
        {
            return (fs) => true;
        }
    }
    public class ExplicitDimensionFilter : DimensionFilter
    {
        private List<DimensionMember> _Members = new List<DimensionMember>();
        public List<DimensionMember> Members { get { return _Members; } set { _Members = value; } }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(" - {");
            foreach (var member in Members)
            {
                sb.Append(member.QName.Content + ", ");
            }
            sb.Append("}");

            return String.Format("{0}", sb.ToString());
        }
        public override Func<string, bool> GetFunc(FactBaseQuery fbq)
        {
            Func<string, bool> f = null;
            List<Func<string, bool>> functions = new List<Func<string, bool>>();


            foreach (var member in Members)
            {
                if (!Complement)
                {
                    fbq.TrueFilters = fbq.TrueFilters + String.Format("[{0}]{1}, ", this.Dimension.QName.Content, member.QName.Content);
                    functions.Add((s) =>
                    {
                        return s.Contains("]" + member.QName.Content + ",");
                    });

                }
                else
                {
                    fbq.FalseFilters = fbq.FalseFilters + String.Format("[{0}]{1}, ", this.Dimension.QName.Content, member.QName.Content);
                    functions.Add((s) =>
                    {
                        return !s.Contains("]" + member.QName.Content + ",");
                    });
                }

            }


            f = (s) => functions.All(fx => fx(s));

            return f;
        }

        public override List<FactBaseQuery> GetQueries()
        {
            var queries = new List<FactBaseQuery>();
            foreach (var member in Members)
            {
                var query = new FactBaseQuery();
                queries.Add(query);
                if (member.QName.Value != Literals.Literal.Defaultmember)
                {
                    

                    if (!Complement)
                    {
                        var tag = String.Format(":{0}]{1},", this.Dimension.QName.Value, member.QName.Content);

                        query.TrueFilters = query.TrueFilters + String.Format("{0} ", tag);
                        query.Filter = (s) =>
                        {
                            var ok = s.Contains(tag);
                            if (ok)
                            {

                            }
                            return ok;
                        };

                    }
                    else
                    {
                        var tag = String.Format(":{0}]{1},", this.Dimension.QName.Value, member.QName.Content);

                        query.FalseFilters = query.FalseFilters + String.Format("{0} ", tag);
                        query.Filter = (s) =>
                        {
                            var ok = !s.Contains(tag);
                            if (ok)
                            {

                            }
                            return ok;
                        };
                    }
                }
                else 
                {
                    var tag = String.Format(":{0}]{1}:", this.Dimension.QName.Value, member.QName.Domain);
                    query.FalseFilters = query.FalseFilters + String.Format("{0}, ", tag);
                    query.Filter = (s) =>
                    {

                        var ok = !s.Contains(tag);
                        if (ok)
                        {

                        }
                        return ok;
                    };
                }
            }


            return queries;
        }
    }

    public class TypedDimensionFilter : DimensionFilter
    {
        private String _Test = "";
        public String Test { get { return _Test; } set { _Test = value; } }

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();
        //    sb.Append(this.Dimension.QName.Content);
        //    return sb.ToString();
        //}
    }

    public class DimensionQName 
    {
        private QName _QName = new QName("");
        public QName QName { get { return _QName; } set { _QName = value; } }

        private String _QNameExpression = "";
        public String QNameExpression { get { return _QNameExpression; } set { _QNameExpression = value; } }
    }

    public class DimensionMember
    {
        private QName _Variable = new QName("");
        public QName Variable { get { return _Variable; } set { _Variable = value; } }

        private QName _QName = new QName("");
        public QName QName { get { return _QName; } set { _QName = value; } }

        private String _QNameExpression = "";
        public String QNameExpression { get { return _QNameExpression; } set { _QNameExpression = value; } }

        private String _LinkRole = "";
        public String LinkRole { get { return _LinkRole; } set { _LinkRole = value; } }

        private String _ArcRole = "";
        public String ArcRole { get { return _ArcRole; } set { _ArcRole = value; } }


        public string Axis { get; set; }


    }
}
