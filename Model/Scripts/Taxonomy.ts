module Control {
    export class TaxonomyContainer
    {
        public Taxonomy: Model.Taxonomy = new Model.Taxonomy();

        constructor()
        {
            //this.SetExternals(this.Taxonomy);
        }

        public SetExternals(Taxonomy: Model.Taxonomy) {
            if (IsNull(Taxonomy))
            {
                Taxonomy = this.Taxonomy;
            }
            Taxonomy.Concepts = window[var_tax_concepts] == null ? [] : window[var_tax_concepts];
            Taxonomy.ValidationRules = window[var_tax_validations] == null ? [] : window[var_tax_validations];
            //Taxonomy.Concepts = this.Concepts.AsLinq<Model.Concept>().Where(i=> i.Domain != null).ToArray();
            Taxonomy.Hierarchies = window[var_tax_hierarchies] == null ? [] : window[var_tax_hierarchies];
            //Taxonomy.FactMap = window[var_tax_facts] == null ? [] : window[var_tax_facts];
            Taxonomy.Labels = window[var_tax_labels] == null ? [] : window[var_tax_labels];

        }
    }
}

var taxonomycontainer = new Control.TaxonomyContainer();