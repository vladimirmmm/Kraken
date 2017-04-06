using BaseModel;
using LogicalModel;
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



    }
    public class DimensionRelationShip : DimensionFilter
    {

        private QName _Member = new QName("");
        public QName Member { get { return _Member; } set { _Member = value; } }

  
        private String _LinkRole = "";
        public String LinkRole { get { return _LinkRole; } set { _LinkRole = value; } }

        //private String _ArcRole = "";
        //public String ArcRole { get { return _ArcRole; } set { _ArcRole = value; } }


        public string Axis { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(" - {");

            sb.Append(Member.Content + ", ");
        
            sb.Append("}");

            return String.Format("{0}", sb.ToString());
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
        
        private void Handle(bool result,string factstring,string tag,string operation) 
        {
            if (!result) 
            {

            }
        }

        public override FactBaseQuery GetQuery(Taxonomy taxonomy, Hierarchy<XbrlIdentifiable> currentfilter, FactBaseQuery parent)
        {
            //Console.WriteLine(String.Format("ExplicitDimensionFilter.GetQuery( {0} ) ", currentfilter.Item));

            var _complement = Complement;
            var factsofparts = taxonomy.FactsOfParts;
            var factparts = taxonomy.FactParts;
            var resultquery = new FactBaseQuery();
            //var querycontainer = parent;
            if (Members.Count == 0) 
            {
                var dimkey = String.Format("{0}:{1}", this.Dimension.QName.Domain, this.Dimension.QName.Value);

                var domains = taxonomy.DomainsofDimensions.ContainsKey(dimkey) ? taxonomy.DomainsofDimensions[dimkey] : new List<string>();
                var firstdomain = domains.FirstOrDefault();
                if (firstdomain != null) 
                {
                    var members = taxonomy.GetMembersOf(firstdomain).Where(i => !i.IsDefaultMember).ToList();                   
                    this.Members=members.Select(i=>{
                        var dm = new DimensionMember();
                        dm.QName.Content = i.Content;
                        dm.IsDefaultMember=i.IsDefaultMember;
                        return dm;
                    }).ToList();
                }
            }
            var firstmemebr = Members.FirstOrDefault();
            var domaintag = String.Format("[{0}:{1}]{2}", this.Dimension.QName.Domain, this.Dimension.QName.Value, firstmemebr.QName.Domain);
            //FactBaseQuery resultquery = new FactBaseQuery();    
            var querycontainer = resultquery;
            var poolneeded = Members.Count > 1 || (Members.Count == 1 && firstmemebr.IsDefaultMember && this.Complement);
            if (poolneeded) 
            {
                querycontainer = new FactPoolQuery();
                resultquery.AddChildQuery(querycontainer);
                //resultquery.DictFilterIndexes.Add(factparts[domaintag]);

                //querycontainer = resultquery;
            }
            foreach (var member in Members)
            {
                var membertag = String.Format("[{0}:{1}]{2}", this.Dimension.QName.Domain, this.Dimension.QName.Value, member.QName.Content);
                if (!member.IsDefaultMember)
                {

                    if (!_complement)
                    {
                        if (factparts.ContainsKey(membertag))
                        {
                            var xquery = querycontainer;
                            if (xquery is FactPoolQuery) 
                            {
                                var fbq = new FactBaseQuery();
                                xquery.AddChildQuery(fbq);
                                xquery = fbq;
                            }
                            xquery.DictFilterIndexes.Add(factparts[membertag]);
                        }
                    }
                    else
                    {
                        if (factparts.ContainsKey(membertag))
                        {
                            querycontainer.NegativeDictFilterIndexes.Add(factparts[membertag]);
                        }
                    }
                }
                else
                {
                    //default member
                    if (!_complement)
                    {
                        if (factparts.ContainsKey(domaintag))
                        {
                             var xquery = querycontainer;
                             if (xquery is FactPoolQuery)
                             {
                                 var fbq = new FactBaseQuery();
                                 xquery.AddChildQuery(fbq);
                                 xquery = fbq;
                             }
                             xquery.NegativeDictFilterIndexes.Add(factparts[domaintag]);
                        }
                    }
                    else
                    {
                        //default member with complement
                        if (Members.Count == 1)
                        {
                            var tag = String.Format("[{0}:{1}]", this.Dimension.QName.Domain, this.Dimension.QName.Value);

                            var domain = member.QName.Domain;
                            var members = taxonomy.GetMembersOf(domain, this._Members.Select(i => i.QName.Content).ToList());                       

                            foreach (var memberitem in members)
                            {
                                var mfbq = new FactBaseQuery();
                                var memberdictfilter = tag + memberitem;

                                //FP if (taxonomy.FactsOfDimensions.ContainsKey(memberdictfilter))
                                var ix = taxonomy.FactParts[memberdictfilter];
                                if (taxonomy.FactsOfParts.ContainsKey(ix))
                                {
                                    mfbq.DictFilters = memberdictfilter + ", ";
                                    if (factparts.ContainsKey(memberdictfilter))
                                    {
                                        mfbq.DictFilterIndexes.Add(factparts[memberdictfilter]);
                                    }
                                    querycontainer.AddChildQuery(mfbq);
                                }

                            }
                            //return resultquery;
                        }
                        else
                        {
                            if (factparts.ContainsKey(domaintag))
                            {
                                querycontainer.DictFilterIndexes.Add(factparts[domaintag]);
                            }
                        }


                    }

                }     
            }



            SetCover(resultquery);
     

            return resultquery;
        }
     
    }

    public class TypedDimensionFilter : DimensionFilter
    {
        private String _Test = "";
        public String Test { get { return _Test; } set { _Test = value; } }
        public override FactBaseQuery GetQuery(Taxonomy taxonomy, Hierarchy<XbrlIdentifiable> currentfilter, FactBaseQuery parent)
        {
            //Console.WriteLine(String.Format("TypedDimensionFilter.GetQuery( {0} ) ", currentfilter.Item));

            var query = new FactBaseQuery();
            var factparts = taxonomy.FactParts;
          
            var typeddomain = taxonomy.FindDimensionDomain(this.Dimension.QName.Content);

            // var td = taxonomy.TypedDimensions.FirstOrDefault(i => i.Key.EndsWith(":" + this.Dimension.QName.Value));
            var domainpart = typeddomain.Namespace + ":" + typeddomain.Name;
            var tag = String.Format("[{0}:{1}]{2}", this.Dimension.QName.Domain, this.Dimension.QName.Value, domainpart);
            if (!Complement)
            {
                //var tag = String.Format(":{0}]", this.Dimension.QName.Value);

                query.TrueFilters = query.TrueFilters + String.Format("{0}, ", tag);
                if (factparts.ContainsKey(tag))
                {
                    query.DictFilterIndexes.Add(factparts[tag]);
                }


            }
            else
            {
                //var tag = String.Format(":{0}]", this.Dimension.QName.Value);

                query.FalseFilters = query.FalseFilters + String.Format("{0} ", tag);
                if (factparts.ContainsKey(tag))
                {
                    query.NegativeDictFilterIndexes.Add(factparts[tag]);
                }
      
            }

            SetCover(query);

            return query;
        }
        /*
        public override List<FactBaseQuery> GetQueries(LogicalModel.Taxonomy taxonomy, int level = 0)
        {
            var queries = new List<FactBaseQuery>();
            var query = new FactBaseQuery();
            var factparts = taxonomy.FactParts;
            queries.Add(query);
            var typeddomain = taxonomy.FindDimensionDomain(this.Dimension.QName.Content);
     
           // var td = taxonomy.TypedDimensions.FirstOrDefault(i => i.Key.EndsWith(":" + this.Dimension.QName.Value));
            var domainpart = typeddomain.Namespace + ":" + typeddomain.Name;
            var tag = String.Format("[{0}:{1}]{2}", this.Dimension.QName.Domain, this.Dimension.QName.Value, domainpart);
            if (!Complement)
            {
                //var tag = String.Format(":{0}]", this.Dimension.QName.Value);

                query.TrueFilters = query.TrueFilters + String.Format("{0}, ", tag);
                if (factparts.ContainsKey(tag))
                {
                    query.DictFilterIndexes.Add(factparts[tag]);
                }
                query.Filter = (string s) =>
                {
                    var ok = s.IndexOf(tag, StringComparison.Ordinal) > -1;
                    if (ok)
                    {

                    }
                    return ok;
                };

            }
            else
            {
                //var tag = String.Format(":{0}]", this.Dimension.QName.Value);

                query.FalseFilters = query.FalseFilters + String.Format("{0} ", tag);
                if (factparts.ContainsKey(tag))
                {
                    query.NegativeDictFilterIndexes.Add(factparts[tag]);
                }
                query.Filter = (s) =>
                {
                    var ok = s.IndexOf(tag, StringComparison.Ordinal) == -1;
                    if (ok)
                    {

                    }
                    return ok;
                };
            }

            SetCover(queries);

            return queries;
        }
   */
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
        public bool IsDefaultMember { get; set; }
       
    }
}
