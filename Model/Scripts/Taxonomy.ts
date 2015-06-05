module Control {
    export class TaxonomyContainer
    {
        public Taxonomy: Model.Taxonomy = new Model.Taxonomy();

        public Instance: Model.Instance = null;
        constructor()
        {
            this.SetExternals(this.Taxonomy);
        }

        public SetExternals(Taxonomy: Model.Taxonomy) {
            Taxonomy.Concepts = window["concepts"] == null ? [] : window["concepts"];
            //Taxonomy.Concepts = this.Concepts.AsLinq<Model.Concept>().Where(i=> i.Domain != null).ToArray();
            Taxonomy.Hierarchies = window["hierarchies"] == null ? [] : window["hierarchies"];

        }
    }
}

var taxonomycontainer = new Control.InstanceContainer();